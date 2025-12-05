using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 3f;   // kecepatan pipa

    void Update()
    {
        // gerak ke kiri terus
        transform.position += Vector3.left * speed * Time.deltaTime;

        // kalau sudah jauh ke kiri, hapus biar gak berat
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}