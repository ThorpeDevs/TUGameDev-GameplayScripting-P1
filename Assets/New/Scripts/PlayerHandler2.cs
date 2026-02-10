using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHandler2 : MonoBehaviour
{
    [Header("Movement Params")] 
    [SerializeField] private float plrAcceleration = 10f;
    [SerializeField] private float plrMaxVelocity = 10f;
    [SerializeField] private float plrRotationSpeed = 180f;
    [SerializeField] private float bulletCooldown = 0.1f;
    [SerializeField] public float bulletSpeed = 8f;
    [SerializeField] public float dashCooldown = 10f;
    [SerializeField] public float dashDBCooldown = 2f;
    [SerializeField] public float dashRegenSpeed = 1f;
    [SerializeField] public int Dashes = 1;
    [SerializeField] public int MaxDashes = 3;

    [Header("Object References")]
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private ParticleSystem DestructionParticles;
    [SerializeField] private ParticleSystem MovementParticle;


    public float plrHealth = 100f;

    private float dashDB = 0f;
    private float dashRegen = 0f;   
 
    private Rigidbody2D plrRigidBody;
    public bool isAlive => plrHealth > 0;
    private bool hasDied = false;
    public bool isIframes = false;
    private int isAccelerating = 0;
    private bool isDashCooldown => dashDB > 0;

    private float shootDB = 0;
    
    private Scene CurrentScene; // Holds The Current Scene

    private bool CantDash
    {
        get
        {
            if (isDashCooldown) return true; // Returns If Dash Is On DB
            return Dashes <= 0; // Returns if Has No Dashes
        }
    }


    private void Start() {
        // Get a reference to RigidBody2D
        plrRigidBody = GetComponent<Rigidbody2D>();
    }

    private void HandleDashDB()
    {
        switch (isDashCooldown)
        {
            case true: // If True
                dashDB -= Time.deltaTime; // Removes Time.Deltatime (real time) from dash DB
                // dashRegen = 0;
                break; // Breaks Free
            case false: // If False
                if (Dashes < MaxDashes) RegenDashes(); // If Dashes are below the max it will use the RegenDashes Function
                break; // Breaks Free
        }
    }

    private void RegenDashes()
    {
        dashRegen += (dashRegenSpeed * Time.deltaTime);
        if (dashRegen >= dashCooldown)
        {
            dashRegen = 0; // Sets Regen Value To 0 So Full Regen Time Takes Place
            Dashes++; // Adds A Dash
            Debug.Log("Dashes"); // Console Log amount of dashes
        }
    }

    private void Dash()
    {
        Debug.Log("Dashes"); // Console Log amount of dashes
        if (CantDash) return; // Returns if cannot Dash
        
        dashDB = dashDBCooldown; // Sets DB to The DB Time
        Dashes--; // Removes A Dash
        
        // Adds Force To Player When Dashing
        plrRigidBody.AddForce((isAccelerating == -1 ? -plrAcceleration : plrAcceleration) * transform.up, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (!isAlive && !hasDied)
        {
            Die();
            hasDied = true; // If Dead Return!
        }
        
        HandlePlrAcceleration(); // Accels
        HandlePlrRotationSpeed(); // Rotats
        HandleShooting(); // Pew Pews
        HandleDashDB(); // Dash DB
        
        
    }

    private void FixedUpdate() {
        if (isAlive && isAccelerating != 0) // If Alive And Accelorating
        {
            //Increase velocity
            //MovementParticle.enableEmission = true;
            MovementParticle.Emit(1);
            plrRigidBody.AddForce((isAccelerating == 1 ? plrAcceleration : -plrAcceleration) * transform.up);
            plrRigidBody.linearVelocity = Vector2.ClampMagnitude(plrRigidBody.linearVelocity, plrMaxVelocity);
        }
    }

    private void HandlePlrAcceleration(){
        isAccelerating = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0; // If Pressing W Direction = 1 Else If Holding S Direction = -1
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) Dash();
    }

    private void HandlePlrRotationSpeed() {
        if (Input.GetKey(KeyCode.A))
        {
            plrRigidBody.AddTorque(plrRotationSpeed * Time.deltaTime);
            //transform.Rotate(plrRotationSpeed * Time.deltaTime * transform.forward);
        } else if (Input.GetKey(KeyCode.D))
        {
            plrRigidBody.AddTorque(-plrRotationSpeed * Time.deltaTime);
            //transform.Rotate(-plrRotationSpeed * Time.deltaTime * transform.forward);
        }
    }

    private void HandleShooting()
    {
        //Handles Shooting Input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
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

    public void TakeDamage(float Damage)
    {
        if (!isIframes) plrHealth -= Damage;
        
        CameraShakeHandler shakeHandler = Camera.main.GetComponent<CameraShakeHandler>();
        shakeHandler.duration = 0.2f;
        shakeHandler.start = true;
    }

    private void Die()
    {
        if (!isAlive)
        {
                
            CurrentScene = SceneManager.GetActiveScene(); // Sets Current Scene Variable to Current Scene
            SceneManager.LoadScene(CurrentScene.name); // Loads The Currently Active Scene
        }
    }
}
