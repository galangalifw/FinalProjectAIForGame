using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 11f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < -16f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}