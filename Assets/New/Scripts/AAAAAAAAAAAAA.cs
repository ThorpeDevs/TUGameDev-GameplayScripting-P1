using UnityEngine;
using UnityEngine.SceneManagement;

public class AAAAAAAAAAAAA : MonoBehaviour
{
    public GameHandler gameHandler;
    public Rigidbody2D RockRigidBody;
    private Scene CurrentScene; // Holds The Current Scene
	public int AdditionalSpeed = 0; // Additional Speed Added To The Rock
	public int Size = 3; // Size of the Rock

    private void Start() // NEED TO USE THIS https://www.youtube.com/watch?v=wzQ9Xn406wc
    {
        // Scale
		transform.localScale = 0.5f * Size * Vector3.one; // Scales the rock based on the size variable
        
		// Force
		Vector2 direction = new Vector2(Random.value, Random.value).normalized;
		float spawnspeed = Random.Range(4f - Size, 5f - Size);
		RockRigidBody.AddForce(direction * spawnspeed * AdditionalSpeed, ForceMode2D.Impulse); // Adds the force to the rock's rigidbody
        
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
