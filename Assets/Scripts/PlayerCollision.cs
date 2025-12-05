using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameOverText;   // nanti kita buat

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe") || other.CompareTag("Ground") || other.gameObject.name.Contains("Enemy"))
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    void Die()
    {
        Time.timeScale = 0;  // game berhenti
        if (gameOverText != null)
            gameOverText.SetActive(true);
    }

    // restart dengan tekan R
    void Update()
    {
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
}