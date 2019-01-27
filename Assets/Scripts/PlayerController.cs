using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public float moveSpeed;
    public float rotationSpeed;
    public GameObject flare;
    public FlareCounter fc;

    private float rMovement;
    private float zMovement;
    private Rigidbody rb;
    private int flaresRemaining;

    // Start is called before the first frame update
    void Start()
    {
        rMovement = 0;
        zMovement = 0;
        rb = this.GetComponent<Rigidbody>();
        flaresRemaining = 3;
    }

    void FixedUpdate()
    {
        float moveForward = 0;
        float moveStrafe = 0;
        //PLAYER CONTROLS
        //STEP ONE RESET MOVEMENT VARIABLES
        rMovement = 0f;
        zMovement = 0f;
        //STEP TWO STORE INTENDED MOVEMENT
        if (gameController.IsGameOn())
        {
            moveForward = Input.GetAxis("Vertical") + Input.GetAxis("Horizontal");
            moveStrafe = -Input.GetAxis("Vertical") + Input.GetAxis("Horizontal");
            zMovement = moveForward * moveSpeed;
            rMovement = moveStrafe * moveSpeed;
            if (!gameController.isPanning)
            {
                transform.Translate(rMovement, 0, zMovement, Space.World);
            }
            Vector3 targetPoint = new Vector3(
                Input.GetAxis("Vertical") - Input.GetAxis("Horizontal"),
                0,
                - Input.GetAxis("Vertical") - Input.GetAxis("Horizontal")
            );
            if (!gameController.isPanning && targetPoint.sqrMagnitude > 0)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(-targetPoint, Vector3.up),
                    Time.deltaTime * 2.0f
                );
            }
            if (Input.GetAxis("Fire1") > 0
                && gameController.interactionCooldown <= 0
                && flaresRemaining > 0)
            {
                fc.removeFlare();
                DropFlare();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Safety") gameController.YouAreSafe();
    }

    private void DropFlare()
    {
        gameController.interactionCooldown = 15;
        Vector3 playerPos = transform.position;
        Vector3 playerDirection = transform.forward;
        Quaternion playerRotation = transform.rotation;
        float spawnDistance = 1;

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        Instantiate(flare, spawnPos, playerRotation);
        flaresRemaining--;
    }
}
