using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<Vector3> occupiedSpawnPoints = new List<Vector3>();
    private static Vector3 DEFAULT_SPAWN_POINT_IF_OTHERS_ARE_OCCUPIED = Vector3.zero;
    private float timeUntilSpawn;

    void Awake() {
        SetTimeUntilSpawn();
    }

    void Update() {
        timeUntilSpawn -= Time.deltaTime;
        if  (timeUntilSpawn <= 0) {
            Vector3 spawnPoint = takeRandomSpawnPoint();
            if (!spawnPoint.Equals(DEFAULT_SPAWN_POINT_IF_OTHERS_ARE_OCCUPIED)) {
                // here we instantiating weapon with position of spawn point and without rotation so we use Quaternion.identity
                Instantiate(getRandomWeaponPrefab(), spawnPoint, Quaternion.identity);
                occupiedSpawnPoints.Add(spawnPoint);
            }
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
        List<Vector3> availablePoints = spawnPoints.Where(spawnPoint => !occupiedSpawnPoints.Contains(spawnPoint)).ToList();
        int availableSpawnPointsSize = availablePoints.Count;

        if (availableSpawnPointsSize == 0) {
            Debug.Log("All spawnpoints are occupied.");
            return DEFAULT_SPAWN_POINT_IF_OTHERS_ARE_OCCUPIED;
        }

        int randomNumber = Random.Range(0, availableSpawnPointsSize);
        return availablePoints[randomNumber];
    }

    public void releaseSpawnPoint(Vector3 spawnPoint) {
        occupiedSpawnPoints.Remove(spawnPoint);
    }

}
