using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvicibilityController : MonoBehaviour
{
    private HealthController healthController;

    private void Awake() {
        healthController = GetComponent<HealthController>();
    }

    public void StartInvicibility(float invicibilityDuration) {
        StartCoroutine(InvicibilityCoroutine(invicibilityDuration));
    }

    private IEnumerator InvicibilityCoroutine(float invicibilityDuration) {
        healthController.IsInvincible = true;
        yield return new WaitForSeconds(invicibilityDuration);
        healthController.IsInvincible = false;
    }
}
