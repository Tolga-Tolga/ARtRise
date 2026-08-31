using UnityEngine;
using Oculus.Interaction;

public class MetaFocusEventFinder : MonoBehaviour
{
    private PokeInteractable[] pokeInteractables;
    private PointableCanvas[] pointableCanvases;
    private PointableCanvasMesh[] pointableCanvasMeshes;

    private void Start()
    {
        pokeInteractables = FindObjectsOfType<PokeInteractable>(true);
        pointableCanvases = FindObjectsOfType<PointableCanvas>(true);
        pointableCanvasMeshes = FindObjectsOfType<PointableCanvasMesh>(true);

        Debug.Log($"[UI-DEBUG] Found PokeInteractables: {pokeInteractables.Length}");
        Debug.Log($"[UI-DEBUG] Found PointableCanvases: {pointableCanvases.Length}");
        Debug.Log($"[UI-DEBUG] Found PointableCanvasMeshes: {pointableCanvasMeshes.Length}");

        foreach (var p in pokeInteractables)
        {
            Debug.Log($"[UI-DEBUG] PokeInteractable: {GetPath(p.transform)}", p);
        }

        foreach (var c in pointableCanvases)
        {
            Debug.Log($"[UI-DEBUG] PointableCanvas: {GetPath(c.transform)}", c);
        }

        foreach (var m in pointableCanvasMeshes)
        {
            Debug.Log($"[UI-DEBUG] PointableCanvasMesh: {GetPath(m.transform)}", m);
        }
    }

    private string GetPath(Transform t)
    {
        string path = t.name;

        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }

        return path;
    }
}