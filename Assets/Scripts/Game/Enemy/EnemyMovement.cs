using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D rigidbody2D;
    private PlayerAwarnessController playerAwarnessController;
    private Vector2 targetDirection;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerAwarnessController = GetComponent<PlayerAwarnessController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();     
    }

    private void UpdateTargetDirection() {
        if (playerAwarnessController.AwareOfPlayer) {
            targetDirection = playerAwarnessController.DirectionToPlayer;
        }
        else {
            targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget() {
        if (targetDirection == Vector2.zero) {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rigidbody2D.SetRotation(rotation);
    }

    private void SetVelocity() {
        if (targetDirection == Vector2.zero) {
            rigidbody2D.velocity = Vector2.zero;
        }
        else {
            rigidbody2D.velocity = transform.up * speed;
        }
    }

}
