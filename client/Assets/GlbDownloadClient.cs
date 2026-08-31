using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GlbDownloadClient : MonoBehaviour
{
    private string serverBaseUrl = "http://192.168.178.69:18082";

    private HashSet<string> alreadyDownloaded = new HashSet<string>();

    public void StartCheckingForGlbs()
    {
        StartCoroutine(CheckKnownGlbsLoop());
    }

    private IEnumerator CheckKnownGlbsLoop()
    {
        while (true)
        {
            for (int i = -10; i <= 10; i++)
            {
                string fileName = i + ".glb";

                if (!alreadyDownloaded.Contains(fileName))
                {
                    yield return StartCoroutine(CheckAndDownload(fileName));
                }
            }

            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator CheckAndDownload(string fileName)
    {
        string existsUrl = $"{serverBaseUrl}/files/exists/{fileName}";

        using (UnityWebRequest req = UnityWebRequest.Get(existsUrl))
        {
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Exist check failed for " + fileName + ": " + req.error);
                yield break;
            }

            bool exists = req.downloadHandler.text.Trim().ToLower() == "true";

            if (!exists)
            {
                Debug.Log(fileName + " is not available yet.");
                yield break;
            }
        }

        yield return StartCoroutine(DownloadGlbFile(fileName));
    }

    private IEnumerator DownloadGlbFile(string fileName)
    {
        string url = $"{serverBaseUrl}/files/download/{fileName}";
        string savePath = Path.Combine(Application.persistentDataPath, fileName);

        Debug.Log("Downloading GLB: " + fileName);
        Debug.Log("URL: " + url);

        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                byte[] data = req.downloadHandler.data;
                File.WriteAllBytes(savePath, data);

                alreadyDownloaded.Add(fileName);

                Debug.Log("Downloaded " + fileName);
                Debug.Log("Saved to: " + savePath);
                Debug.Log("Bytes: " + data.Length);
            }
            else
            {
                Debug.LogError("Download failed for " + fileName + ": " + req.error);
                Debug.LogError("Response code: " + req.responseCode);
                Debug.LogError("Server response: " + req.downloadHandler.text);
            }
        }
    }
}