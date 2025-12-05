using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject normalPipePrefab;    // PipePair biasa
    public GameObject reflexPipePrefab;    // PipeReflex baru
    public float spawnTime = 2.5f;         // lebih cepat
    public float heightRange = 3f;

    void Start()
    {
        SpawnPipe();
        InvokeRepeating("SpawnPipe", spawnTime, spawnTime);
    }

    void SpawnPipe()
    {
        // PROBABILITY: 80% pipa biasa, 20% pipa REFLEX!
        float rand = Random.Range(0f, 1f);
        GameObject pipeToSpawn = (rand < 0.5f) ? normalPipePrefab : reflexPipePrefab;

        float randomY = Random.Range(-1.2f, 1.2f);
        Vector3 spawnPos = new Vector3(12f, randomY, 0);
        Instantiate(pipeToSpawn, spawnPos, Quaternion.identity);
    }
}