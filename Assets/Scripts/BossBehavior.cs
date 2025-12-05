using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public Transform player;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float shootDistance = 6f;
    public float tooCloseDistance = 2.5f;
    public GameObject bulletPrefab;
    public float shootInterval = 1.5f;

    private enum State { Patrol, Chase, Shoot, Flee }
    private State currentState = State.Patrol;

    private Vector3 patrolTarget;
    private float patrolTimer = 0f;
    private float shootTimer = 0f;

    private float escapeY = 0f;
    private bool hasEscaped = false;   // Hanya sekali cari Y

    void Start()
    {
        player = GameObject.Find("Player")?.transform;
        SetNewPatrolTarget();
    }

    void Update()
    {
        if (player == null) return;

        shootTimer -= Time.deltaTime;
        float dist = Vector2.Distance(transform.position, player.position);

        // PRIORITAS TERTINGGI: KALAU DEKET → LANGSUNG FLEE!
        if (dist < tooCloseDistance)
            currentState = State.Flee;
        else if (dist < shootDistance)
            currentState = State.Shoot;
        else if (dist < 10f)
            currentState = State.Chase;
        else
            currentState = State.Patrol;

        switch (currentState)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase:  Chase();  break;
            case State.Shoot:  Shoot();  break;
            case State.Flee:   Flee();   break;
        }

        // Gerak kiri normal (kecuali lagi kabur)
        if (currentState != State.Flee)
            transform.position += Vector3.left * 1.2f * Time.deltaTime;

        if (transform.position.x < -16f) Destroy(gameObject);
    }

    void SetNewPatrolTarget()
    {
        patrolTarget = transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-2f, 2f), 0);
        patrolTimer = Random.Range(2f, 4f);
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);
        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0f) SetNewPatrolTarget();
    }

    void Chase()
    {
        Vector3 t = new Vector3(transform.position.x, player.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, t, chaseSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        Chase();
        if (shootTimer <= 0f && bulletPrefab != null)
        {
            Instantiate(bulletPrefab, transform.position + Vector3.left * 1f, Quaternion.identity);
            shootTimer = shootInterval;
        }
    }

    // INI YANG BIKIN NGACIR GILA KAYAK KURA-KURA MARIO!
    void Flee()
    {
        // Hanya sekali cari Y celah pipa
        if (!hasEscaped)
        {
            escapeY = FindNearestGapY();
            hasEscaped = true;
        }

        // NGACIR KENCANG BANGET KE KANAN + KE Y CELAH!
        float PANIC_SPEED = 50f;  // CEPET GILA! (bisa 60f kalau mau lebih cepat)
        transform.position += Vector3.right * PANIC_SPEED * Time.deltaTime;

        // Gerakin Y langsung ke celah (tanpa MoveTowards biar kencang)
        float yDiff = escapeY - transform.position.y;
        transform.position += Vector3.up * Mathf.Sign(yDiff) * 30f * Time.deltaTime;

        // Muter-muter panik
        transform.Rotate(0, 0, 1500f * Time.deltaTime);

        // Keluar layar kanan = langsung hilang
        if (transform.position.x > 16f)
            Destroy(gameObject);
    }

    float FindNearestGapY()
    {
        GameObject[] pipes = GameObject.FindObjectsOfType<GameObject>();
        float bestY = transform.position.y;
        float closestX = float.MaxValue;

        foreach (GameObject g in pipes)
        {
            if ((g.name.Contains("Pipe") || g.CompareTag("Pipe")) && g.transform.position.x > transform.position.x - 3f)
            {
                if (g.transform.position.x < closestX)
                {
                    closestX = g.transform.position.x;
                    bestY = g.transform.position.y;  // Y parent = tengah celah!
                }
            }
        }
        return bestY;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
    }
}