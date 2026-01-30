using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PewPewHandler : MonoBehaviour
{
    private Vector3 mousePos;

    void Update() // https://www.youtube.com/watch?v=-bkmPm_Besk
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Finds Mouse Point On Map

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // Creates The Z Axis Roation

        transform.rotation = Quaternion.Euler(0,0,rotZ - 90);
    }
}
