using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maximumHealth;

    public float RemainingHealthPercentage {
        get {
            return currentHealth/maximumHealth;
        }
    }

    public bool IsInvincible { get; set;}

    public UnityEvent onDied;
    public UnityEvent onDamaged;
    public UnityEvent onHealthChanged;

    public void TakeDamage(float damageAmount) {
        if (currentHealth == 0) {
            return;
        }

        if (IsInvincible) {
            return;
        }

        currentHealth -= damageAmount;
        
        onHealthChanged.Invoke();
        
        if (currentHealth < 0) {
            currentHealth = 0;
        }

        if(currentHealth == 0) {
            onDied.Invoke();
        }
        else {
            onDamaged.Invoke();
        }

    }

    public void AddHealth(float amountToAdd) {
        if (currentHealth == maximumHealth) {
            return;
        }

        currentHealth += amountToAdd;

        onHealthChanged.Invoke();

        if (currentHealth > maximumHealth) {
            currentHealth = maximumHealth;
        }
    }   
}
