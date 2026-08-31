using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GlbPreloader : MonoBehaviour
{
    [Tooltip("Dateinamen inkl. Endung, z.B. \"1.glb\"")]
    public string[] glbFileNames = { "1.glb", "2.glb", "3.glb", "4.glb", "5.glb", "6.glb", "7.glb", "8.glb", "9.glb", "10.glb", "neg1.glb", "neg2.glb", "neg3.glb", "neg4.glb", "neg5.glb", "neg6.glb", "neg7.glb", "neg8.glb", "neg9.glb", "neg10.glb"};

    private void Awake()
    {
        StartCoroutine(CopyGlbsToPersistent());
    }

    private IEnumerator CopyGlbsToPersistent()
    {
        string dstDir = Path.Combine(Application.persistentDataPath, "gameobjects/preloaded");
        if (!Directory.Exists(dstDir)) Directory.CreateDirectory(dstDir);

        Debug.Log($"[GlbPreloader] persistentDataPath: {Application.persistentDataPath}");

        foreach (var file in glbFileNames)
        {
            string src = Path.Combine(Application.streamingAssetsPath, "gameobjects/" + file);
            string dst = Path.Combine(dstDir, file);

            if (File.Exists(dst))
            {
                // Datei schon vorhanden -> weiter
                continue;
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            using (UnityWebRequest req = UnityWebRequest.Get(src))
            {
                yield return req.SendWebRequest();

                if (req.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"[GlbPreloader] Konnte {file} nicht aus StreamingAssets laden: {req.error} ({src})");
                    continue;
                }

                try
                {
                    File.WriteAllBytes(dst, req.downloadHandler.data);
                    Debug.Log($"[GlbPreloader] Kopiert: {dst}");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"[GlbPreloader] WriteAllBytes fehlgeschlagen: {ex.Message}");
                }
            }
#else
            // Editor/Standalone: direkte Dateikopie + kurzer Frame-Yield,
            // damit diese Methode eine gültige Coroutine bleibt.
            try
            {
                File.Copy(src, dst, overwrite: true);
                Debug.Log($"[GlbPreloader] Kopiert (Editor/PC): {dst}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[GlbPreloader] File.Copy fehlgeschlagen von {src} nach {dst}: {ex.Message}");
            }
            // Mindestens ein Yield je Iteration:
            yield return null;
#endif
        }

        // Sauber beenden
        yield break;
    }
}
