using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConsumable : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;

    private void OnTriggerEnter2D(Collider2D collider2D) {

        if (collider2D.GetComponent<PlayerMovement>()) {
            PlayerShoot playerShoot = collider2D.GetComponent<PlayerShoot>();
            playerShoot.ChangeBulletType(bulletPrefab);
            Debug.Log("Releasing spawn point: " + transform.position);
            FindObjectOfType<WeaponSpawner>().releaseSpawnPoint(transform.position);
            Destroy(gameObject);
        }

    }

}
