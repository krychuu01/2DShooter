using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageInvincibility : MonoBehaviour
{
    
    private InvicibilityController invicibilityController;
    [SerializeField]
    private float invicibilityDuration;

    private void Awake() {
        invicibilityController = GetComponent<InvicibilityController>();
    }

    public void StartInvicibility() {
        invicibilityController.StartInvicibility(invicibilityDuration);
    }

}
