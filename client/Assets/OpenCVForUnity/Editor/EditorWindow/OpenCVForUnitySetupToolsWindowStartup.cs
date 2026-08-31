#if UNITY_EDITOR
using UnityEditor;

namespace OpenCVForUnity.Editor
{
    [InitializeOnLoad]
    public class OpenCVForUnitySetupToolsWindowStartup
    {
        // Private Methods
        static OpenCVForUnitySetupToolsWindowStartup()
        {
            EditorApplication.update -= ShowSetupToolsWindow;
            EditorApplication.update += ShowSetupToolsWindow;

            EditorApplication.playModeStateChanged -= PlayModeChanged;
            EditorApplication.playModeStateChanged += PlayModeChanged;
        }

        private static void ShowSetupToolsWindow()
        {
            //Debug.Log("OpenCVForUnityProjectSettings.Instance.ShowSetupToolsWindowFlag: " + OpenCVForUnityProjectSettings.Instance.ShowSetupToolsWindowFlag);

            var showAtStartup = OpenCVForUnityProjectSettings.Instance.ShowSetupToolsWindowFlag;

            if (showAtStartup)
            {
                OpenCVForUnitySetupToolsWindow.OpenSetupToolsWindow();

                OpenCVForUnityProjectSettings.Instance.ShowSetupToolsWindowFlag = false;
                EditorUtility.SetDirty(OpenCVForUnityProjectSettings.Instance);

                OpenCVForUnityMenuItem.SetPluginImportSettings();
            }

            EditorApplication.update -= ShowSetupToolsWindow;
        }

        private static void PlayModeChanged(PlayModeStateChange playMode)
        {
            EditorApplication.update -= ShowSetupToolsWindow;
        }
    }
}
#endif
