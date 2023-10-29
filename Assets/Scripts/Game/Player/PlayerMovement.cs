using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector2 movementInput;
    [SerializeField]
    private float speed = 4.5f;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rigidbody2D.velocity = movementInput * speed;
    }

    private void OnMove(InputValue inputValue) {
        movementInput = inputValue.Get<Vector2>();
    }

}
