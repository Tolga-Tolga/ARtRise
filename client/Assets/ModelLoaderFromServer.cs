using UnityEngine;

public class ModelLoaderFromServer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Application.persistentDataPath);
    }
}
