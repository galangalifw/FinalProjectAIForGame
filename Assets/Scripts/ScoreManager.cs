using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;   // Drag ScoreText ke sini

    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    void Update()
    {
        UpdateScoreText();
    }

    public static void AddScore()
    {
        score++;
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}