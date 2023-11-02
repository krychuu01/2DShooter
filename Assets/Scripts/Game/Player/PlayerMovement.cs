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

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    private void SetPlayerVelocity() {
        // added so character stops moving smoothly, not when user stops pushing buttons 
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rigidbody2D.velocity = smoothedMovementInput * speed;
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
