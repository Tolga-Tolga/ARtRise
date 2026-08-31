using UnityEngine;
using Oculus.Interaction;

public class CanvasFocusReceiver : MonoBehaviour
{
    [SerializeField] private PointableCanvas pointableCanvas;

    private void Awake()
    {
        if (pointableCanvas == null)
            pointableCanvas = GetComponent<PointableCanvas>();
    }

    private void OnEnable()
    {
        if (pointableCanvas == null)
        {
            Debug.LogError("Kein PointableCanvas gefunden auf " + gameObject.name);
            return;
        }

        pointableCanvas.WhenPointerEventRaised += OnPointerEventRaised;
    }

    private void OnDisable()
    {
        if (pointableCanvas != null)
            pointableCanvas.WhenPointerEventRaised -= OnPointerEventRaised;
    }

    private void OnPointerEventRaised(PointerEvent evt)
    {
        Debug.Log($"Canvas Pointer Event: {evt.Type}");

        if (evt.Type == PointerEventType.Hover)
        {
            Debug.Log("CANVAS FOCUS ENTER");
        }
        else if (evt.Type == PointerEventType.Unhover)
        {
            Debug.Log("CANVAS FOCUS EXIT");
        }
    }
}