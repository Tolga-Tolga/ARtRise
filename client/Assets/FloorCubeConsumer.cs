using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
// using Siccity.GLTFUtility;
using System.IO;
using GLTFast;
using System;
using System.Threading.Tasks;


public class FloorCubeConsumer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QrCodeDisplayManager qrCodeDisplayManager;
    [Tooltip("Optional: Parent für erzeugte Cubes (für saubere Hierarchie).")]
    public Transform cubesParent;

    [Header("Smoothing")]
    [Tooltip("Kleinere Werte = weicher (langsamer).")]
    public float positionSmoothTime = 0.10f;
    public float positionMaxSpeed = 6f;
    [Tooltip("Grad pro Sekunde für weiches Rotieren.")]
    public float rotationDegPerSec = 360f;
    [Tooltip("Kleinere Werte = weicher (langsamer).")]
    public float scaleSmoothTime = 0.10f;

    [Header("Thresholds (Jitter-Filter)")]
    public float positionEpsilon = 0.01f;   // 1 cm
    public float rotationEpsilonDeg = 1.5f; // 1.5°
    public float scaleEpsilon = 0.01f;      // 1 cm

    [Header("Depth & Limits")]
    [Tooltip("Z-Dicke der Cubes (Meter).")]
    public float depthThickness = 0.02f;  // 2 cm
    [Tooltip("Clamp für XY-Skalen (Meter).")]
    public Vector2 xyClamp = new Vector2(0.05f, 1.5f);

    [Header("Misc")]
    public bool snapOnCreate = true;
    public bool setLayerAndTag = false;
    public int cubeLayer = 0;
    public string cubeTag = "Untagged";
    public Color cubeColor = Color.cyan;
    public string scannerObjectName = "QrCodeScanner";
    public bool hideModels = false;
    [Header("Other")]
    public float scaleMulitplier;

    private readonly Dictionary<string, GameObject> gameObjectsById = new();
    private readonly Dictionary<string, Vector3> posVelocity = new();
    private readonly Dictionary<string, Vector3> scaleVelocity = new();
    private GLBFactory factory;

    public VRState vrState;

    private bool isExperimentalMode;

    public GameManager gameManager;


    public List<int> activeCards;

    [Header("Model Placement")]
    [SerializeField] private float modelSurfaceOffset = 0.05f;


    private void OnEnable()
    {
        if (qrCodeDisplayManager == null)
        {
            var go = GameObject.Find(scannerObjectName);
            if (go != null) qrCodeDisplayManager = go.GetComponent<QrCodeDisplayManager>();
        }
        factory = FindFirstObjectByType<GLBFactory>();
        isExperimentalMode = vrState.experimentalMode;
        activeCards = gameManager.GetActiveCards();
        if (!isExperimentalMode)
        {
            PreloadAllCubes();
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

    private void OnDisable() => CleanupAllCubes();

    private void CleanupAllCubes()
    {
        foreach (var kv in gameObjectsById)
            if (kv.Value) Destroy(kv.Value);

        gameObjectsById.Clear();
        posVelocity.Clear();
        scaleVelocity.Clear();
    }


    GameObject go;
    GameObject go2;
    bool posChanged;
    bool rotChanged;
    bool scaleChanged;
    Vector3 currentPos;
    Quaternion currentRot;
    Quaternion targetRot;
    Vector3 currentScale;
    Vector3 vel;
    Vector3 targetScale;
    float maxStep;
    Vector3 svel;
    int poseid;
    private void UpdateCubesSmooth(List<QrCodeDisplayManager.MarkerPose> poses)
    {
        // 1) Aufräumen
        if (gameManager.activeCardsListBlocked)
        {
            return;
        }
        foreach (var pose in poses)
        {
            int.TryParse(pose.id, out poseid);
            if (activeCards.Contains(poseid))
            {
                if (gameObjectsById.TryGetValue(poseid.ToString(), out go) && go != null)
                go.SetActive(true);
                // aktuelle Werte
                currentPos = go.transform.position;
                currentRot = go.transform.rotation;
                currentScale = go.transform.localScale;

                // Zielrotation mit Korrektur
                targetRot = pose.rotation * Quaternion.Euler(270f, 0f, 180f);

                if (poseid == 2 || poseid == -2)
                {
                    targetRot = targetRot * Quaternion.Euler(0f, 180f, 0f);
                }

                // Ziel-Scale mit Clamp + fixer Z-Dicke
                targetScale = pose.scale;
                targetScale.x = Mathf.Clamp(targetScale.x, xyClamp.x, xyClamp.y);
                targetScale.y = Mathf.Clamp(targetScale.y, xyClamp.x, xyClamp.y);
                targetScale.z = depthThickness;

                targetScale *= scaleMulitplier;

                // Deadzone-Checks
                Vector3 surfaceNormal = pose.rotation * Vector3.forward;
                Vector3 targetPos = pose.center + surfaceNormal * modelSurfaceOffset;

                posChanged = (targetPos - currentPos).sqrMagnitude > positionEpsilon * positionEpsilon;
                // posChanged = (pose.center - currentPos).sqrMagnitude > positionEpsilon * positionEpsilon;
                rotChanged = Quaternion.Angle(currentRot, targetRot) > rotationEpsilonDeg;
                scaleChanged = (targetScale - currentScale).sqrMagnitude > scaleEpsilon * scaleEpsilon;

                // Smooth Position
                if (posChanged)
                {
                    vel = posVelocity[pose.id];
                    // currentPos = Vector3.SmoothDamp(currentPos, pose.center, ref vel, positionSmoothTime, positionMaxSpeed);
                    currentPos = Vector3.SmoothDamp(currentPos, targetPos, ref vel, positionSmoothTime, positionMaxSpeed);
                    posVelocity[pose.id] = vel;
                }

                // Smooth Rotation (ruhiger, aber begrenzt)
                if (rotChanged)
                {
                    maxStep = rotationDegPerSec * Time.deltaTime;
                    currentRot = Quaternion.RotateTowards(currentRot, targetRot, maxStep);
                }

                // Smooth Scale
                if (scaleChanged)
                {
                    svel = scaleVelocity[pose.id];
                    currentScale = Vector3.SmoothDamp(currentScale, targetScale, ref svel, scaleSmoothTime);
                    scaleVelocity[pose.id] = svel;
                }

                go.transform.SetPositionAndRotation(currentPos, currentRot);
                go.transform.localScale = currentScale;
            }
            else
            {
                if (gameObjectsById.TryGetValue(pose.id, out go) && go != null)
                go.SetActive(false);
            }
            for (int i = -10; i < 11; i++)
            {
                if(i==0) continue;
                if (!activeCards.Contains(i))
                {
                    if (gameObjectsById.TryGetValue(i.ToString(), out go2) && go2 != null)
                    {
                        go2.SetActive(false);
                    }
                }
            }
        }
    }
    private static string FileNameFromId(string id)
    {
        if (int.TryParse(id, out var n) && n < 0)
            return $"neg{Mathf.Abs(n)}.glb";
        return $"{id}.glb";
    }

    // Debug-Gizmos (optional)
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

    public Dictionary<string, GameObject> GetGameObjectsByID()
    {
        return gameObjectsById;
    }



    // GameObject cube;
    // private void PreloadAllCubes()
    // {
    //     for (int i = -10; i < 11; i++)
    //     {
    //         if (i == 0)
    //         {
    //             continue;
    //         }
    //         var fileName = FileNameFromId(i.ToString());
    //         var path = Path.Combine(Application.persistentDataPath, "gameobjects/preloaded", fileName);
    //         if (File.Exists(path))
    //         {
    //             cube = Importer.LoadFromFile(path);
    //         }
    //         else
    //         {
    //             Debug.LogError($"GLB nicht gefunden unter {path}. Wurde es vorher kopiert?");
    //             continue;
    //         }
    //         Debug.Log("[cube] Cube " + i + " wurde erstellt!");
    //         var animator = cube.GetComponentInChildren<Animator>();
    //         var animation = cube.GetComponentInChildren<Animation>();

    //         Debug.Log("[GLB] Animator found: " + (animator != null));
    //         Debug.Log("[GLB] Animation found: " + (animation != null));

    //         if (animator != null)
    //         {
    //             Debug.Log("[GLB] runtimeAnimatorController: " + animator.runtimeAnimatorController);
    //             Debug.Log("[GLB] avatar: " + animator.avatar);
    //         }
    //         cube.name = i.ToString();
    //         if (cubesParent)
    //         cube.transform.SetParent(cubesParent, worldPositionStays: true);
    //         if (setLayerAndTag)
    //         {
    //             cube.layer = cubeLayer;
    //             cube.tag = cubeTag;
    //         }
    //         var rend = cube.GetComponent<Renderer>();
    //         if (rend != null)
    //         {
    //             // robustes Material-Setup (URP/Built-in)
    //             var urpUnlit = Shader.Find("Universal Render Pipeline/Unlit");
    //             if (urpUnlit != null)
    //             {
    //                 var mat = new Material(urpUnlit);
    //                 if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", cubeColor);
    //                 rend.material = mat;
    //             }
    //             else
    //             {
    //                 var mat = rend.material;
    //                 if (mat != null)
    //                 {
    //                     if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", cubeColor);
    //                     else if (mat.HasProperty("_Color")) mat.SetColor("_Color", cubeColor);
    //                 }
    //             }
    //             rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    //             rend.receiveShadows = false;
    //         }
    //         gameObjectsById[i.ToString()] = cube;
    //         posVelocity[i.ToString()] = Vector3.zero;
    //         scaleVelocity[i.ToString()] = Vector3.zero;
            
    //         // if (snapOnCreate)
    //         // {
    //         //     cube.transform.SetPositionAndRotation(pose.center, pose.rotation);
    //         //     var snapped = pose.scale;
    //         //     snapped.x = Mathf.Clamp(snapped.x, xyClamp.x, xyClamp.y);
    //         //     snapped.y = Mathf.Clamp(snapped.y, xyClamp.x, xyClamp.y);
    //         //     snapped.z = depthThickness; // <- fixe Dicke
    //         //     cube.transform.localScale = snapped;
    //         //     continue;
    //         // }
    //         cube.SetActive(false);
    //     }                  
    // }

    
    private async void PreloadAllCubes()
    {
        for (int i = -10; i < 11; i++)
        {
            if (i == 0)
            {
                continue;
            }
            var fileName = FileNameFromId(i.ToString());
            var path = Path.Combine(Application.persistentDataPath, "gameobjects/preloaded", fileName);

            if (!File.Exists(path))
            {
                Debug.LogError($"[GLB] GLB nicht gefunden unter {path}. Wurde es vorher kopiert?");
                continue;
            }

            var (cube, animation) = await LoadGltfBinaryFromMemory(path);
            if (cube == null)
            {
                Debug.LogError("[GLB] Laden fehlgeschlagen für " + path);
                continue;
            }
            Debug.Log("[cube] Cube " + i + " wurde erstellt!");
            if (animation != null)
            {
                foreach (AnimationState state in animation)
                {
                    Debug.Log("[GLB] Clip: " + state.name);
                }
            }

            cube.name = i.ToString();
            if (cubesParent)
            cube.transform.SetParent(cubesParent, worldPositionStays: true);
            if (setLayerAndTag)
            {
                cube.layer = cubeLayer;
                cube.tag = cubeTag;
            }
            // var rend = cube.GetComponent<Renderer>();
            var renderers = cube.GetComponentsInChildren<Renderer>(true);
            foreach (var rend in renderers)
            {
                var urpUnlit = Shader.Find("Universal Render Pipeline/Unlit");
                if (urpUnlit != null)
                {
                    var mat = new Material(urpUnlit);
                    if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", cubeColor);
                    rend.material = mat;
                }
                else
                {
                    var mat = rend.material;
                    if (mat != null)
                    {
                        if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", cubeColor);
                        else if (mat.HasProperty("_Color")) mat.SetColor("_Color", cubeColor);
                    }
                }

                rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                rend.receiveShadows = false;
                if(hideModels) rend.enabled = false;
            }
            gameObjectsById[i.ToString()] = cube;
            posVelocity[i.ToString()] = Vector3.zero;
            scaleVelocity[i.ToString()] = Vector3.zero;
            
            // if (snapOnCreate)
            // {
            //     cube.transform.SetPositionAndRotation(pose.center, pose.rotation);
            //     var snapped = pose.scale;
            //     snapped.x = Mathf.Clamp(snapped.x, xyClamp.x, xyClamp.y);
            //     snapped.y = Mathf.Clamp(snapped.y, xyClamp.x, xyClamp.y);
            //     snapped.z = depthThickness; // <- fixe Dicke
            //     cube.transform.localScale = snapped;
            //     continue;
            // }
            cube.SetActive(false);
        }                  
    }

    async Task<(GameObject root, Animation animation)> LoadGltfBinaryFromMemory(string filePath) {
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

        if (!success) return (null,null);

        var root = new GameObject(Path.GetFileNameWithoutExtension(filePath));
        var instantiator = new GameObjectInstantiator(gltf, root.transform);

        success = await gltf.InstantiateMainSceneAsync(instantiator);
        if (!success) {
            UnityEngine.Object.Destroy(root);
            return (null, null);
        }
        var animation = instantiator.SceneInstance?.LegacyAnimation;
        return (root, animation);
    }

}
