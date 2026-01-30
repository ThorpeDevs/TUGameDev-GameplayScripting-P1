using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerHandler2 : MonoBehaviour
{
    public Rigidbody2D plrRigidBody; // Rigidbody of Player Character
    public float PlrSpeed; // General Speed To Multiply By

    private Vector2 InputPower; // Movement Direction
    private float TotalSpeed; // Total Speed After DeltaTime

    void Start() // Fires On Initalisation
    {
        
    }

    void Update() // Fires Every Frame
    {
        

        TotalSpeed = PlrSpeed * Time.deltaTime; // Multiplies Speed by DeltaTime to fix FPS related speed issues!

        plrRigidBody.AddForce(InputPower * TotalSpeed); // Applies Speed and Direction to Rigidbody
    }

    public void OnMove(InputValue Value) // Fires On Movement Input Event (W/A/S/D)
    {
        InputPower = Value.Get<Vector2>(); // Gets Movement Direction From Unity New Input System
    }

    public void OnJump() // Fires On "Jump" (Space)
    {
        
    }
}