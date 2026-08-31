using UnityEngine;
using Oculus.Interaction;

public class HPBarFocusEventDebug : MonoBehaviour
{
    private PokeInteractable pokeInteractable;
    private PointableCanvas pointableCanvas;
    private PointableCanvasMesh pointableCanvasMesh;

    private void Awake()
    {
        pokeInteractable = GetComponent<PokeInteractable>();
        pointableCanvas = GetComponent<PointableCanvas>();
        pointableCanvasMesh = GetComponent<PointableCanvasMesh>();

        Debug.Log($"[HPBAR-DEBUG] {name} has PokeInteractable: {pokeInteractable != null}");
        Debug.Log($"[HPBAR-DEBUG] {name} has PointableCanvas: {pointableCanvas != null}");
        Debug.Log($"[HPBAR-DEBUG] {name} has PointableCanvasMesh: {pointableCanvasMesh != null}");
    }

    private void OnEnable()
    {
        if (pokeInteractable != null)
        {
            pokeInteractable.WhenStateChanged += OnPokeStateChanged;
            Debug.Log($"[HPBAR-DEBUG] Listening PokeInteractable on {GetPath(transform)}");
        }

        if (pointableCanvas != null)
        {
            pointableCanvas.WhenPointerEventRaised += OnPointableCanvasPointerEvent;
            Debug.Log($"[HPBAR-DEBUG] Listening PointableCanvas on {GetPath(transform)}");
        }

        if (pointableCanvasMesh != null)
        {
            pointableCanvasMesh.WhenPointerEventRaised += OnPointableCanvasMeshPointerEvent;
            Debug.Log($"[HPBAR-DEBUG] Listening PointableCanvasMesh on {GetPath(transform)}");
        }
    }

    private void OnDisable()
    {
        if (pokeInteractable != null)
            pokeInteractable.WhenStateChanged -= OnPokeStateChanged;

        if (pointableCanvas != null)
            pointableCanvas.WhenPointerEventRaised -= OnPointableCanvasPointerEvent;

        if (pointableCanvasMesh != null)
            pointableCanvasMesh.WhenPointerEventRaised -= OnPointableCanvasMeshPointerEvent;
    }

    private void OnPokeStateChanged(InteractableStateChangeArgs args)
    {
        Debug.Log($"[HPBAR-DEBUG] {GetPath(transform)} PokeInteractable state: {args.PreviousState} -> {args.NewState}", this);
    }

    private void OnPointableCanvasPointerEvent(PointerEvent evt)
    {
        Debug.Log($"[HPBAR-DEBUG] {GetPath(transform)} PointableCanvas event: {evt.Type}", this);
    }

    private void OnPointableCanvasMeshPointerEvent(PointerEvent evt)
    {
        Debug.Log($"[HPBAR-DEBUG] {GetPath(transform)} PointableCanvasMesh event: {evt.Type}", this);
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