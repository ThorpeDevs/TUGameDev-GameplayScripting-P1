using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PewPewHandler : MonoBehaviour
{
    private Vector3 mousePos;
    public GameObject Bullet;
    public Transform bulletTrans;
    public bool canShoot;
    private float timer;
    public float timeBetweenFiring;

    void Update() // https://www.youtube.com/watch?v=-bkmPm_Besk
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Finds Mouse Point On Map

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // Creates The Z Axis Roation

        transform.rotation = Quaternion.Euler(0,0,rotZ - 90);

        if (!canShoot) // Resets Firing Timer
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring ) {
                canShoot = true;
                timer = 0;
        }

        if (Input.GetMouseButton(0) && canShoot) // Instances Shooting if can fire
        {
            canShoot = false;
            Instantiate(Bullet, bulletTrans.position, Quaternion.identity);
        }
    }
}
