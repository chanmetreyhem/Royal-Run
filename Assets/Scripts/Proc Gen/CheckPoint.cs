using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] int timeExtension = 6;
    private GameManager gameManager;
    private ObstracleSpawn obstracleSpawn;
    private const string PLAYER_STRING = "Player";
    [SerializeField] TextMesh timeExtensionText;
    private float descreasSpawnerTime = 0.1f;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstracleSpawn = FindFirstObjectByType<ObstracleSpawn>();
        timeExtensionText = GetComponentInChildren<TextMesh>();
        timeExtensionText.text = "+" + timeExtension.ToString();
    }
    public void Init(GameManager gameManager) => this.gameManager = gameManager;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_STRING))
        {
            gameManager?.IncreasTime(timeExtension);
            obstracleSpawn.DecreasSpawnTime(descreasSpawnerTime);
        }
    }
}
