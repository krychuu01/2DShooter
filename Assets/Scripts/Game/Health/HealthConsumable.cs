using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : MonoBehaviour
{

    [SerializeField]
    private float healFor;

    private void OnTriggerEnter2D(Collider2D collider2D) {
        // if enemy touched hearth
        if (collider2D.GetComponent<PlayerMovement>()) {
            HealthController healthController = collider2D.GetComponent<HealthController>();
            healthController.AddHealth(healFor);
            Destroy(gameObject);
        }
    }
}
