using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;  // Kekuatan loncat (bisa diubah di Inspector)
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Tekan SPASI atau KLIK MOUSE = LONCAT!
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
} 