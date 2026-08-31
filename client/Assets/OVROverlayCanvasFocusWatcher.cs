using UnityEngine;
using UnityEngine.Events;

public class OVROverlayCanvasFocusWatcher : MonoBehaviour
{
    [SerializeField] private OVROverlayCanvas overlayCanvas;

    public UnityEvent OnFocusEnter;
    public UnityEvent OnFocusExit;

    private bool lastPriority;

    private void Awake()
    {
        if (overlayCanvas == null)
            overlayCanvas = GetComponent<OVROverlayCanvas>();
    }

    private void OnEnable()
    {
        if (overlayCanvas == null)
        {
            Debug.LogError("Kein OVROverlayCanvas gefunden auf " + gameObject.name, this);
            enabled = false;
            return;
        }

        lastPriority = overlayCanvas.IsCanvasPriority;
    }

    private void Update()
    {
        bool currentPriority = overlayCanvas.IsCanvasPriority;

        if (currentPriority == lastPriority)
            return;

        lastPriority = currentPriority;

        if (currentPriority)
        {
            Debug.Log("[OVR-FOCUS] Enter: " + gameObject.name, this);
            OnFocusEnter?.Invoke();
        }
        else
        {
            Debug.Log("[OVR-FOCUS] Exit: " + gameObject.name, this);
            OnFocusExit?.Invoke();
        }
    }
}