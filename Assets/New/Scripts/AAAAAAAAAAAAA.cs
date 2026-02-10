using UnityEngine;
using UnityEngine.SceneManagement;

public class AAAAAAAAAAAAA : MonoBehaviour
{
    [SerializeField] private ParticleSystem DestructionParticles;
    public Rigidbody2D RockRigidBody;
	public int AdditionalSpeed = 0; // Additional Speed Added To The Rock
	public int Size = 3; // Size of the Rock
    public float damage = 2.5f; // was 2.5f

    private SpriteRenderer SprRen;

    public GameHandler gameHandler;

    private void Start() // NEED TO USE THIS https://www.youtube.com/watch?v=wzQ9Xn406wc
    {
        SprRen = GetComponent<SpriteRenderer>();

        // Scale
		transform.localScale = 0.5f * Size * Vector3.one; // Scales the rock based on the size variable
        
		// Force
		Vector2 direction = new Vector2(Random.value, Random.value).normalized;
		float spawnspeed = Random.Range(4f - Size, 5f - Size);
		RockRigidBody.AddForce(direction * spawnspeed * AdditionalSpeed, ForceMode2D.Impulse); // Adds the force to the rock's rigidbody

        // damage *= (float)Size;// + (gameHandler.level / 100f);
        Debug.Log($"{Size}, {damage}");
        
        gameHandler.RockCount++;
    }

    private void SplitRock(bool isBullet = true) {
        gameHandler.RockCount--;
        if (Size > 1) {
            for (int i = 0; i < 2; i++)
            {
                AAAAAAAAAAAAA newRock = Instantiate(this, transform.position, Quaternion.identity);
                newRock.Size = Size - 1;
                newRock.gameHandler = gameHandler;
            }
        }

        if (isBullet) gameHandler.HighScore += Size * 100;// If a bullet Hit Rock Add HighScore
        else gameHandler.HighScore -= 500 * Size; // Removes Highscore If Hit
        
        Destroy(gameObject); // Destroys This Game Object
        ParticleSystem particle = Instantiate(DestructionParticles, transform.position, Quaternion.identity);
        particle.startColor = SprRen.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Checks to see if player collided with GameObject
        {
            PlayerHandler2 player = other.GetComponent<PlayerHandler2>();
            Debug.Log(damage);
            player.TakeDamage(damage);
            SplitRock(false);
        }
        else if (other.CompareTag("Bullet")) // Checks to see if bullet collided with GameObject
        {
            Destroy(other.gameObject); // Destroys The Bullet GameObject
            SplitRock(true);
        }
    }
}
