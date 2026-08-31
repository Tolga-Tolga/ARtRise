using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GLTFast;
using UnityEngine;
using UnityEngine.Networking;

public class FloorCubeConsumerExperimental : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QrCodeDisplayManager qrCodeDisplayManager;

    [Tooltip("Optional: Parent für erzeugte Cubes / Modelle.")]
    public Transform cubesParent;

    [Header("Server GLB Download")]
    [SerializeField] private string serverBaseUrl = "http://192.168.178.69:18082";
    [SerializeField] private float downloadRetryDelaySeconds = 2f;
    [SerializeField] private string preloadedFolder = "gameobjects/preloaded";

    [Header("Smoothing")]
    [Tooltip("Kleinere Werte = weicher (langsamer).")]
    public float positionSmoothTime = 0.10f;

    public float positionMaxSpeed = 6f;

    [Tooltip("Grad pro Sekunde für weiches Rotieren.")]
    public float rotationDegPerSec = 360f;

    [Tooltip("Kleinere Werte = weicher (langsamer).")]
    public float scaleSmoothTime = 0.10f;

    [Header("Thresholds (Jitter-Filter)")]
    public float positionEpsilon = 0.01f;
    public float rotationEpsilonDeg = 1.5f;
    public float scaleEpsilon = 0.01f;

    [Header("Depth & Limits")]
    [Tooltip("Z-Dicke der Cubes / Modelle in Meter.")]
    public float depthThickness = 0.02f;

    [Tooltip("Clamp für XY-Skalen in Meter.")]
    public Vector2 xyClamp = new Vector2(0.05f, 1.5f);

    [Header("Misc")]
    public bool snapOnCreate = true;
    public bool setLayerAndTag = false;
    public int cubeLayer = 0;
    public string cubeTag = "Untagged";
    public Color cubeColor = Color.cyan;
    public string scannerObjectName = "QRCodeScanner";
    public bool hideModels = false;

    [Header("Other")]
    public float scaleMulitplier = 1f;

    [Header("State")]
    public VRState vrState;
    public GameManager gameManager;
    public List<int> activeCards;

    [Header("Model Placement")]
    [SerializeField] private float modelSurfaceOffset = 0.05f;

    private readonly Dictionary<string, GameObject> gameObjectsById = new();
    private readonly Dictionary<string, Vector3> posVelocity = new();
    private readonly Dictionary<string, Vector3> scaleVelocity = new();

    private readonly HashSet<string> loadedIds = new();
    private readonly HashSet<string> currentlyDownloadingIds = new();
    private readonly HashSet<string> animatedIds = new();

    private bool isExperimentalMode;

    private GameObject go;
    private GameObject go2;
    private bool posChanged;
    private bool rotChanged;
    private bool scaleChanged;
    private Vector3 currentPos;
    private Quaternion currentRot;
    private Quaternion targetRot;
    private Vector3 currentScale;
    private Vector3 vel;
    private Vector3 targetScale;
    private float maxStep;
    private Vector3 svel;
    private int poseid;

    private string LocalGlbDirectory
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, preloadedFolder);
        }
    }

    private void OnEnable()
    {
        string configuredUrl = SceneManagerVRCard.ExperimentalServerUrl;
        if (PlayerPrefs.HasKey("ExperimentalServerUrl"))
            configuredUrl = PlayerPrefs.GetString("ExperimentalServerUrl", configuredUrl);
        if (!string.IsNullOrWhiteSpace(configuredUrl))
            serverBaseUrl = configuredUrl.TrimEnd('/');

        if (qrCodeDisplayManager == null)
        {
            var scanner = GameObject.Find(scannerObjectName);
            if (scanner != null)
            {
                qrCodeDisplayManager = scanner.GetComponent<QrCodeDisplayManager>();
            }
        }

        if (gameManager == null)
        {
    #if UNITY_2023_1_OR_NEWER
            gameManager = FindFirstObjectByType<GameManager>();
    #else
            gameManager = FindObjectOfType<GameManager>();
    #endif
        }

        if (vrState == null)
        {
    #if UNITY_2023_1_OR_NEWER
            vrState = FindFirstObjectByType<VRState>();
    #else
            vrState = FindObjectOfType<VRState>();
    #endif
        }

        isExperimentalMode = vrState != null && vrState.experimentalMode;

        if (gameManager != null)
        {
            activeCards = gameManager.GetActiveCards();
        }
        else
        {
            activeCards = new List<int>();
            Debug.LogWarning("[FloorCubeConsumerExperimental] GameManager nicht gefunden. Prüfe, ob in dieser Szene ein aktives GameObject mit GameManager existiert.");
        }

        if (!isExperimentalMode)
        {
            PreloadAllCubesExperimental();
        }
    }

    private void LateUpdate()
    {
        if (qrCodeDisplayManager == null) return;
        if (qrCodeDisplayManager.isBlocked) return;

        var poses = qrCodeDisplayManager.objectPoses;
        if (poses == null) return;

        UpdateCubesSmooth(poses);
    }

    private void OnDisable()
    {
        CleanupAllCubes();
    }

    private void CleanupAllCubes()
    {
        foreach (var kv in gameObjectsById)
        {
            if (kv.Value != null)
            {
                Destroy(kv.Value);
            }
        }

        gameObjectsById.Clear();
        posVelocity.Clear();
        scaleVelocity.Clear();
        loadedIds.Clear();
        currentlyDownloadingIds.Clear();
        animatedIds.Clear();
    }

    private void UpdateCubesSmooth(List<QrCodeDisplayManager.MarkerPose> poses)
    {
        if (gameManager != null && gameManager.activeCardsListBlocked)
        {
            return;
        }

        foreach (var pose in poses)
        {
            if (!int.TryParse(pose.id, out poseid))
            {
                continue;
            }

            if (poseid == 0)
            {
                continue;
            }

            string poseId = poseid.ToString();
            if (!gameObjectsById.TryGetValue(poseId, out go) || go == null)
            {
                go = CreateInvisiblePlaceholder(poseId);
                gameObjectsById[poseId] = go;
                posVelocity[poseId] = Vector3.zero;
                scaleVelocity[poseId] = Vector3.zero;
                go.SetActive(false);
                StartModelLoad(poseId);
                Debug.Log("[FloorCubeConsumerExperimental] Started model loading for QR id " + poseId);
            }

            if (activeCards != null && activeCards.Contains(poseid))
            {
                go.SetActive(true);

                currentPos = go.transform.position;
                currentRot = go.transform.rotation;
                currentScale = go.transform.localScale;

                targetRot = pose.rotation * Quaternion.Euler(270f, 0f, 180f);

                if (poseid < 0)
                {
                    targetRot = targetRot * Quaternion.Euler(0f, 180f, 0f);
                }

                targetScale = pose.scale;
                targetScale.x = Mathf.Clamp(targetScale.x, xyClamp.x, xyClamp.y);
                targetScale.y = Mathf.Clamp(targetScale.y, xyClamp.x, xyClamp.y);
                targetScale.z = depthThickness;
                targetScale *= scaleMulitplier;

                Vector3 surfaceNormal = pose.rotation * Vector3.forward;
                Vector3 targetPos = pose.center + surfaceNormal * modelSurfaceOffset;

                posChanged = (targetPos - currentPos).sqrMagnitude > positionEpsilon * positionEpsilon;
                rotChanged = Quaternion.Angle(currentRot, targetRot) > rotationEpsilonDeg;
                scaleChanged = (targetScale - currentScale).sqrMagnitude > scaleEpsilon * scaleEpsilon;

                if (posChanged)
                {
                    vel = posVelocity[pose.id];
                    currentPos = Vector3.SmoothDamp(
                        currentPos,
                        targetPos,
                        ref vel,
                        positionSmoothTime,
                        positionMaxSpeed
                    );
                    posVelocity[pose.id] = vel;
                }

                if (rotChanged)
                {
                    maxStep = rotationDegPerSec * Time.deltaTime;
                    currentRot = Quaternion.RotateTowards(currentRot, targetRot, maxStep);
                }

                if (scaleChanged)
                {
                    svel = scaleVelocity[pose.id];
                    currentScale = Vector3.SmoothDamp(
                        currentScale,
                        targetScale,
                        ref svel,
                        scaleSmoothTime
                    );
                    scaleVelocity[pose.id] = svel;
                }

                go.transform.SetPositionAndRotation(currentPos, currentRot);
                go.transform.localScale = currentScale;
            }
            else
            {
                if (gameObjectsById.TryGetValue(pose.id, out go) && go != null)
                {
                    go.SetActive(false);
                }
            }

            for (int i = -10; i < 11; i++)
            {
                if (i == 0) continue;

                if (activeCards == null || !activeCards.Contains(i))
                {
                    if (gameObjectsById.TryGetValue(i.ToString(), out go2) && go2 != null)
                    {
                        go2.SetActive(false);
                    }
                }
            }
        }
    }

    private static string[] FileNamesFromId(string id)
    {
        return new[] { $"{id}.glb", $"{id}_out.glb" };
    }

    private void PreloadAllCubesExperimental()
    {
        if (!Directory.Exists(LocalGlbDirectory))
        {
            Directory.CreateDirectory(LocalGlbDirectory);
        }

        Debug.Log("[FloorCubeConsumerExperimental] persistentDataPath: " + Application.persistentDataPath);
        Debug.Log("[FloorCubeConsumerExperimental] Local GLB directory: " + LocalGlbDirectory);

        for (int i = -10; i < 11; i++)
        {
            if (i == 0)
            {
                continue;
            }

            string id = i.ToString();
            GameObject placeholder = CreateInvisiblePlaceholder(id);

            gameObjectsById[id] = placeholder;
            posVelocity[id] = Vector3.zero;
            scaleVelocity[id] = Vector3.zero;

            placeholder.SetActive(false);

            StartModelLoad(id);
        }
    }

    private void StartModelLoad(string id)
    {
        if (!Directory.Exists(LocalGlbDirectory))
            Directory.CreateDirectory(LocalGlbDirectory);

        Debug.Log($"[FloorCubeConsumerExperimental] Loading model {id} from {serverBaseUrl}");

        StartCoroutine(DownloadAndReplaceWhenAvailable(id));
    }

    private GameObject CreateInvisiblePlaceholder(string id)
    {
        GameObject placeholder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        placeholder.name = id;

        if (cubesParent)
        {
            placeholder.transform.SetParent(cubesParent, worldPositionStays: true);
        }

        if (setLayerAndTag)
        {
            placeholder.layer = cubeLayer;
            placeholder.tag = cubeTag;
        }

        Renderer renderer = placeholder.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        Collider collider = placeholder.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        return placeholder;
    }

    private IEnumerator DownloadAndReplaceWhenAvailable(string id)
    {
        if (currentlyDownloadingIds.Contains(id))
        {
            yield break;
        }

        currentlyDownloadingIds.Add(id);

        string[] fileNames = FileNamesFromId(id);

        while (!loadedIds.Contains(id) && !animatedIds.Contains(id))
        {
            foreach (string fileName in fileNames)
            {
                string localPath = Path.Combine(LocalGlbDirectory, fileName);
                string url = $"{serverBaseUrl}/files/download/{fileName}";
                bool shouldRetryAfterSaveError = false;

                using (UnityWebRequest req = UnityWebRequest.Get(url))
                {
                    yield return req.SendWebRequest();

                    if (req.result == UnityWebRequest.Result.Success)
                    {
                        try
                        {
                            File.WriteAllBytes(localPath, req.downloadHandler.data);
                            Debug.Log($"[FloorCubeConsumerExperimental] Downloaded {fileName} to {localPath}");
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[FloorCubeConsumerExperimental] Could not save {fileName}: {ex.Message}");
                            shouldRetryAfterSaveError = true;
                        }

                        if (shouldRetryAfterSaveError)
                        {
                            yield return new WaitForSeconds(downloadRetryDelaySeconds);
                            continue;
                        }

                        yield return StartCoroutine(ReplacePlaceholderWithGlbCoroutine(id, localPath));

                        StartCoroutine(DownloadAnimatedAndReplaceWhenAvailable(id));

                        currentlyDownloadingIds.Remove(id);
                        yield break;
                    }

                    if (req.responseCode == 404)
                    {
                        Debug.Log($"[FloorCubeConsumerExperimental] File not available yet: {fileName}");
                    }
                    else
                    {
                        Debug.LogWarning(
                            $"[FloorCubeConsumerExperimental] Download failed for {fileName}: {req.error}, code={req.responseCode}"
                        );
                    }
                }
            }

            yield return new WaitForSeconds(downloadRetryDelaySeconds);
        }

        currentlyDownloadingIds.Remove(id);
    }

    private IEnumerator DownloadAnimatedAndReplaceWhenAvailable(string id)
    {
        string fileName = $"{id}.gltf";
        string localPath = Path.Combine(LocalGlbDirectory, fileName);
        string url = $"{serverBaseUrl}/files/animated/{fileName}";

        while (true)
        {
            using (UnityWebRequest req = UnityWebRequest.Get(url))
            {
                yield return req.SendWebRequest();
                if (req.result == UnityWebRequest.Result.Success)
                {
                    File.WriteAllBytes(localPath, req.downloadHandler.data);
                    Debug.Log($"[FloorCubeConsumerExperimental] Downloaded animated {fileName} to {localPath}");
                    yield return StartCoroutine(ReplaceModelWithGltfCoroutine(id, localPath));
                    yield break;
                }

                if (req.responseCode != 404)
                    Debug.LogWarning($"[FloorCubeConsumerExperimental] Animated GLTF download failed for {fileName}: {req.error}");
            }
            yield return new WaitForSeconds(downloadRetryDelaySeconds);
        }
    }

    private IEnumerator ReplaceModelWithGltfCoroutine(string id, string path)
    {
        Task<(GameObject root, Animation animation)> task = LoadGltfFromFile(path);
        while (!task.IsCompleted) yield return null;
        if (task.IsFaulted || task.Result.root == null)
        {
            Debug.LogError($"[FloorCubeConsumerExperimental] Failed to load animated GLTF for {id}: {task.Exception}");
            yield break;
        }
        InstallLoadedModel(id, task.Result.root);
        Debug.Log($"[FloorCubeConsumerExperimental] Installed animated model for {id}");
    }

    private void InstallLoadedModel(string id, GameObject model)
    {
        animatedIds.Add(id);
        loadedIds.Add(id);
        model.name = id;
        if (cubesParent) model.transform.SetParent(cubesParent, true);
        if (setLayerAndTag) { model.layer = cubeLayer; model.tag = cubeTag; }
        ApplyRendererSettings(model);
        SetModelVisibility(model, !hideModels);

        if (gameObjectsById.TryGetValue(id, out GameObject oldModel) && oldModel != null)
        {
            model.transform.position = oldModel.transform.position;
            model.transform.rotation = oldModel.transform.rotation;
            model.transform.localScale = oldModel.transform.localScale;
            bool wasActive = oldModel.activeSelf;
            Destroy(oldModel);
            model.SetActive(wasActive);
        }
        gameObjectsById[id] = model;
    }

    private IEnumerator ReplacePlaceholderWithGlbCoroutine(string id, string path)
    {
        if (loadedIds.Contains(id))
        {
            yield break;
        }

        Task<bool> task = ReplacePlaceholderWithGlb(id, path);

        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsFaulted)
        {
            Debug.LogError($"[FloorCubeConsumerExperimental] Replace task failed for {id}: {task.Exception}");
        }
    }

    private async Task<bool> ReplacePlaceholderWithGlb(string id, string path)
    {
        if (loadedIds.Contains(id) || animatedIds.Contains(id))
        {
            return true;
        }

        var result = await LoadGltfBinaryFromMemory(path);
        GameObject cube = result.root;
        Animation animation = result.animation;

        if (cube == null)
        {
            Debug.LogError("[FloorCubeConsumerExperimental] Failed to load GLB " + path);
            return false;
        }

        Debug.Log("[FloorCubeConsumerExperimental] Cube " + id + " wurde aus GLB erstellt.");

        if (animation != null)
        {
            foreach (AnimationState state in animation)
            {
                Debug.Log("[FloorCubeConsumerExperimental] GLB Clip: " + state.name);
            }
        }

        cube.name = id;

        if (cubesParent)
        {
            cube.transform.SetParent(cubesParent, worldPositionStays: true);
        }

        if (setLayerAndTag)
        {
            cube.layer = cubeLayer;
            cube.tag = cubeTag;
        }

        ApplyRendererSettings(cube);
        SetModelVisibility(cube, !hideModels);
        Debug.Log($"[FloorCubeConsumerExperimental] Installed server static model {id} with {cube.GetComponentsInChildren<Renderer>(true).Length} renderers, visible={!hideModels}");

        if (gameObjectsById.TryGetValue(id, out GameObject oldPlaceholder) && oldPlaceholder != null)
        {
            cube.transform.position = oldPlaceholder.transform.position;
            cube.transform.rotation = oldPlaceholder.transform.rotation;
            cube.transform.localScale = oldPlaceholder.transform.localScale;

            bool wasActive = oldPlaceholder.activeSelf;

            Destroy(oldPlaceholder);

            cube.SetActive(wasActive);
        }
        else
        {
            cube.SetActive(false);
        }

        gameObjectsById[id] = cube;
        loadedIds.Add(id);

        return true;
    }

    private void ApplyRendererSettings(GameObject cube)
    {
        var renderers = cube.GetComponentsInChildren<Renderer>(true);

        foreach (var rend in renderers)
        {
            var urpUnlit = Shader.Find("Universal Render Pipeline/Unlit");

            if (urpUnlit != null)
            {
                var mat = new Material(urpUnlit);

                if (mat.HasProperty("_BaseColor"))
                {
                    mat.SetColor("_BaseColor", cubeColor);
                }

                rend.material = mat;
            }
            else
            {
                var mat = rend.material;

                if (mat != null)
                {
                    if (mat.HasProperty("_BaseColor"))
                    {
                        mat.SetColor("_BaseColor", cubeColor);
                    }
                    else if (mat.HasProperty("_Color"))
                    {
                        mat.SetColor("_Color", cubeColor);
                    }
                }
            }

            rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            rend.receiveShadows = false;

            if (hideModels)
            {
                rend.enabled = false;
            }
        }
    }

    private void SetModelVisibility(GameObject model, bool visible)
    {
        if (model == null)
        {
            return;
        }

        foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>(true))
        {
            renderer.enabled = visible;
        }
    }

    private async Task<(GameObject root, Animation animation)> LoadGltfBinaryFromMemory(string filePath)
    {
        byte[] data = File.ReadAllBytes(filePath);
        var gltf = new GltfImport();

        var importSettings = new ImportSettings
        {
            AnimationMethod = AnimationMethod.Legacy
        };

        bool success = await gltf.LoadGltfBinary(
            data,
            new Uri(filePath),
            importSettings: importSettings
        );

        if (!success)
        {
            return (null, null);
        }

        var root = new GameObject(Path.GetFileNameWithoutExtension(filePath));
        var instantiator = new GameObjectInstantiator(gltf, root.transform);

        success = await gltf.InstantiateMainSceneAsync(instantiator);

        if (!success)
        {
            Destroy(root);
            return (null, null);
        }

        var animation = instantiator.SceneInstance?.LegacyAnimation;
        return (root, animation);
    }

    private async Task<(GameObject root, Animation animation)> LoadGltfFromFile(string filePath)
    {
        var gltf = new GltfImport();
        var importSettings = new ImportSettings { AnimationMethod = AnimationMethod.Legacy };
        bool success = await gltf.Load(filePath, importSettings);
        if (!success) return (null, null);
        var root = new GameObject(Path.GetFileNameWithoutExtension(filePath));
        var instantiator = new GameObjectInstantiator(gltf, root.transform);
        success = await gltf.InstantiateMainSceneAsync(instantiator);
        if (!success) { Destroy(root); return (null, null); }
        return (root, instantiator.SceneInstance?.LegacyAnimation);
    }

    public Dictionary<string, GameObject> GetGameObjectsByID()
    {
        return gameObjectsById;
    }

    private void OnDrawGizmos()
    {
        if (qrCodeDisplayManager == null || qrCodeDisplayManager.objectPoses == null) return;

        Gizmos.color = Color.green;

        foreach (var p in qrCodeDisplayManager.objectPoses)
        {
            Gizmos.DrawWireSphere(p.center, 0.05f);
            Gizmos.DrawRay(p.center, p.rotation * Vector3.forward * 0.2f);
        }
    }
}
