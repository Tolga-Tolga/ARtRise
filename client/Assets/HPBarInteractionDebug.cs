using Oculus.Interaction;
using UnityEngine;

public class HPBarInteractionDebug : MonoBehaviour
{
    [SerializeField] private PokeInteractable pokeInteractable;
    [SerializeField] private PointableCanvas pointableCanvas;
    [SerializeField] private PointableCanvasMesh pointableCanvasMesh;

    private InteractableState lastPokeState;

    private void Awake()
    {
        if (pokeInteractable == null)
            pokeInteractable = GetComponent<PokeInteractable>();

        if (pointableCanvas == null)
            pointableCanvas = GetComponent<PointableCanvas>();

        if (pointableCanvasMesh == null)
            pointableCanvasMesh = GetComponent<PointableCanvasMesh>();

        if (pokeInteractable != null)
            lastPokeState = pokeInteractable.State;
    }

    private void Update()
    {
        if (pokeInteractable == null) return;

        if (pokeInteractable.State != lastPokeState)
        {
            Debug.Log("[UI-DEBUG] PokeInteractable State: {lastPokeState} -> {pokeInteractable.State}");
            lastPokeState = pokeInteractable.State;
        }
    }
}