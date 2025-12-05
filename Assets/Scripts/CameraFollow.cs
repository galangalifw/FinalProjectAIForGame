using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Burung yang diikuti
    public float smoothSpeed = 0.125f;  // Kamera gerak halus
    public Vector3 offset;  // Jarak kamera dari burung

    void Start()
    {
        // Otomatis ambil Player
        target = GameObject.Find("Player").transform;
        offset = new Vector3(5f, 0, -10f);  // Kamera di belakang burung
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, transform.position.y, -10f);
        }
    }
}