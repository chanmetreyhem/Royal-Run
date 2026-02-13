
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class ObstracleSpawn : MonoBehaviour
{
   [SerializeField] private List<GameObject> obstaclePrefabs = new List<GameObject>();
    [SerializeField] float spawnInterval = 1;
    [SerializeField] float xSpawnOffset = 3;
    int spawnCount = 5;
    float minSpawnInterval = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnObstacleCoroutine());
    }

    IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            var obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
            var xPos = Random.Range(-xSpawnOffset, xSpawnOffset);
            Instantiate(obstacle,new Vector3(xPos, transform.position.y, transform.position.z),Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void DecreasSpawnTime(float amount)
    {
        spawnInterval -= amount;
        if(spawnInterval <= minSpawnInterval)
        {
            spawnInterval = minSpawnInterval;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
