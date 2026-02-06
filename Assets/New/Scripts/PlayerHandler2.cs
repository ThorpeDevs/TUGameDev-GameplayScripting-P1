using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler2 : MonoBehaviour
{
    [Header("Movement Params")] 
    [SerializeField] private float plrAcceleration = 10f;
    [SerializeField] private float plrMaxVelocity = 10f;
    [SerializeField] private float plrRotationSpeed = 180f;

    private float plrHealth = 100f;

    private Rigidbody2D plrRigidBody;
    private bool isAlive => plrHealth > 0;
    private int isAccelerating = 0;

    private void Start() {
        // Get a reference to RigidBody2D
        plrRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (isAlive) {
            HandleShipAcceleration();
            HandleShipRotationSpeed();
        }
    }

    private void FixedUpdate() {
        if (isAlive && isAccelerating != 0)
        {
            //Increase velocity
            plrRigidBody.AddForce((isAccelerating == 1 ? plrAcceleration : -plrAcceleration) * transform.up);
            plrRigidBody.linearVelocity = Vector2.ClampMagnitude(plrRigidBody.linearVelocity, plrMaxVelocity);
        }
    }

    private void HandleShipAcceleration(){
        isAccelerating = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
    }

    private void HandleShipRotationSpeed() {
        if (Input.GetKey(KeyCode.A))
        {
            // plrRigidBody.Add
            transform.Rotate(plrRotationSpeed * Time.deltaTime * transform.forward);
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-plrRotationSpeed * Time.deltaTime * transform.forward);
        }
    }
}
