using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider2D) {

        if (collider2D.GetComponent<EnemyMovement>()) {
            Destroy(collider2D.gameObject);
            Destroy(gameObject);
        }

    }

}
