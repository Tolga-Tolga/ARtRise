using UnityEngine;

public class ColliderFocusReceiver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[UI-DEBUG] TRIGGER ENTER: {other.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"[UI-DEBUG] TRIGGER EXIT: {other.name}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[UI-DEBUG] COLLISION ENTER: {collision.collider.name}");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"[UI-DEBUG] COLLISION EXIT: {collision.collider.name}");
    }
}