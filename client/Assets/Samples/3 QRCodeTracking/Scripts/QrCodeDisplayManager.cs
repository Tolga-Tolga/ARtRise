using System.Collections.Generic;
using UnityEngine;
using Meta.XR;
using System.Threading.Tasks;
using System;

public class QrCodeDisplayManager : MonoBehaviour
{
#if ZXING_ENABLED
    private QrCodeScanner _scanner;
    private EnvironmentRaycastManager _envRaycastManager;
    private readonly Dictionary<string, MarkerController> _activeMarkers = new();

    private enum QrRaycastMode
    {
        CenterOnly,
        PerCorner
    }

    public struct MarkerPose
    {
        public string id;
        public Vector3 center;
        public Quaternion rotation;
        public Vector3 scale;
    }

    public List<MarkerPose> objectPoses { get; private set; } = new List<MarkerPose>();
    public bool isBlocked { get; private set; } = false;

    private Task _scanTask;
    private readonly List<MarkerPose> _tmp = new List<MarkerPose>(32);

    private Dictionary<string, Tuple<List<Texture2D>, bool>> idsWithPicture =
        new Dictionary<string, Tuple<List<Texture2D>, bool>>();

    private ModelPipelineCustom _pipeline;

    [SerializeField] private QrRaycastMode raycastMode = QrRaycastMode.PerCorner;

    [Tooltip("Experimental Mode or Game Mode")]
    [SerializeField] private VRState state;
    private bool _experimentalMode;

    private void Awake()
    {
        _scanner = GetComponent<QrCodeScanner>();
        _envRaycastManager = GetComponent<EnvironmentRaycastManager>();
        _pipeline = GetComponent<ModelPipelineCustom>();
        _experimentalMode = state.experimentalMode;
        Debug.Log($"[DISPLAY] Awake(): scanner={_scanner}, envRaycast={_envRaycastManager}, pipeline={_pipeline}");
    }

    int timer = 0;
    private void Update()
    { 
        timer++;
        if (_scanTask == null || _scanTask.IsCompleted)
        {
            if(timer == 30){
                _scanTask = RefreshMarkers();
                timer = 0;
            }
            
        }
    }

    public bool HasQrAlreadyPictures(string id)
    {
        if (!idsWithPicture.TryGetValue(id, out var entry))
            return false;

        return entry.Item2; // uploaded flag
    }

    private async Task RefreshMarkers()
    {
        // Debug.Log("[DISPLAY] RefreshMarkers() START");
        if (!_envRaycastManager || !_scanner)
        {
            // Debug.LogWarning($"[DISPLAY] Missing components: envRaycast={_envRaycastManager}, scanner={_scanner}");
            return;
        }

        // Debug.Log("[DISPLAY] RefreshMarkers(): scanning...");
        isBlocked = true;

        var qrResultsTuple = await _scanner.ScanFrameAsync();
        // Debug.Log("[DISPLAY] AFTER AWAIT ScanFrameAsync()");
        int count = qrResultsTuple == null ? -1 : qrResultsTuple.Length;
        // Debug.Log($"[DISPLAY] ScanFrameAsync() returned {count} result(s)");

        if (qrResultsTuple == null || qrResultsTuple.Length == 0)
        {
            // Debug.Log("[DISPLAY] No QR codes detected → cleanup + exit");
            CleanupInactiveMarkers();
            isBlocked = false;
            return;
        }

        foreach (var tuple in qrResultsTuple)
        {
            var qr = tuple.Item1;
            var artwork = tuple.Item2;

            // Debug.Log($"[DISPLAY] Handling QR: text=\"{qr.text}\" corners={qr.corners?.Length ?? 0}");

            if (qr.corners != null)
            {
                for (int i = 0; i < qr.corners.Length; i++)
                {
                    var c = qr.corners[i];
                    // Debug.Log($"[DISPLAY]   corner[{i}] = ({c.x:F3}, {c.y:F3})");
                }
            }

            if (!TryBuildMarkerPose(qr, out Pose pose, out Vector3 scale, qr.text))
            {
                // Debug.LogWarning($"[DISPLAY] TryBuildMarkerPose FAILED for id=\"{qr.text}\"");
                continue;
            }

            // Debug.Log($"[DISPLAY] Pose SUCCESS for \"{qr.text}\" → pos={pose.position}, rot={pose.rotation.eulerAngles}, scale={scale}");

            // var marker = GetOrCreateMarker(qr.text);
            // if (!marker)
            // {
            //     // Debug.LogWarning($"[DISPLAY] GetOrCreateMarker FAILED for id=\"{qr.text}\" (pool empty?)");
            //     continue;
            // }

            // Debug.Log($"[DISPLAY] Updating marker for id=\"{qr.text}\"");
            // marker.UpdateMarker(pose.position, pose.rotation, scale, qr.text);
            if (_experimentalMode)
            {
               addPictureToMarkers(qr.text, artwork); 
            }
        }

        CleanupInactiveMarkers();

        objectPoses.Clear();
        objectPoses.AddRange(_tmp);
        _tmp.Clear();

        // Debug.Log($"[DISPLAY] Refresh complete → objectPoses={objectPoses.Count}, activeMarkers={_activeMarkers.Count}");

        isBlocked = false;
    }

