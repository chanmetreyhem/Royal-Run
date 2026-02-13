using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private CameraController CameraController;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private GameObject[] chunkPrefabs;
    [SerializeField] private GameObject checkPointPrefab;
    [SerializeField] private ScoreManager scoreManager;
    [Header("Level Setting")]
    [SerializeField] int chunkAmount = 12;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] int checkPointInterval = 10;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float minSpeed = 2;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float minGravity = -2;
    [SerializeField] private float maxGravity = -22;
    List<GameObject> chunks = new List<GameObject>();
    int chunkSpawnCount = 0;
    private void Start()
    {
        SpawnStartChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    private void SpawnStartChunks()
    {
        for (int i = 0; i < chunkAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        var pos = CalculateSpawnPosZ();
        var chunkToSpawn = (chunkSpawnCount % checkPointInterval == 0 && chunkSpawnCount != 0) ? checkPointPrefab : chunkPrefabs[Random.Range(0,chunkPrefabs.Length)];
        GameObject newChunk = Instantiate(chunkToSpawn, pos, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
        newChunk.GetComponent<Chunk>().Init(this, scoreManager);
        chunkSpawnCount += 1;
        if(chunkSpawnCount > checkPointInterval) chunkSpawnCount = 0;
    }

    private Vector3 CalculateSpawnPosZ()
    {
        float zPos = 0;
        if (chunks.Count == 0)
        {
            zPos = transform.position.z;
        }
        else
        {
            zPos = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }
        var position = new Vector3(transform.position.x, chunkParent.transform.position.y, zPos);
        return position;
    }

    private void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            var chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
            if(chunk.transform.position.z < Camera.main.transform.position.z - 10)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }

    public void ChangeChunkSpeed(float changeSpeed)
    {
        float newMoveSpeed = moveSpeed + changeSpeed;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minSpeed, maxSpeed);
        moveSpeed += changeSpeed;
        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;
            float newZGravity = Physics.gravity.z - changeSpeed;
            newZGravity = Mathf.Clamp(newZGravity, minGravity, maxGravity);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newZGravity);
        }
      
        CameraController.ChangeCameraFOV(changeSpeed);
    }
     
}