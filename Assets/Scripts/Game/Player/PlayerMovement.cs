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
    private Animator animator;

    [SerializeField]
    private float screenBorder;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        SetPlayerVelocity();
        RotateInDirectionOfMouse();
        SetAnimation();
    }

    private void SetAnimation() {
        bool isMoving = movementInput != Vector2.zero;

        animator.SetBool("IsMoving", isMoving);
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


    private void RotateInDirectionOfMouse() {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    private void OnMove(InputValue inputValue) {
        movementInput = inputValue.Get<Vector2>();
    }

}