    private void addPictureToMarkers(string id, Texture2D tex)
    {
        string texInfo = tex ? $"{tex.width}x{tex.height}" : "NULL";
        // Debug.Log($"[DISPLAY] addPictureToMarkers: id={id}, tex={texInfo}");

        // Eintrag holen oder neu erstellen
        if (!idsWithPicture.TryGetValue(id, out var entry))
        {
            entry = Tuple.Create(new List<Texture2D>(), false);
            idsWithPicture[id] = entry;

            // Debug.Log("[DISPLAY] Created new texture list for id=" + id);
        }

        var list = entry.Item1;
        var uploaded = entry.Item2;

        // Sobald eine ID einmal hochgeladen wurde → nie wieder Bilder speichern
        if (uploaded)
        {
            // Debug.Log($"[DISPLAY] id={id} already uploaded → ignore further textures");
            return;
        }

        // Null-Texture ignorieren (kann vorkommen)
        if (tex == null)
        {
            // Debug.Log($"[DISPLAY] id={id} tex is NULL → skip");
            return;
        }
        
        list.Add(tex);
        // Debug.Log($"[DISPLAY] Added texture #{list.Count} for id={id}");

        if (list.Count < 6)
            return;

        // Debug.Log($"[DISPLAY] Upload triggered for id={id} with {list.Count} images");

        // ENDSTATUS → nie wieder Bilder sammeln
        idsWithPicture[id] = Tuple.Create(list, true);
        uploadPicturesToPipeline(id, list);

        // Speicher aufräumen
        // foreach (var t in list)
        // {
        //     if (t != null) UnityEngine.Object.Destroy(t);
        // }
        list.Clear();


        // Debug.Log($"[DISPLAY] Upload complete for id={id}. Future captures will be ignored.");
    }



    private void uploadPicturesToPipeline(string id, List<Texture2D> pictures)
    {
        // Debug.Log($"[DISPLAY] UploadPicturesToPipeline: id={id}, count={pictures.Count}");
        // EXACTLY ONE CALL !!!
        
        _pipeline.uploadTextures(id, pictures.ToArray());
    }

#else
    public struct MarkerPose { public string id; public Vector3 center; public Quaternion rotation; public Vector3 scale; }
    public List<MarkerPose> objectPoses { get; private set; } = new List<MarkerPose>();
    public bool isBlocked { get; private set; } = false;
#endif

    private static Vector2 ToViewport(Vector2 uv) =>
        new(Mathf.Clamp01(uv.x), Mathf.Clamp01(uv.y));

