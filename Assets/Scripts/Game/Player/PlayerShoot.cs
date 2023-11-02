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
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = bulletSpeed * transform.up;
    }

    private void OnFire(InputValue inputValue) {
        fireContinuously = inputValue.isPressed;
        if (inputValue.isPressed) {
            fireSingle = true;
        }
    }
}
