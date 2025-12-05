using UnityEngine;

public class FlockBird : MonoBehaviour
{
    public FlockManager manager;
    public float speed = 3f;
    private Vector3 velocity;

    void Start()
    {
        velocity = transform.right * speed;
        if (manager == null) manager = FindObjectOfType<FlockManager>();
    }

    void Update()
    {
        if (manager == null) return;

        ApplyFlock();
        transform.position += velocity * Time.deltaTime;
        transform.position += Vector3.left * 2f * Time.deltaTime;

        if (transform.position.x < -15f)
        {
            manager.allBirds.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    void ApplyFlock()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int count = 0;

        foreach (GameObject b in manager.allBirds)
        {
            if (b != null && b != gameObject)
            {
                float d = Vector3.Distance(transform.position, b.transform.position);
                if (d < manager.neighbourDistance)
                {
                    cohesion += b.transform.position;
                    align += b.GetComponent<FlockBird>().velocity;
                    separation += (transform.position - b.transform.position);
                    count++;
                }
            }
        }

        if (count > 0)
        {
            cohesion = (cohesion / count - transform.position).normalized * speed;
            align = align / count;
            separation = separation.normalized * speed;

            velocity += cohesion * 1f + align * 1.5f + separation * 2f;
        }

        velocity = velocity.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cari script player apa pun yang punya method Die() atau GameOver()
            var playerScript = other.GetComponentInParent<MonoBehaviour>();
            if (playerScript != null)
            {
                // Coba panggil method yang umum
                playerScript.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
                // atau
                // playerScript.SendMessage("GameOver", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    
    void OnDestroy()
    {
        if (manager != null) manager.allBirds.Remove(gameObject);
    }
}