    private static Ray BuildWorldRay(QrCodeResult result, Vector2 uv)
    {
        // Debug.Log($"[POSE] BuildWorldRay: uv=({uv.x:F3},{uv.y:F3})");

        var viewport = ToViewport(uv);
        var intr = result.Intrinsics;

        var sensorRes = (Vector2)intr.SensorResolution;
        var currRes = (Vector2)result.captureResolution;

        if (currRes == Vector2.zero)
            currRes = sensorRes;

        var crop = ComputeSensorCrop(sensorRes, currRes);

        var sensorPoint = new Vector2(
            crop.x + crop.width * viewport.x,
            crop.y + crop.height * viewport.y);

        var localDir = new Vector3(
            (sensorPoint.x - intr.PrincipalPoint.x) / intr.FocalLength.x,
            (sensorPoint.y - intr.PrincipalPoint.y) / intr.FocalLength.y,
            1f).normalized;

        var worldDir = result.cameraPose.rotation * localDir;

        return new Ray(result.cameraPose.position, worldDir);
    }

    private static Rect ComputeSensorCrop(Vector2 sensorResolution, Vector2 currentResolution)
    {
        if (sensorResolution == Vector2.zero)
            return new Rect(0, 0, currentResolution.x, currentResolution.y);

        var scale = new Vector2(
            currentResolution.x / sensorResolution.x,
            currentResolution.y / sensorResolution.y);

        float maxScale = Mathf.Max(scale.x, scale.y);
        if (maxScale <= 0) maxScale = 1f;
        scale /= maxScale;

        return new Rect(
            sensorResolution.x * (1f - scale.x) * 0.5f,
            sensorResolution.y * (1f - scale.y) * 0.5f,
            sensorResolution.x * scale.x,
            sensorResolution.y * scale.y);
    }

    private static Vector3 ProjectOntoPlane(Plane plane, Ray ray, float fallbackDistance)
    {
        return plane.Raycast(ray, out var d)
            ? ray.GetPoint(d)
            : ray.GetPoint(fallbackDistance);
    }

    private bool TryBuildMarkerPose(QrCodeResult result, out Pose pose, out Vector3 scale, string id)
    {
        // Debug.Log($"[POSE] TryBuildMarkerPose for id=\"{id}\"");

        pose = default;
        scale = default;

        if (result?.corners == null || result.corners.Length < 4)
        {
            // Debug.LogWarning("[POSE] Not enough corners");
            return false;
        }

        // new
        // Vector2 TL = ToPixel(result.corners[0], result.captureResolution);
        // Vector2 TR = ToPixel(result.corners[1], result.captureResolution);
        // Vector2 BL = ToPixel(result.corners[2], result.captureResolution);
        // Vector2 BR = ToPixel(result.corners[3], result.captureResolution);
        // Vector2[] uvsRaw = { TL, TR, BR, BL };
        // Vector2[] uvs = RotateCorners(uvsRaw);
        // uvs[1] = uvs[2] + uvs[0] - uvs[3];

        //old

        int count = result.corners.Length;
        var uvs = new Vector2[count];

        for (int i = 0; i < count; i++)
        {
            uvs[i] = new Vector2(result.corners[i].x, result.corners[i].y);
            // Debug.Log($"[POSE]   UV[{i}] = ({uvs[i].x:F3},{uvs[i].y:F3})");
        }

        var centerUV = Vector2.zero;
        foreach (var uv in uvs) centerUV += uv;
        centerUV /= count;

        Debug.Log($"[POSE] centerUV = {centerUV}");

        var centerRay = BuildWorldRay(result, centerUV);

        if (!_envRaycastManager.Raycast(centerRay, out var centerHit))
        {
            // Debug.LogWarning("[POSE] Center raycast FAILED");
            return false;
        }

        var center = centerHit.point;
        var distance = Vector3.Distance(centerRay.origin, center);
        var plane = new Plane(centerHit.normal, centerHit.point);

        // Debug.Log($"[POSE] CenterHit = {center}, planeNormal={centerHit.normal}");

        var worldCorners = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            var ray = BuildWorldRay(result, uvs[i]);

            if (raycastMode == QrRaycastMode.PerCorner &&
                _envRaycastManager.Raycast(ray, out var cornerHit))
            {
                worldCorners[i] = cornerHit.point;
                // Debug.Log($"[POSE] Corner[{i}] RAYCAST hit {worldCorners[i]}");
            }
            else
            {
                worldCorners[i] = ProjectOntoPlane(plane, ray, distance);
                // Debug.Log($"[POSE] Corner[{i}] projected {worldCorners[i]}");
            }
        }

