using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;


public class ModelPipelineCustom : MonoBehaviour
{

    private void OnDisable()
    {
        Debug.LogError("[Upload] ModelPipelineCustom DISABLED");
    }

    private void OnDestroy()
    {
        Debug.LogError("[Upload] ModelPipelineCustom DESTROYED");
    }
    
    // The ip has to be the ip of the pc which is hosting the rest server
    private const string URL = "http://192.168.178.69/files";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Persistent path: " + Application.persistentDataPath);
    }
    int counter = 0;
    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter == 100)
        {
            // StartCoroutine(DownloadFile("test.txt"));
            // StartCoroutine(UploadFile("test.txt"));
            counter =0;
        }
        // Debug.Log("Persistent path: " + Application.persistentDataPath);
    }

    public void uploadTextures(string id, Texture2D[] textures)
    {   
        Debug.Log("[Upload] UploadTexturesIntern");
        StartCoroutine(UploadTexturesIntern(id, textures));
    }

    private IEnumerator UploadTexturesIntern(string id, Texture2D[] textures)
    {   
        string id_last = id.Split('/')[^1];
        Debug.Log($"[Upload] textures.Length = {textures?.Length ?? 0}");

        if (textures == null || textures.Length == 0 || textures[0] == null)
        {
            Debug.LogWarning($"[Upload] No first texture available for id {id_last}");
            yield break;
        }

        byte[] data = textures[0].EncodeToPNG();
        string fileName = $"{id_last}.png";
        Debug.Log("[Upload] filename is: " + fileName);

        yield return UploadFile(data, fileName);
    }

    private IEnumerator UploadFile(string filePath)
    {
        string uploadFilePath = Path.Combine(Application.persistentDataPath, filePath);
        byte[] fileData = System.IO.File.ReadAllBytes(uploadFilePath);
        string fileName = System.IO.Path.GetFileName(uploadFilePath);
        Debug.Log("[Upload] Trying to upload file " + fileName);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, fileName, "application/octet-stream");
        UnityWebRequest req = UnityWebRequest.Post(SceneManagerVRCard.ExperimentalServerUrl.TrimEnd('/') + "/files/upload", form);
        Debug.Log("[Upload] Post request has been created!");
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("[Upload] Upload OK: " + req.downloadHandler.text);
        }
        else
        {
            Debug.LogError("[Upload] Upload Failed: " + req.error);
        }
    }

    private IEnumerator UploadFile(byte[] fileData, string fileName)
    {
        Debug.Log("[Upload] Trying to upload file " + fileName);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, fileName, "application/octet-stream");
        UnityWebRequest req = UnityWebRequest.Post(SceneManagerVRCard.ExperimentalServerUrl.TrimEnd('/') + "/files/upload", form);
        Debug.Log("[Upload] Post request has been created!");
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("[Upload] Upload OK: " + req.downloadHandler.text);
        }
        else
        {
            Debug.LogError("[Upload] Upload Failed: " + req.error);
        }
    }

    private IEnumerator DownloadFile(string id)
    {
        Debug.Log("Trying to download the file: " + id );
        string url = $"http://192.168.178.69:18082/files/download/{id}";
        Debug.Log("The download URL is: " + url);
        UnityWebRequest req = UnityWebRequest.Get(url);
        Debug.Log("The request is: " + req);
        yield return req.SendWebRequest();
        string savePath = Path.Combine(Application.persistentDataPath, "text.txt");
        if (req.result == UnityWebRequest.Result.Success)
        {
            byte[] data = req.downloadHandler.data;
            File.WriteAllBytes(savePath, data);
            Debug.Log("Saved to: " + savePath);
            string content = File.ReadAllText(savePath);
            Debug.Log("Datei Inhalt:\n" + content);
        }
        else
        {
            Debug.LogError("Download Failed: " + req.error);
        }
    }

    private IEnumerator DownloadGlbFile(string id)
    {
        Debug.Log("Trying to download the GLB file: " + id);

        string fileName = id;

        if (!fileName.EndsWith(".glb"))
        {
            fileName += ".glb";
        }

        string url = $"http://192.168.178.69:18082/files/download/{fileName}";
        Debug.Log("The download URL is: " + url);

        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                byte[] data = req.downloadHandler.data;

                string savePath = Path.Combine(Application.persistentDataPath, fileName);

                File.WriteAllBytes(savePath, data);

                Debug.Log("GLB download successful.");
                Debug.Log("Saved to: " + savePath);
                Debug.Log("Downloaded bytes: " + data.Length);
            }
            else
            {
                Debug.LogError("GLB download failed: " + req.error);
                Debug.LogError("Response code: " + req.responseCode);
                Debug.LogError("Server response: " + req.downloadHandler.text);
            }
        }
    }
}
