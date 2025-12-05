using UnityEngine;

public class PipeReflex : MonoBehaviour
{
    public Transform player;
    public float reflexDistance = 5f;      // deket baru reflex
    public float reflexSpeed     = 6f;     // kecepatan turun
    public float maxDrop         = 2.8f;   // maksimal turun 2.8 unit dari posisi awal
    public float normalSpeed     = 3f;

    // Batas aman agar PipeBottom tidak nyentuh Ground & PipeTop tidak nyentuh Ceiling
    public float minAllowedY = -1.5f;   // posisi tengah pipa tidak boleh lebih rendah dari ini
    public float maxAllowedY =  2.5f;   // posisi tengah pipa tidak boleh lebih tinggi dari ini

    private bool hasReflexed = false;
    private float droppedAmount = 0f;
    private float initialY;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        initialY = transform.position.y;

        // PAKSA SPAWN DI TENGAH SAJA (biar aman)
        float safeY = Mathf.Clamp(initialY, minAllowedY + 1f, maxAllowedY - 1f);
        transform.position = new Vector3(transform.position.x, safeY, 0);
        initialY = safeY; // update initialY
    }

    void Update()
    {
        if (player == null) return;

        // Gerak kiri selalu
        transform.position += Vector3.left * normalSpeed * Time.deltaTime;
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        // Mulai reflex kalau player deket
        if (distance < reflexDistance && !hasReflexed)
            hasReflexed = true;

        // TURUN cuma parent-nya (PipeTop & PipeBottom tetap posisi relatif!)
        if (hasReflexed && droppedAmount < maxDrop)
        {
            float desiredDrop = reflexSpeed * Time.deltaTime;
            float remainingDrop = maxDrop - droppedAmount;
            float actualDrop = Mathf.Min(desiredDrop, remainingDrop);

            // CLAMP supaya tidak melebihi batas
            float newY = Mathf.Max(transform.position.y - actualDrop, initialY - maxDrop);
            newY = Mathf.Max(newY, minAllowedY);  // tidak boleh lebih rendah dari batas aman

            transform.position = new Vector3(transform.position.x, newY, 0);
            droppedAmount += (transform.position.y - (initialY - droppedAmount)); // update dropped
        }
    }
}