        center = Vector3.zero;
        foreach (var c in worldCorners) center += c;
        center /= count;

        var diagA = (worldCorners[2] - worldCorners[0]).normalized;
        var diagB = (worldCorners[3] - worldCorners[1]).normalized;
        var normal = Vector3.Cross(diagA, diagB).normalized;

        // Debug.Log($"[POSE] diagA={diagA}, diagB={diagB}, normal={normal}");

        if (normal == Vector3.zero)
        {
            // Debug.LogWarning("[POSE] degenerate normal");
            return false;
        }

        var rotation = Quaternion.LookRotation(normal, diagA);

        float width = Vector3.Distance(worldCorners[0], worldCorners[1]);
        float height = Vector3.Distance(worldCorners[0], worldCorners[3]);

        scale = new Vector3(width, height, 0.02f);
        pose = new Pose(center, rotation);

        // Debug.Log($"[POSE] SUCCESS pos={center}, size=({width:F3},{height:F3})");

        var spl = id.Split("/");
        id = spl[spl.Length - 1];

        _tmp.Add(new MarkerPose
        {
            id = id,
            center = center,
            rotation = rotation,
            scale = scale
        });

        return true;
    }

    Vector2[] RotateCorners(Vector2[] points)
    {
        // if (points == null || points.Length != 4)
            // throw new ArgumentException("RotateCorners expects exactly 4 points");
        // first sort to top and bottom corners
        Vector2[] corners = SortVector2Y((Vector2[])points.Clone());

        // afterwars decide the corners
        Vector2 TL = corners[0].x < corners[1].x ? corners[0] : corners[1];
        Vector2 TR = corners[0].x > corners[1].x ? corners[0] : corners[1];
        Vector2 BL = corners[2].x < corners[3].x ? corners[2] : corners[3];
        Vector2 BR = corners[2].x > corners[3].x ? corners[2] : corners[3];
        return new Vector2[]{BL, BR, TR, TL};
    }

    Vector2[] SortVector2Y(Vector2[] points)
    {
        bool sorted;
        do
        {
            sorted = true;

            for (int i = 1; i < points.Length; i++)
            {
                if (points[i - 1].y < points[i].y)
                {
                    var temp = points[i];
                    points[i] = points[i - 1];
                    points[i - 1] = temp;
                    sorted = false;
                }
            }
        }
        while (!sorted);
        return points;
    }

    private Vector2 ToPixel(Vector3 uv, Vector2Int res)
    {
        return new Vector2(uv.x * res.x, uv.y * res.y);
    }

    private MarkerController GetOrCreateMarker(string key)
    {
        if (_activeMarkers.TryGetValue(key, out var marker))
        {
            // Debug.Log($"[DISPLAY] Found existing marker for id={key}");
            return marker;
        }

        // Debug.Log($"[DISPLAY] Creating new marker for id={key}");

        var go = MarkerPool.Instance ? MarkerPool.Instance.GetMarker() : null;
        if (!go)
        {
            // Debug.LogWarning("[DISPLAY] MarkerPool returned NULL!");
            return null;
        }

        marker = go.GetComponent<MarkerController>();
        if (!marker)
        {
            // Debug.LogWarning("[DISPLAY] Marker prefab has no MarkerController!");
            return null;
        }

        _activeMarkers[key] = marker;
        return marker;
    }

    private void CleanupInactiveMarkers()
    {
        var toRemove = new List<string>();

        foreach (var kvp in _activeMarkers)
        {
            if (!kvp.Value || !kvp.Value.gameObject.activeSelf)
            {
                // Debug.Log($"[DISPLAY] Removing inactive marker id={kvp.Key}");
                toRemove.Add(kvp.Key);
            }
        }

        foreach (var key in toRemove)
            _activeMarkers.Remove(key);
    }
}
