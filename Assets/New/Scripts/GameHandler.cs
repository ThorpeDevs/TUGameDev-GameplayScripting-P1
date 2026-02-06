using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public AAAAAAAAAAAAA Rock;
    
    public int RockCount = 0;

    private int level = 0;

    // Update is called once per frame
    private void Update()
    {
        // Returns function if no rock count
        if (RockCount > 0) return;
        
        // Increases the Difficulty and Spawn Level
        level ++;

        int numRock = 2 + (2 * level);
        for (int i = 0; i < numRock; i++)
        {
            SpawnRock();
        }
    }

    private void SpawnRock()
    {
        // How Far Long Along Edge
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;
        
        // Which Edge
        int edge = Random.Range(0, 4);
        if (edge == 0)
        {
            viewportSpawnPosition = new Vector2(offset, 0);
        } else if (edge == 1){
            viewportSpawnPosition = new Vector2(offset, 1);
        } else if (edge == 2) {
            viewportSpawnPosition = new Vector2(0, offset);
        } else if (edge == 3) { 
            viewportSpawnPosition = new Vector2(1, offset);
        }
        
        // Create a rock!
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        AAAAAAAAAAAAA newRock = Instantiate(Rock, worldSpawnPosition, Quaternion.identity);
        newRock.gameHandler = this;
    }
}
