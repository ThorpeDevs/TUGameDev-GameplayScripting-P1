using UnityEngine;
using UnityEngine.SceneManagement;

public class AAAAAAAAAAAAA : MonoBehaviour
{
    public Rigidbody2D RockRigidBody;
    private Scene CurrentScene; // Holds The Current Scene
    private Vector2 Force;

    private void Start() // NEED TO USE THIS https://www.youtube.com/watch?v=wzQ9Xn406wc
    {
        Force = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
    }

    private void Update()
    {
        RockRigidBody.AddRelativeForce(Force * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Checks to see if player collided with GameObject
        {
            CurrentScene = SceneManager.GetActiveScene(); // Sets Current Scene Variable to Current Scene
            SceneManager.LoadScene(CurrentScene.name); // Loads The Currently Active Scene
        }
        else if (other.CompareTag("Bullet")) // Checks to see if bullet collided with GameObject
        {
            Destroy(other.gameObject); // Destroys The Bullet GameObject
            Destroy(gameObject); // Destroys This Game Object
        }
    }
}
