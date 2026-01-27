using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    public Rigidbody2D plrRigidBody;
    public float PlrSpeed;
    public float D_PlrJumpsAvaliable;
    public float PlrNormalSpeed;

    private Vector2 InputPower;
    private float PlrJumpsAvaliable;

    void Start()
    {
        PlrJumpsAvaliable = D_PlrJumpsAvaliable;
    }

    void Update()
    {
        //if (InputPower != null)
        //{
        //    Debug.Log(InputPower.ToString());
        //}

        if (plrRigidBody.IsSleeping())
        {
            PlrJumpsAvaliable = D_PlrJumpsAvaliable;
        }
    }

    public void OnMove(InputValue Value)
    {
        InputPower = Value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (PlrJumpsAvaliable > 0)
        {
            PlrJumpsAvaliable -= 1;
            plrRigidBody.AddForce(InputPower * PlrSpeed);
            Debug.Log(InputPower * PlrSpeed); // Log Inputpower
        }
    }
}