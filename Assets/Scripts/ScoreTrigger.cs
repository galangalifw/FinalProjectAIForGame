using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool sudahLewat = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !sudahLewat)
        {
            sudahLewat = true;
            ScoreManager.AddScore();
        }
    }
}