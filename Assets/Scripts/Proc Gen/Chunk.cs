using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private GameObject fencePrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject coinPrefab;

    private ScoreManager scoreManager;
    private LevelGenerator levelGenerator;
    private float[] lanes = { -2.5f, 0, 2.5f };


    float appleSpawnChance = 0.3f;
    float coinSpawnChance = 0.5f;
    float coinSeperationLength = 1f;
    List<int> availableLanes = new List<int> { 0,1,2 };
    public void Init(LevelGenerator levelGenerator , ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }
    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    private void SpawnApple()
    {
        if(Random.value > appleSpawnChance) return;
        if (availableLanes.Count <= 0) return;
        int selectedLane = SelectedLand();

        Vector3 spawnPos = new Vector3(lanes[selectedLane],applePrefab.transform.position.y, transform.position.z);
        var appleSpawn = Instantiate(applePrefab, spawnPos, Quaternion.identity).GetComponent<Apple>();
        appleSpawn.transform.SetParent(transform, true);
        appleSpawn.Init(levelGenerator);
    }

    void SpawnFences()
    {
        int fenceToSpawn = Random.Range(0,lanes.Length);
      
        for (int i = 0; i < fenceToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;
            int selectedLane = SelectedLand();

            Vector3 spawnPos = new Vector3(lanes[selectedLane], fencePrefab.transform.position.y, transform.position.z);
            var spawnFence = Instantiate(fencePrefab, spawnPos, Quaternion.identity);
            spawnFence.transform.SetParent(transform, true);
        }
    }

    void SpawnCoins()
    {
        if (Random.value > coinSpawnChance) return;
        if (availableLanes.Count <= 0) return;
        int selectedLane = SelectedLand();

        int maxCoin = 6;
        int coinToSpawn = Random.Range(0, maxCoin);
        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);

       for (int i = 0;i < coinToSpawn; i++)
        {
            float zPos = topOfChunkZPos + (i * coinSeperationLength);
            Vector3 spawnPos = new Vector3(lanes[selectedLane], coinPrefab.transform.position.y,zPos);
            var coinSpawn = Instantiate(coinPrefab, spawnPos, Quaternion.identity).GetComponent<Coin>();
            coinSpawn.transform.SetParent(transform, true);
            coinSpawn.Init(scoreManager);
        }
    }

    private int SelectedLand()
    {
        int randNum = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randNum];

        availableLanes.RemoveAt(randNum);
        return selectedLane;
    }
}
