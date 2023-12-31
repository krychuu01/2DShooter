using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;

    private bool fireContinuously;
    [SerializeField]
    private Transform gunOffset;

    [SerializeField]
    private float timeBetweenShots;

    private float lastFireTime;
    private bool fireSingle;
    private Camera camera;
    [SerializeField]
    private int shootsLeft;

    private void Awake() {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if ( fireContinuously || fireSingle ) {
            float timeSinceLastFire = Time.time - lastFireTime;
            if (timeSinceLastFire >= timeBetweenShots) {
                FireBullet();
                lastFireTime = Time.time;
                fireSingle = false;
            }

        }
    }

    private void FireBullet() {
        Vector3 direction = Input.mousePosition;
        direction = camera.ScreenToWorldPoint(direction);
        direction -= transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instantiate the bullet at the player's position with the rotation
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();

        // Set the velocity of the bullet based on the direction
        rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
    }

    private void OnFire(InputValue inputValue) {
        fireContinuously = inputValue.isPressed;
        if (inputValue.isPressed) {
            fireSingle = true;
        }
    }

    public void ChangeBulletType(GameObject newBulletPrefab) {
        if (newBulletPrefab.name == "FastBullet") {
            timeBetweenShots = 0.25f;
        }
        if (newBulletPrefab.name == "BasicBullet") {
            timeBetweenShots = 0.5f;
        }
        bulletPrefab = newBulletPrefab;
    }
    
}
