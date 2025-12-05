using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public Transform player;
    public float wanderSpeed = 2f;
    public float chaseSpeed = 4f;
    public float fleeSpeed = 6f;

    public float chaseDistance = 6f;   // kalau < ini → chase
    public float fleeDistance = 2f;    // kalau < ini → kabur

    private enum State { Wander, Chase, Flee }
    private State currentState = State.Wander;
    private Vector3 wanderTarget;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        SetNewWanderTarget();
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        // FSM LOGIC
        if (dist < fleeDistance)
            currentState = State.Flee;
        else if (dist < chaseDistance)
            currentState = State.Chase;
        else
            currentState = State.Wander;

        // EKSEKUSI STATE
        switch (currentState)
        {
            case State.Wander:
                Wander();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Flee:
                Flee();
                break;
        }

        // Gerak kiri terus (biar ikut alur game)
        transform.position += Vector3.left * 2f * Time.deltaTime;

        // Hapus kalau keluar layar
        if (transform.position.x < -15f) Destroy(gameObject);
    }

    void SetNewWanderTarget()
    {
        wanderTarget = new Vector3(transform.position.x + Random.Range(-3f, 3f),
                                 transform.position.y + Random.Range(-2f, 2f), 0);
    }

    void Wander()
    {
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
            SetNewWanderTarget();
    }

    void Chase()
    {
        Vector3 target = new Vector3(transform.position.x, player.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
    }

    void Flee()
    {
        Vector3 fleeDir = (transform.position - player.position).normalized;
        transform.position += fleeDir * fleeSpeed * Time.deltaTime;
    }
}