#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using System.IO;
using System.Linq;

[InitializeOnLoad]
public static class ZXingDefineSymbolChecker
{
    static ZXingDefineSymbolChecker()
    {
        EditorApplication.delayCall += UpdateZXingDefine;
    }

    [MenuItem("Tools/Update ZXing Define Symbol")]
    public static void UpdateZXingDefine()
    {
        var hasZXing = HasZXingDLL();

        var targets = new[]
        {
            NamedBuildTarget.Standalone,
            NamedBuildTarget.Android,
            NamedBuildTarget.iOS,
        };

        foreach (var target in targets)
        {
            try
            {
                var defines = PlayerSettings.GetScriptingDefineSymbols(target);
                var defineList = defines.Split(';')
                                        .Select(s => s.Trim())
                                        .Where(s => !string.IsNullOrEmpty(s))
                                        .ToList();

                var symbolExists = defineList.Contains("ZXING_ENABLED");

                switch (hasZXing)
                {
                    case true when !symbolExists:
                        defineList.Add("ZXING_ENABLED");
                        Debug.Log($"[ZXingDefineSymbolChecker] Added ZXING_ENABLED for {target}");
                        break;
                    case false when symbolExists:
                        defineList.Remove("ZXING_ENABLED");
                        Debug.Log($"[ZXingDefineSymbolChecker] Removed ZXING_ENABLED for {target}");
                        break;
                }

                var newDefines = string.Join(";", defineList);
                PlayerSettings.SetScriptingDefineSymbols(target, newDefines);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"[ZXingDefineSymbolChecker] Could not update define for target {target}: {ex.Message}");
            }
        }
    }

    private static bool HasZXingDLL()
    {
        var files = Directory.GetFiles(Application.dataPath, "*ZXing.dll", SearchOption.AllDirectories)
                             .Concat(Directory.GetFiles(Application.dataPath, "*zxing.dll", SearchOption.AllDirectories));
        return files.Any();
    }
}
#endif
