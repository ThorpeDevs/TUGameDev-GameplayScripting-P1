using UnityEngine;
using UnityEngine.SceneManagement;

public class AAAAAAAAAAAAA : MonoBehaviour
{
    private Scene CurrentScene; // Holds The Current Scene

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
