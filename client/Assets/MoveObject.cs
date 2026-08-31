using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Zielkoordinaten im Unity-Weltkoordinatensystem
    public Vector3 targetPosition = new Vector3(1f, 0.5f, 2f);

    void Update()
    {
        // Direkt setzen:
        // transform.position = targetPosition;

        // Oder sanft bewegen:
        transform.position = Vector3.Lerp(
            transform.position, 
            targetPosition, 
            Time.deltaTime * 2f
        );
    }
}
