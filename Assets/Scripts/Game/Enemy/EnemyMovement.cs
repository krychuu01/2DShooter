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

    [SerializeField]
    private float screenBorder;
    private Camera camera;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerAwarnessController = GetComponent<PlayerAwarnessController>();
        targetDirection = transform.up;
        camera = Camera.main;
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
        HandleEnemyGoingOffScreen();
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

    private void HandleEnemyGoingOffScreen() {
        Vector2 screenPosition = camera.WorldToScreenPoint(transform.position); // we retrieve the player's position relative to the main camera

        // if enemy is moving towards end of the left or right map end, then turn him around by negating his X axis position
        if ( (screenPosition.x < screenBorder && targetDirection.x < 0) || (screenPosition.x > camera.pixelWidth - screenBorder && targetDirection.x > 0)) {
            targetDirection = new Vector2(-targetDirection.x, targetDirection.y);
        }

        // if enemy is moving towards end of the top or bottom map end, then turn him around by negating his Y axis position
        if ( (screenPosition.y < screenBorder && targetDirection.y < 0) || (screenPosition.y > camera.pixelHeight - screenBorder && targetDirection.y > 0)) {
            targetDirection = new Vector2(targetDirection.x, -targetDirection.y);
        }
    }
}
