using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("=== PREFAB MUSUH ===")]
    public GameObject simpleEnemyPrefab;    // Burung merah (Simple AI)
    public GameObject fsmEnemyPrefab;       // Burung ungu (Finite State Machine)
    public GameObject flockManagerPrefab;   // Formasi V biru (Flocking)
    // nanti bisa ditambah lagi kalau mau
    public GameObject bossPrefab;


    [Header("=== SETTING SPAWN ===")]
    public float spawnInterval = 6.5f;      // tiap berapa detik coba spawn
    public float spawnX = 12f;              // posisi X spawn (kanan layar)

    [Header("=== PROBABILITAS (jumlahkan ≤ 1) ===")]
    [Range(0f, 1f)] public float chanceSimple = 0.40f;   // 40% burung merah
    [Range(0f, 1f)] public float chanceFSM    = 0.35f;   // 35% burung ungu FSM
    [Range(0f, 1f)] public float chanceFlock  = 0.25f;   // 25% formasi V biru
    [Range(0f,1f)] public float chanceBoss = 0.08f; // 8% kesempatan


    void Start()
    {
        InvokeRepeating(nameof(TrySpawn), 3f, spawnInterval);
    }

    void TrySpawn()
    {
        float rand = Random.value;

        // 1. Simple AI (burung merah)
        if (rand < chanceSimple)
        {
            if (simpleEnemyPrefab != null)
                SpawnSingle(simpleEnemyPrefab);
            return;
        }

        // 2. FSM AI (burung ungu)
        rand -= chanceSimple;
        if (rand < chanceFSM)
        {
            if (fsmEnemyPrefab != null)
                SpawnSingle(fsmEnemyPrefab);
            return;
        }

        // 3. Flocking V-formation (5 burung biru sekaligus)
        rand -= chanceFSM;
        if (rand < chanceFlock)
        {
            if (flockManagerPrefab != null)
                Instantiate(flockManagerPrefab, new Vector3(spawnX, Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);
            return;
        }

        else if (rand < chanceSimple + chanceFSM + chanceFlock + chanceBoss)
        {
            Instantiate(bossPrefab, new Vector3(12f, Random.Range(-1f,1f),0), Quaternion.identity);
        }

        // Kalau semua chance < 1, sisanya gak spawn apa-apa (biar ada jeda)
    }

    void SpawnSingle(GameObject prefab)
    {
        Vector3 pos = new Vector3(spawnX, Random.Range(-3.5f, 3.5f), 0);
        Instantiate(prefab, pos, Quaternion.identity);
    }
}