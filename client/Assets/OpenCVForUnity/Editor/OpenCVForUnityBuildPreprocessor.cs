#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace OpenCVForUnity.Editor
{
    public class OpenCVForUnityBuildPreprocessor : IPreprocessBuildWithReport
    {
        // Public Properties
        public int callbackOrder { get { return 0; } }

        // Public Methods
        public void OnPreprocessBuild(BuildReport report)
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets("OpenCVForUnityBuildPreprocessor");
            if (guids.Length == 0)
            {
                Debug.LogWarning("SetPluginImportSettings Failed : OpenCVForUnityBuildPreprocessor.cs is missing.");
                return;
            }
            string opencvForUnityFolderPath = AssetDatabase.GUIDToAssetPath(guids[0]).Substring(0, AssetDatabase.GUIDToAssetPath(guids[0]).LastIndexOf("/Editor/OpenCVForUnityBuildPreprocessor.cs"));

            string pluginsFolderPath = opencvForUnityFolderPath + "/Plugins";
            //Debug.Log("pluginsFolderPath " + pluginsFolderPath);

            string extraFolderPath = opencvForUnityFolderPath + "/Extra";
            //Debug.Log("extraFolderPath " + extraFolderPath);

            Debug.Log("OpenCVForUnityBuildPreprocessor " + report.summary.platform);

            bool incrementalBuild = (report.summary.options & BuildOptions.AcceptExternalModificationsToPlayer) == BuildOptions.AcceptExternalModificationsToPlayer;

            switch (report.summary.platform)
            {
                case BuildTarget.StandaloneOSX:
                    OpenCVForUnityMenuItem.SetOSXPluginImportSettings(pluginsFolderPath, extraFolderPath);
                    break;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:

                    OpenCVForUnityMenuItem.SetWindowsPluginImportSettings(pluginsFolderPath, extraFolderPath);
                    break;
                case BuildTarget.iOS:
                    OpenCVForUnityMenuItem.SetIOSPluginImportSettings(pluginsFolderPath, extraFolderPath, false, incrementalBuild);
                    break;
#if (UNITY_2022_3_OR_NEWER && !(UNITY_2023_1_OR_NEWER) && !(UNITY_2022_3_0 || UNITY_2022_3_1 || UNITY_2022_3_2 || UNITY_2022_3_3 || UNITY_2022_3_4 || UNITY_2022_3_5 || UNITY_2022_3_6 || UNITY_2022_3_7 || UNITY_2022_3_8 || UNITY_2022_3_9 || UNITY_2022_3_10 || UNITY_2022_3_11 || UNITY_2022_3_12 || UNITY_2022_3_13 || UNITY_2022_3_14 || UNITY_2022_3_15 || UNITY_2022_3_16 || UNITY_2022_3_17)) || UNITY_6000_0_OR_NEWER
                case BuildTarget.VisionOS:
                    OpenCVForUnityMenuItem.SetVisionOSPluginImportSettings(pluginsFolderPath, extraFolderPath, false, incrementalBuild);
                    break;
#endif
                case BuildTarget.Android:

                    OpenCVForUnityMenuItem.SetAndroidPluginImportSettings(pluginsFolderPath, extraFolderPath);
                    break;
                case BuildTarget.StandaloneLinux64:

                    OpenCVForUnityMenuItem.SetLinuxPluginImportSettings(pluginsFolderPath, extraFolderPath);
                    break;
                case BuildTarget.WebGL:

                    OpenCVForUnityMenuItem.SetWebGLPluginImportSettings(pluginsFolderPath, extraFolderPath);
                    break;
                case BuildTarget.WSAPlayer:

                    OpenCVForUnityMenuItem.SetUWPPluginImportSettings(pluginsFolderPath, extraFolderPath);
                    break;
                case BuildTarget.NoTarget:

                    break;
                default:

                    break;
            }

        }
    }
}
#endif
