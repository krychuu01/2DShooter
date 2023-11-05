using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Camera camera;
    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;
    private bool isAudioPlaying = true;
    private bool canBeDestroyed = false;
    private bool canDamageEnemy = true;

    private void Awake() {
        camera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        audioSource.clip = audioClip;
        audioSource.Play();
        StartCoroutine(WaitUntilAudioIsFullyPlayed(audioClip));
    }

    private void Update() {
        DestroyBulletWhenGoingOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.GetComponent<EnemyMovement>() && canDamageEnemy) {
            HealthController healthController = collider2D.GetComponent<HealthController>();
            healthController.TakeDamage(10);
            canDamageEnemy = false;
            transform.position = new Vector2(50, 50); // change it position so it looks like it disappear from the game window, will be Destroyed later
        }
        if (canBeDestroyed) {
            Destroy(gameObject);
        }
    }

    private void DestroyBulletWhenGoingOffScreen() {
        Vector2 screenPosition = camera.WorldToScreenPoint(transform.position);
        
        if (screenPosition.x < 0 || screenPosition.x > camera.pixelWidth || screenPosition.y < 0 || screenPosition.y > camera.pixelHeight) {
            if(canBeDestroyed) {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator WaitUntilAudioIsFullyPlayed(AudioClip audioClip) {
        if (isAudioPlaying) {
            yield return new WaitForSeconds(audioClip.length);
        }
        isAudioPlaying = false;
        canBeDestroyed = true;
    }

}
