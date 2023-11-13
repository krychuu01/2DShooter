using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab;
    [SerializeField]
    private float minimiumSpawnTime;
    [SerializeField]
    private float maximumSpawnTime;
    private float timeUntilSpawn;

    [SerializeField]
    private List<Vector3> spawnPoints;

    void Awake() {
        SetTimeUntilSpawn();
    }

    void Update() {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0) {
            Instantiate(heartPrefab, takeRandomSpawnPoint(), Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn() {
        timeUntilSpawn = Random.Range(minimiumSpawnTime, maximumSpawnTime);
    }

    private Vector3 takeRandomSpawnPoint() {
        int spawnPointsSize = spawnPoints.Count;
        int randomNumber = Random.Range(0, spawnPointsSize);
        return spawnPoints[randomNumber];
    }
}
