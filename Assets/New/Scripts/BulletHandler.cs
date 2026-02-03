using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() // https://www.youtube.com/watch?v=-bkmPm_Besk
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        Invoke("Destroy", 4);
    }

    // Update is called once per frame
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
