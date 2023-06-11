using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabList;
    public Transform[] spawnPoints;

    private void Start()
    {
        SpawnRandomPrefabs();
    }

    public void SpawnRandomPrefabs()
    {
        if (prefabList.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Prefab list or spawn points not set.");
            return;
        }

        // Create a copy of the spawn points array to track available spawn points
        Transform[] availableSpawnPoints = new Transform[spawnPoints.Length];
        spawnPoints.CopyTo(availableSpawnPoints, 0);

        int prefabIndex = 0;

        for (int i = 0; i < Mathf.Min(prefabList.Length, spawnPoints.Length); i++)
        {
            if (prefabIndex >= prefabList.Length)
                prefabIndex = 0;

            GameObject prefabToSpawn = prefabList[prefabIndex];

            if (availableSpawnPoints.Length > 0)
            {
                int randomSpawnPointIndex = Random.Range(0, availableSpawnPoints.Length);
                Transform spawnPoint = availableSpawnPoints[randomSpawnPointIndex];
                Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

                // Remove the used spawn point from the available spawn points array
                availableSpawnPoints[randomSpawnPointIndex] = availableSpawnPoints[availableSpawnPoints.Length - 1];
                System.Array.Resize(ref availableSpawnPoints, availableSpawnPoints.Length - 1);
            }

            prefabIndex++;
        }
    }
}
