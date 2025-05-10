using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public int numberToSpawn = 7;
    public Vector3 spawnAreaSize = new Vector3(10, 1, 10); 
    public Vector3 center = Vector3.zero; 

    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnRandomTarget();
        }
    }

    void SpawnRandomTarget()
    {
        Vector3 randomPosition = center + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        Instantiate(targetPrefab, randomPosition, Quaternion.identity);
    }
}