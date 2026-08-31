using UnityEngine;

public class ArrowFollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0.5f, 0f);
    public bool bobbing = true;
    public float bobSpeed = 3f;
    public float bobAmount = 0.03f;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        float bob = 0f;
        if (bobbing)
            bob = Mathf.Sin(Time.time * bobSpeed) * bobAmount;

        transform.position = target.position + offset + new Vector3(0f, bob, 0f);

        // Pfeil nach unten ausrichten
        // transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
    }
}

