using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;
    [SerializeField]
    private float speed = 4.5f;
    [SerializeField]
    private float rotationSpeed;

    private Camera camera;

    [SerializeField]
    private float screenBorder;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void FixedUpdate() {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    private void SetPlayerVelocity() {
        // added so character stops moving smoothly, not when user stops pushing buttons 
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rigidbody2D.velocity = smoothedMovementInput * speed;

        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen() {
        Vector2 screenPosition = camera.WorldToScreenPoint(transform.position); // we retrieve the player's position relative to the main camera

        // check if player position is on the left or right map end, if yes, then unable his movement on the X axis to prevent him from going out of the map 
        // also distract screenBorder value to keep entire player sprite on the scene
        if ( (screenPosition.x < screenBorder && rigidbody2D.velocity.x < 0) || (screenPosition.x > camera.pixelWidth - screenBorder && rigidbody2D.velocity.x > 0)) {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }

        // check if player position is on the top or bottom map end, if yes, then unable his movement on the Y axis to prevent him from going out of the map
        // also distract screenBorder value to keep entire player sprite on the scene
        if ( (screenPosition.y < screenBorder && rigidbody2D.velocity.y < 0) || (screenPosition.y > camera.pixelHeight - screenBorder && rigidbody2D.velocity.y > 0)) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        }
    }


    private void RotateInDirectionOfInput() {
        if (movementInput != Vector2.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rigidbody2D.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue) {
        movementInput = inputValue.Get<Vector2>();

    }

}
