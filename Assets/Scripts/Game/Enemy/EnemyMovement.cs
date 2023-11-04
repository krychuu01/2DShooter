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
    private float changeDirectionCooldown;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerAwarnessController = GetComponent<PlayerAwarnessController>();
        targetDirection = transform.up;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();     
    }

    private void UpdateTargetDirection() {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        
    }

    private void RotateTowardsTarget() {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rigidbody2D.SetRotation(rotation);
    }

    private void SetVelocity() {
        rigidbody2D.velocity = transform.up * speed;
    }

    private void HandleRandomDirectionChange() {
        changeDirectionCooldown -= Time.deltaTime;

        if (changeDirectionCooldown <= 0) {
            float randomAngle = Random.Range(-90f, 90f); // setting random angle from -90 to 90 degrees
            Quaternion rotation = Quaternion.AngleAxis(randomAngle, transform.forward); // setting rotation so enemy will turn to a random angle, forward
            targetDirection = rotation * targetDirection; // multiplying existing targetDirection with 'rotation' value to turn enemy in a specific way

            changeDirectionCooldown = Random.Range(1f, 5f); // setting cooldown from 1 to 5 seconds
        }
    }

    private void HandlePlayerTargeting() {
        if (playerAwarnessController.AwareOfPlayer) {
            targetDirection = playerAwarnessController.DirectionToPlayer;
        }
    }
}
