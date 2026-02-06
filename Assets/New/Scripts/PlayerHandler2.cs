using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler2 : MonoBehaviour
{
    [Header("Movement Params")] 
    [SerializeField] private float plrAcceleration = 10f;
    [SerializeField] private float plrMaxVelocity = 10f;
    [SerializeField] private float plrRotationSpeed = 180f;
    [SerializeField] public float bulletSpeed = 8f;

    [Header("Object References")]
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Rigidbody2D bulletPrefab;
    
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
            HandleShooting();
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
            plrRigidBody.AddTorque(plrRotationSpeed * Time.deltaTime);
            // plrRigidBody.angularVelocity = Vector2.ClampMagnitude(plrRigidBody.angularVelocity, plrMaxVelocity);
        } else if (Input.GetKey(KeyCode.D))
        {
            plrRigidBody.AddTorque(-plrRotationSpeed * Time.deltaTime);
            // transform.Rotate(-plrRotationSpeed * Time.deltaTime * transform.forward);
        }
    }

    private void HandleShooting()
    {
        //Handles Shooting Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            
            // Inherit Velicity only in the forward direction
            Vector2 plrVelocity = plrRigidBody.linearVelocity;
            Vector2 plrDirection = transform.up;
            float plrForwardSpeed = Vector2.Dot(plrVelocity, plrDirection.normalized);
            
            // Dont inherit from opposite direction
            if (plrForwardSpeed < 0)
            {
                plrForwardSpeed = 0;
            }

            bullet.linearVelocity = plrDirection * plrForwardSpeed;
            
            // Add force to propel bullet in direction
            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
        }
    }
}
