#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace OpenCVForUnity.Editor
{
    public class OpenCVForUnityProjectSettings : ScriptableObject
    {
        // Private Fields
        private static OpenCVForUnityProjectSettings _instance;

        // Public Fields
        public bool ShowSetupToolsWindowFlag;

        // Public Properties
        public static OpenCVForUnityProjectSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    string path = GetEditorFolderPath() + "/OpenCVForUnityProjectSettings.asset";
                    _instance = AssetDatabase.LoadAssetAtPath<OpenCVForUnityProjectSettings>(path);

                    if (_instance == null)
                    {
                        _instance = ScriptableObject.CreateInstance<OpenCVForUnityProjectSettings>();
                        _instance.ShowSetupToolsWindowFlag = true;
                        AssetDatabase.CreateAsset(_instance, path);
                    }
                }

                return _instance;
            }
        }

        // Private Methods
        private static string GetEditorFolderPath()
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets("OpenCVForUnityProjectSettings");
            if (guids.Length == 0)
            {
                Debug.LogWarning("OpenCVForUnityProjectSettings.cs is missing.");
                return null;
            }
            string editorFolderPath = AssetDatabase.GUIDToAssetPath(guids[0]).Substring(0, AssetDatabase.GUIDToAssetPath(guids[0]).LastIndexOf("/OpenCVForUnityProjectSettings"));

            //Debug.Log("editorFolderPath " + editorFolderPath);

            return editorFolderPath;
        }
    }
}
#endif
