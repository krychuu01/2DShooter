using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField]
    private float damageAmount;

    private void OnCollisionStay2D(Collision2D collision2D) {
        // if collision has PlayerMovement component, then we know that enemy attacked player
        if (collision2D.gameObject.GetComponent<PlayerMovement>()) {
            var HealthController = collision2D.gameObject.GetComponent<HealthController>();

            HealthController.TakeDamage(damageAmount);
        }
    }
}
