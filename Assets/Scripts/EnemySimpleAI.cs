using UnityEngine;

public class EnemySimpleAI : MonoBehaviour
{
    public Transform player;           // nanti diisi otomatis
    public float followSpeed = 2f;     // seberapa cepat ngikutin
    public float minDistance = 1.5f;   // jangan terlalu deket player

    void Start()
    {
        // otomatis cari player
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // cuma ngikutin tinggi Y player, X tetap gerak kiri kayak pipa
        Vector3 targetPos = new Vector3(transform.position.x, player.position.y, 0);

        // jangan terlalu deket
        if (Vector3.Distance(transform.position, player.position) > minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);
        }

        // GERAK KE KIRI LEBIH PELAN
        transform.position += Vector3.left * 2f * Time.deltaTime;   

        // kalau keluar layar kiri → hapus
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}