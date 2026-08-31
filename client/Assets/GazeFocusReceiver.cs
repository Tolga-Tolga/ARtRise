using UnityEngine;
using UnityEngine.EventSystems;

public class GazeFocusReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"FOCUS ENTER: {gameObject.name}, pointer: {eventData.pointerId}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"FOCUS EXIT: {gameObject.name}, pointer: {eventData.pointerId}");
    }
}