using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Camera camera;

    private void Awake() {
        camera = Camera.main;
    }

    private void Update() {
        DestroyBulletWhenGoingOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {

        if (collider2D.GetComponent<EnemyMovement>()) {
            HealthController healthController = collider2D.GetComponent<HealthController>();
            healthController.TakeDamage(10);
            Destroy(gameObject);
        }

    }

    private void DestroyBulletWhenGoingOffScreen() {
        Vector2 screenPosition = camera.WorldToScreenPoint(transform.position);
        
        if (screenPosition.x < 0 || screenPosition.x > camera.pixelWidth || screenPosition.y < 0 || screenPosition.y > camera.pixelHeight) {
            Destroy(gameObject);
        }
    }

}
