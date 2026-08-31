using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Net;

public class SceneManagerVRCard : MonoBehaviour
{
    public static SceneManagerVRCard Instance;
    string playerWonTextString = "";
    [SerializeField] private string gameSceneName = "A";
    private bool gameLoaded = false;
    public TextMeshProUGUI playerWonText;
    private GameObject connectionDialog;

    public static string ExperimentalServerUrl = "http://192.168.178.69:18082";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (GameData.playerWon != 0)
        {
            playerWonTextString = "Player " + GameData.playerWon.ToString() + " has won the game!";
            SetText(playerWonTextString , playerWonText);
            // set player has won text
        }
        Instance = this;
    }

    public void StartGame()
    {
        if (!gameLoaded)
        {
            StartCoroutine(LoadGameScene());
        }
    }

    public void EndGame()
    {
        if (gameLoaded)
        {
            StartCoroutine(UnloadGameScene());
        }
    }

    private IEnumerator LoadGameScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);

        while (!op.isDone)
            yield return null;

        gameLoaded = true;

        Scene loadedScene = SceneManager.GetSceneByName(gameSceneName);
        if (loadedScene.IsValid())
        {
            SceneManager.SetActiveScene(loadedScene);
        }
    }

    public void LoadGameSceneFull()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void RunMode(string x)
    {
        if (x == "E")
        {
            ShowExperimentalConnectionDialog();
            return;
        }

        switch (x)
        {
            case "A":
                gameSceneName = "A";
                break;
            case "B":
                gameSceneName = "B";
                break;
            case "C":
                gameSceneName = "C";
                break;
            default:
                break;
        }
        LoadGameSceneFull();
    }

    private void ShowExperimentalConnectionDialog()
    {
        if (connectionDialog != null) return;

        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Kein Canvas für den Serverdialog gefunden.");
            return;
        }

        connectionDialog = new GameObject("ExperimentalConnectionDialog");
        connectionDialog.transform.SetParent(canvas.transform, false);
        RectTransform panel = connectionDialog.AddComponent<RectTransform>();
        panel.anchorMin = new Vector2(.5f, .5f);
        panel.anchorMax = new Vector2(.5f, .5f);
        panel.sizeDelta = new Vector2(600f, 420f);
        connectionDialog.AddComponent<Image>().color = new Color(.08f, .08f, .08f, .96f);

        TMP_FontAsset font = FindFirstObjectByType<TextMeshProUGUI>()?.font;
        CreateText("Title", "Experimental - Server Connection", panel, new Vector2(0, 130), new Vector2(560, 60), 30, font);
        TMP_InputField ip = CreateInput("IP Address", panel, new Vector2(0, 55), font);
        ip.text = "192.168.178.69";
        TMP_InputField port = CreateInput("Port", panel, new Vector2(0, -35), font);
        port.contentType = TMP_InputField.ContentType.IntegerNumber;
        port.keyboardType = TouchScreenKeyboardType.NumberPad;
        port.text = "18082";
        TextMeshProUGUI error = CreateText("Error", "", panel, new Vector2(0, -95), new Vector2(560, 45), 20, font);
        error.color = Color.red;

        GameObject connect = CreateButton("Connect", panel, new Vector2(130, -155), font);
        connect.GetComponent<Button>().onClick.AddListener(() =>
        {
            string ipText = ip.text.Trim();
            if (!IPAddress.TryParse(ipText, out _) || !int.TryParse(port.text.Trim(), out int portNumber) || portNumber < 1 || portNumber > 65535)
            {
                error.text = "Please enter a valid IP address and port (1-65535).";
                return;
            }

            ExperimentalServerUrl = $"http://{ipText}:{portNumber}";
            PlayerPrefs.SetString("ExperimentalServerUrl", ExperimentalServerUrl);
            PlayerPrefs.Save();
            Destroy(connectionDialog);
            connectionDialog = null;
            gameSceneName = "Experimental";
            LoadGameSceneFull();
        });

        GameObject cancel = CreateButton("Cancel", panel, new Vector2(-130, -155), font);
        cancel.GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(connectionDialog);
            connectionDialog = null;
        });
    }

    private static TMP_InputField CreateInput(string placeholder, Transform parent, Vector2 position, TMP_FontAsset font)
    {
        GameObject go = new GameObject(placeholder + "Input");
        go.transform.SetParent(parent, false);
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(500, 65);
        go.AddComponent<Image>().color = Color.white;
        TMP_InputField input = go.AddComponent<TMP_InputField>();
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(go.transform, false);
        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.font = font;
        text.fontSize = 24;
        text.color = Color.black;
        text.rectTransform.anchorMin = Vector2.zero;
        text.rectTransform.anchorMax = Vector2.one;
        text.rectTransform.offsetMin = new Vector2(15, 0);
        text.rectTransform.offsetMax = new Vector2(-15, 0);
        input.textComponent = text;
        input.shouldHideMobileInput = false;
        input.keyboardType = TouchScreenKeyboardType.Default;
        input.placeholder = CreateText("Placeholder", placeholder, go.transform, Vector2.zero, new Vector2(500, 65), 24, font);
        input.placeholder.color = Color.gray;
        return input;
    }

    private static TextMeshProUGUI CreateText(string name, string value, Transform parent, Vector2 position, Vector2 size, float fontSize, TMP_FontAsset font)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = size;
        TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
        text.text = value;
        text.font = font;
        text.fontSize = fontSize;
        text.alignment = TextAlignmentOptions.Center;
        return text;
    }

    private static GameObject CreateButton(string label, Transform parent, Vector2 position, TMP_FontAsset font)
    {
        GameObject go = new GameObject(label);
        go.transform.SetParent(parent, false);
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(220, 65);
        go.AddComponent<Image>().color = new Color(.2f, .45f, .8f, 1f);
        go.AddComponent<Button>();
        CreateText("Label", label, go.transform, Vector2.zero, new Vector2(220, 65), 24, font);
        return go;
    }

    private IEnumerator UnloadGameScene()
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(gameSceneName);

        while (op != null && !op.isDone)
            yield return null;

        gameLoaded = false;

        Scene managerScene = SceneManager.GetSceneByName("ManagerScene");
        if (managerScene.IsValid())
        {
            SceneManager.SetActiveScene(managerScene);
        }
    }

    public void SetText(string newText, TextMeshProUGUI tmp)
    {
        if (tmp != null)
        {
            tmp.text = newText;
        }
    }
}
