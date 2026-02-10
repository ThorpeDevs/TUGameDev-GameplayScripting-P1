using UnityEngine;
using System.Collections;

public class CameraShakeHandler : MonoBehaviour
{
    public bool start = false; // Start Shaking?
    public AnimationCurve curve; // Animation Curve For Smoothing
    public float duration = 0f; // How Long For?
    
    void Update() // Ran every Frame
    {
        if (start) // If started
        {
            start = false; // Set Started To False
            StartCoroutine(Shaking()); // Start ScreenShaking on a new thread
        }
    }
    IEnumerator Shaking()
    {
        Vector3 startPos = transform.position; // Get start position
        float elapsedTime = 0f; // How long shaking for
        while (elapsedTime < duration) // If Elapsed Time is Less Than duration
        {
            elapsedTime += Time.deltaTime; // Add Real (delta) Time To Elapsed Time
            float str = curve.Evaluate(elapsedTime / duration); // Adds Smoothness To The Shaking
            transform.position = startPos + Random.insideUnitSphere * str; // Position = Start position + Random 
            yield return null; // Return Null
        }
        
        transform.position = startPos; // Set back To Start Position
    }
}
