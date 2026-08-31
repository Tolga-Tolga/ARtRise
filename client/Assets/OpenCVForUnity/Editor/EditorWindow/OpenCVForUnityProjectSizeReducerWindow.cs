#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.Editor
{
    /// <summary>
    /// Editor window for reducing OpenCVForUnity project size.
    /// Allows users to remove unnecessary platform-specific plugins, examples, and other components to reduce project size.
    /// </summary>
    public class OpenCVForUnityProjectSizeReducerWindow : EditorWindow
    {
        // Private Fields
        private Dictionary<string, bool> _platformFolders = new Dictionary<string, bool>();
        private Dictionary<string, bool> _additionalComponents = new Dictionary<string, bool>();
        private Dictionary<string, long> _componentSizes = new Dictionary<string, long>();
        private Dictionary<string, bool> _platformLocked = new Dictionary<string, bool>();
        private Vector2 _scrollPosition;
        private string _opencvForUnityFolderPath;
        private string _pluginsFolderPath;
        private bool _isInitialized = false;
        private bool _showPlatforms = true;
        private long _totalOriginalSize = 0;
        private long _totalRemainingSize = 0;
        private GUIStyle _rightAlignedStyle;
        private GUIStyle _boldRightAlignedStyle;
        private GUIStyle _disabledLabelStyle;
        private bool _stylesInitialized = false;

        // Unity Lifecycle Methods
        private void OnEnable()
        {
            InitializeFolders();
            EditorApplication.projectChanged += OnProjectChanged;
        }

        private void OnDisable()
        {
            EditorApplication.projectChanged -= OnProjectChanged;
        }

        private void OnGUI()
        {
            if (!_isInitialized)
            {
                InitializeFolders();
                return;
            }

            InitializeStyles();

            EditorGUILayout.Space(10);
            EditorGUILayout.HelpBox(
                "Unnecessary components can be removed to reduce the size of the project.\n" +
                "Uncheck the components you want to remove from your project.\n" +
                "To restore removed components, please re-import the OpenCV for Unity asset.",
                MessageType.Info
            );
            EditorGUILayout.Space(10);

            // Display total size information
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Total Size: {FormatFileSize(_totalRemainingSize)} / {FormatFileSize(_totalOriginalSize)}", _boldRightAlignedStyle);
            EditorGUILayout.Space(5, false);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            EditorGUI.BeginChangeCheck();

            // Draw OpenCVForUnity header
            EditorGUILayout.BeginHorizontal(EditorStyles.boldLabel);
            EditorGUILayout.LabelField("OpenCVForUnity", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            // Draw Examples folder
            if (_additionalComponents.ContainsKey("Examples"))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(10, false);
                bool examplesChanged = EditorGUILayout.ToggleLeft("Examples", _additionalComponents["Examples"]);
                if (examplesChanged != _additionalComponents["Examples"])
                {
                    _additionalComponents["Examples"] = examplesChanged;
                    CalculateTotalSizes();
                }
                EditorGUILayout.LabelField(FormatFileSize(_componentSizes["Examples"]), _rightAlignedStyle, GUILayout.Width(80));
                EditorGUILayout.Space(5, false);
                EditorGUILayout.EndHorizontal();
            }

            // Draw Plugins folder with platforms
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(10, false);
            _showPlatforms = EditorGUILayout.Foldout(_showPlatforms, "Plugins", true);
            EditorGUILayout.EndHorizontal();

            if (_showPlatforms)
            {
                // Create a list of keys to avoid modifying the dictionary during enumeration
                List<string> platformKeys = new List<string>(_platformFolders.Keys);
                foreach (var platformKey in platformKeys)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Space(20, false);

                    // Check if platform is locked
                    if (_platformLocked[platformKey])
                    {
                        // Draw disabled toggle and label
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.ToggleLeft(platformKey, _platformFolders[platformKey]);
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        // Draw normal toggle
                        bool platformChanged = EditorGUILayout.ToggleLeft(platformKey, _platformFolders[platformKey]);
                        if (platformChanged != _platformFolders[platformKey])
                        {
                            _platformFolders[platformKey] = platformChanged;
                            CalculateTotalSizes();
                        }
                    }

                    EditorGUILayout.LabelField(FormatFileSize(_componentSizes[platformKey]), _rightAlignedStyle, GUILayout.Width(80));
                    EditorGUILayout.Space(5, false);
                    EditorGUILayout.EndHorizontal();
                }
            }

            // Draw StreamingAssets folder
            if (_additionalComponents.ContainsKey("StreamingAssets"))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(10, false);
                bool streamingAssetsChanged = EditorGUILayout.ToggleLeft("StreamingAssets", _additionalComponents["StreamingAssets"]);
                if (streamingAssetsChanged != _additionalComponents["StreamingAssets"])
                {
                    _additionalComponents["StreamingAssets"] = streamingAssetsChanged;
                    CalculateTotalSizes();
                }
                EditorGUILayout.LabelField(FormatFileSize(_componentSizes["StreamingAssets"]), _rightAlignedStyle, GUILayout.Width(80));
                EditorGUILayout.Space(5, false);
                EditorGUILayout.EndHorizontal();
            }

            // Draw Extra.unitypackage
            if (_additionalComponents.ContainsKey("Extra.unitypackage"))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(10, false);
                bool extraPackageChanged = EditorGUILayout.ToggleLeft("Extra.unitypackage", _additionalComponents["Extra.unitypackage"]);
                if (extraPackageChanged != _additionalComponents["Extra.unitypackage"])
                {
                    _additionalComponents["Extra.unitypackage"] = extraPackageChanged;
                    CalculateTotalSizes();
                }
                EditorGUILayout.LabelField(FormatFileSize(_componentSizes["Extra.unitypackage"]), _rightAlignedStyle, GUILayout.Width(80));
                EditorGUILayout.Space(5, false);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(10);

            // Add All/None buttons
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("All"))
            {
                // Set all checkboxes to true
                foreach (var platformKey in _platformFolders.Keys.ToList())
                {
                    if (!_platformLocked[platformKey])
                    {
                        _platformFolders[platformKey] = true;
                    }
                }
                foreach (var componentKey in _additionalComponents.Keys.ToList())
                {
                    _additionalComponents[componentKey] = true;
                }
                CalculateTotalSizes();
            }
            if (GUILayout.Button("None"))
            {
                // Set all checkboxes to false
                foreach (var platformKey in _platformFolders.Keys.ToList())
                {
                    if (!_platformLocked[platformKey])
                    {
                        _platformFolders[platformKey] = false;
                    }
                }
                foreach (var componentKey in _additionalComponents.Keys.ToList())
                {
                    _additionalComponents[componentKey] = false;
                }
                CalculateTotalSizes();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space(10);

            GUI.enabled = _platformFolders.Count > 0 || _additionalComponents.Count > 0;
            if (GUILayout.Button("Remove Unchecked Components"))
            {
                DeleteSelectedComponents();
            }
            GUI.enabled = true;

            EditorGUILayout.Space(10);
        }

        // Public Methods
        [MenuItem("Tools/OpenCV for Unity/Open Project Size Reducer", false, 13)]
        public static void OpenProjectSizeReducerWindow()
        {
            OpenCVForUnityProjectSizeReducerWindow m_Window = GetWindow<OpenCVForUnityProjectSizeReducerWindow>(true, "OpenCV for Unity | Project Size Reducer");
            //m_Window.minSize = new Vector2(360, 400);
            //m_Window.maxSize = new Vector2(320, 800);
        }

        // Private Methods
        private void OnProjectChanged()
        {
            CheckForFileSystemChanges();
        }

        private void CheckForFileSystemChanges()
        {
            if (!_isInitialized)
                return;

            bool hasChanges = false;

            // Check for platform folders existence
            foreach (var platform in new Dictionary<string, bool>(_platformFolders))
            {
                string folderPath = Path.Combine(_pluginsFolderPath, platform.Key);
                if (!Directory.Exists(folderPath))
                {
                    _platformFolders.Remove(platform.Key);
                    _componentSizes.Remove(platform.Key);
                    _platformLocked.Remove(platform.Key);
                    hasChanges = true;
                }
                else
                {
                    // Recalculate size for existing folders
                    long newSize = CalculateDirectorySize(folderPath);
                    if (newSize != _componentSizes[platform.Key])
                    {
                        _componentSizes[platform.Key] = newSize;
                        hasChanges = true;
                    }
                }
            }

            // Check for additional components existence
            foreach (var component in new Dictionary<string, bool>(_additionalComponents))
            {
                string itemPath = Path.Combine(_opencvForUnityFolderPath, component.Key);
                if (!Directory.Exists(itemPath) && !File.Exists(itemPath))
                {
                    _additionalComponents.Remove(component.Key);
                    _componentSizes.Remove(component.Key);
                    hasChanges = true;
                }
                else
                {
                    // Recalculate size for existing components
                    long newSize = 0;
                    if (Directory.Exists(itemPath))
                    {
                        newSize = CalculateDirectorySize(itemPath);
                    }
                    else if (File.Exists(itemPath))
                    {
                        newSize = new FileInfo(itemPath).Length;
                    }

                    if (newSize != _componentSizes[component.Key])
                    {
                        _componentSizes[component.Key] = newSize;
                        hasChanges = true;
                    }
                }
            }

            // Check for new platform folders
            if (Directory.Exists(_pluginsFolderPath))
            {
                string[] directories = Directory.GetDirectories(_pluginsFolderPath);
                foreach (string dir in directories)
                {
                    string platformName = Path.GetFileName(dir);
                    if (!_platformFolders.ContainsKey(platformName))
                    {
                        _platformFolders[platformName] = true;
                        _componentSizes[platformName] = CalculateDirectorySize(dir);
                        _platformLocked[platformName] = IsPlatformLocked(platformName, GetCurrentPlatform());
                        hasChanges = true;
                    }
                }
            }

            // Check for new additional components
            string[] expectedComponents = { "Examples", "StreamingAssets", "Extra.unitypackage" };
            foreach (string component in expectedComponents)
            {
                string itemPath = Path.Combine(_opencvForUnityFolderPath, component);
                if ((Directory.Exists(itemPath) || File.Exists(itemPath)) && !_additionalComponents.ContainsKey(component))
                {
                    _additionalComponents[component] = true;
                    if (Directory.Exists(itemPath))
                        _componentSizes[component] = CalculateDirectorySize(itemPath);
                    else if (File.Exists(itemPath))
                        _componentSizes[component] = new FileInfo(itemPath).Length;
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                CalculateTotalSizes();
                Repaint();
            }
        }

        private void InitializeFolders()
        {
            _opencvForUnityFolderPath = GetOpenCVForUnityFolderPath();
            if (string.IsNullOrEmpty(_opencvForUnityFolderPath))
            {
                return;
            }

            _pluginsFolderPath = Path.Combine(_opencvForUnityFolderPath, "Plugins");
            if (!Directory.Exists(_pluginsFolderPath))
            {
                Debug.LogError("OpenCVForUnity Plugins folder not found.\nPlease make sure OpenCVForUnity is properly imported.");
                return;
            }

            // Initialize styles
            InitializeStyles();

            // Get current platform
            string currentPlatform = GetCurrentPlatform();

            // Initialize platform folders
            _platformFolders.Clear();
            _componentSizes.Clear();
            _platformLocked.Clear();
            string[] directories = Directory.GetDirectories(_pluginsFolderPath);
            foreach (string dir in directories)
            {
                string platformName = Path.GetFileName(dir);
                _platformFolders[platformName] = true; // Default to checked
                long size = CalculateDirectorySize(dir);
                _componentSizes[platformName] = size;

                // Check if this platform is locked (current OS only)
                bool isLocked = IsPlatformLocked(platformName, currentPlatform);
                _platformLocked[platformName] = isLocked;

                // If locked, ensure it's checked
                if (isLocked)
                {
                    _platformFolders[platformName] = true;
                }
            }

            // Initialize additional components
            _additionalComponents.Clear();

            // Add only existing components
            string examplesPath = Path.Combine(_opencvForUnityFolderPath, "Examples");
            if (Directory.Exists(examplesPath))
            {
                _additionalComponents["Examples"] = true;
                _componentSizes["Examples"] = CalculateDirectorySize(examplesPath);
            }

            string streamingAssetsPath = Path.Combine(_opencvForUnityFolderPath, "StreamingAssets");
            if (Directory.Exists(streamingAssetsPath))
            {
                _additionalComponents["StreamingAssets"] = true;
                _componentSizes["StreamingAssets"] = CalculateDirectorySize(streamingAssetsPath);
            }

            string extraPackagePath = Path.Combine(_opencvForUnityFolderPath, "Extra.unitypackage");
            if (File.Exists(extraPackagePath))
            {
                _additionalComponents["Extra.unitypackage"] = true;
                _componentSizes["Extra.unitypackage"] = new FileInfo(extraPackagePath).Length;
            }

            // Calculate total sizes
            CalculateTotalSizes();

            _isInitialized = true;
        }

        private void InitializeStyles()
        {
            if (_stylesInitialized) return;

            _rightAlignedStyle = new GUIStyle(EditorStyles.label);
            _rightAlignedStyle.alignment = TextAnchor.MiddleRight;
            _boldRightAlignedStyle = new GUIStyle(EditorStyles.boldLabel);
            _boldRightAlignedStyle.alignment = TextAnchor.MiddleRight;
            _disabledLabelStyle = new GUIStyle(EditorStyles.label);
            _disabledLabelStyle.normal.textColor = new Color(0.5f, 0.5f, 0.5f, 1f);

            _stylesInitialized = true;
        }

        private string GetCurrentPlatform()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                return "Windows";
            else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
                return "macOS";
            else if (Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.LinuxPlayer)
                return "Linux";
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                return "iOS";
            else if (Application.platform == RuntimePlatform.Android)
                return "Android";
            else if (Application.platform == RuntimePlatform.WebGLPlayer)
                return "WebGL";
            else
                return "";
        }

        private bool IsPlatformLocked(string platformName, string currentPlatform)
        {
            // Check if this is the current OS platform only
            return platformName.Equals(currentPlatform, System.StringComparison.OrdinalIgnoreCase);
        }

        private void CalculateTotalSizes()
        {
            _totalOriginalSize = 0;
            _totalRemainingSize = 0;

            // Add platform folder sizes
            foreach (var platform in _platformFolders)
            {
                _totalOriginalSize += _componentSizes[platform.Key];
                if (platform.Value)
                {
                    _totalRemainingSize += _componentSizes[platform.Key];
                }
            }

            // Add additional component sizes
            foreach (var component in _additionalComponents)
            {
                _totalOriginalSize += _componentSizes[component.Key];
                if (component.Value)
                {
                    _totalRemainingSize += _componentSizes[component.Key];
                }
            }
        }

        private long CalculateDirectorySize(string directoryPath)
        {
            long size = 0;
            try
            {
                // Get all files in the directory
                string[] files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    // Skip .meta files
                    if (file.EndsWith(".meta"))
                        continue;

                    FileInfo fileInfo = new FileInfo(file);
                    size += fileInfo.Length;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error calculating size for {directoryPath}: {e.Message}");
            }
            return size;
        }

        private string GetOpenCVForUnityFolderPath()
        {
            string[] guids = AssetDatabase.FindAssets("OpenCVForUnityProjectSizeReducerWindow");
            if (guids.Length == 0)
            {
                Debug.LogError("OpenCVForUnityProjectSizeReducerWindow.cs is missing.");
                return null;
            }

            string currentScriptPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            string opencvForUnityFolderPath = currentScriptPath.Substring(0, currentScriptPath.LastIndexOf("/Editor/EditorWindow"));

            return opencvForUnityFolderPath;
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;
            }

            return $"{size:0.00} {sizes[order]}";
        }

        private void DeleteSelectedComponents()
        {
            List<string> itemsToDelete = new List<string>();

            // Collect platform folders to delete
            foreach (var platform in _platformFolders)
            {
                if (!platform.Value && !_platformLocked[platform.Key])
                {
                    string folderPath = Path.Combine(_pluginsFolderPath, platform.Key);
                    itemsToDelete.Add(folderPath);
                }
            }

            // Collect additional components to delete
            foreach (var component in _additionalComponents)
            {
                if (!component.Value)
                {
                    string itemPath = Path.Combine(_opencvForUnityFolderPath, component.Key);
                    itemsToDelete.Add(itemPath);
                }
            }

            if (itemsToDelete.Count == 0)
            {
                EditorUtility.DisplayDialog("Project Size Reducer",
                    "No components selected for removal.",
                    "OK");
                return;
            }

            string message = "The following components will be removed:\n\n";
            foreach (string item in itemsToDelete)
            {
                message += $"- {item.Replace("\\", "/")}\n";
            }
            message += "\nThis operation cannot be undone. Continue?";

            if (EditorUtility.DisplayDialog("Project Size Reducer - Confirmation", message, "Remove", "Cancel"))
            {
                foreach (string item in itemsToDelete)
                {
                    if (Directory.Exists(item))
                    {
                        AssetDatabase.DeleteAsset(item);
                    }
                    else if (File.Exists(item))
                    {
                        AssetDatabase.DeleteAsset(item);
                    }
                }
                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("Project Size Reducer",
                    "Selected components have been successfully removed.",
                    "OK");
            }
        }
    }
}
#endif
