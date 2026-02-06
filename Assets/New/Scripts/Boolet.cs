using System;
using UnityEngine;

public class Boolet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 1f;
    
    // When Created, Destroy after a little bit!
    private void Awake()
    {
        Destroy(gameObject, bulletLifetime);
    }
}
