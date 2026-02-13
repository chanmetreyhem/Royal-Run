using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Text timeText;
    [SerializeField] float startTime = 5f;
    float timeLeft;
    public void IncreasTime(int time) => timeLeft += time;
    bool isGameOver = false;

    public bool IsGameOver { get => isGameOver; }
    void Start()
    {
        Time.timeScale = 1f;
        timeLeft = startTime;
    }

    // Update is called once per frame
    void Update()
    {
       if (isGameOver) return;
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");
        if (timeLeft <= 0)
        {
            GameOver();
           
        }
            
       
    }

    void GameOver()
    {
        isGameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }
}
