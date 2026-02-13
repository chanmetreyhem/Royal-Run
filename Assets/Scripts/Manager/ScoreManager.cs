using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text scoreText;
    int score = 0;
    public void AddScore(int score)
    {
        if (gameManager.IsGameOver) return;
        this.score += score;
        scoreText.text = this.score.ToString("D4");
    }
   
}
