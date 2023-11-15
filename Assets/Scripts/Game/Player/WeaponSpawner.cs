using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> weaponPrefabs;

    [SerializeField]
    private float minimiumSpawnTime;

    [SerializeField]
    private float maximumSpawnTime;
    [SerializeField]
    private List<Vector3> spawnPoints;

    private float timeUntilSpawn;

    void Awake() {
        SetTimeUntilSpawn();
    }

    void Update() {
        timeUntilSpawn -= Time.deltaTime;
        if  (timeUntilSpawn <= 0) {
            // here we instantiating enemy with position of EnemySpawner and without rotation so we use Quaternion.identity
            Instantiate(getRandomWeaponPrefab(), takeRandomSpawnPoint(), Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn() {
        timeUntilSpawn = Random.Range(minimiumSpawnTime, maximumSpawnTime);
    }

    private GameObject getRandomWeaponPrefab() {
        int randomPrefabNumber = Random.Range(0, weaponPrefabs.Count);
        return weaponPrefabs[randomPrefabNumber];
    }

    private Vector3 takeRandomSpawnPoint() {
        int spawnPointsSize = spawnPoints.Count;
        int randomNumber = Random.Range(0, spawnPointsSize);
        return spawnPoints[randomNumber];
    }

}
