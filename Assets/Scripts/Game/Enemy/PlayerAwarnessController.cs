using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarnessController : MonoBehaviour
{
    public bool AwareOfPlayer{ get; private set;}

    public Vector2 DirectionToPlayer {get; private set;}

    [SerializeField]
    private float playerAwarnessDistance;

    private Transform player;

    private void Awake() {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= playerAwarnessDistance) {
            AwareOfPlayer = true;
        }   
        else {
            AwareOfPlayer = false;
        }
    }

    
}
