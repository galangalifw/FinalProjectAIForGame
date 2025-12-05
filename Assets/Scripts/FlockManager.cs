using UnityEngine;
using System.Collections.Generic;

public class FlockManager : MonoBehaviour
{
    public GameObject birdPrefab;
    public int numberOfBirds = 7;
    public List<GameObject> allBirds = new List<GameObject>();

    public float neighbourDistance = 1.8f;
    public float speed = 3f;

    void Start()
    {
        SpawnVFormation();
    }

    void SpawnVFormation()
    {
        Vector3 center = transform.position;

        // Formasi V klasik (7 burung)
        Vector3[] positions = new Vector3[]
        {
            center,                                            // leader
            center + new Vector3(-1.5f,  1.2f, 0),             // kiri atas
            center + new Vector3( 1.5f,  1.2f, 0),             // kanan atas
            center + new Vector3(-3f,   2.4f, 0),              // kiri jauh
            center + new Vector3( 3f,   2.4f, 0),              // kanan jauh
            center + new Vector3(-4.5f, 3.6f, 0),              // paling kiri
            center + new Vector3( 4.5f, 3.6f, 0)               // paling kanan
        };

        for (int i = 0; i < numberOfBirds && i < positions.Length; i++)
        {
            GameObject bird = Instantiate(birdPrefab, positions[i], Quaternion.identity);
            FlockBird fb = bird.GetComponent<FlockBird>();
            fb.manager = this;
            fb.speed = speed;
            allBirds.Add(bird);
        }
    }

    void Update()
    {
        allBirds.RemoveAll(b => b == null);
    }
}