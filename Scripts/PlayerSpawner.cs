using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab of the player object

    void Start()
    {
        // Spawn the player at the randomly chosen location
        Instantiate(playerPrefab, SelectSpawnLocation(), Quaternion.identity);
    }

    public static Vector3 SelectSpawnLocation()
    {
        // Define the bounds of the spawn area
        float minX = -200f;
        float maxX = 200f;
        float minZ = -200f;
        float maxZ = 200f;

        // Generate random spawn position within the defined bounds
        float spawnX = Random.Range(minX, maxX);
        float spawnZ = Random.Range(minZ, maxZ);

        // Set the spawn position
        Vector3 spawnPosition = new Vector3(spawnX, 1, spawnZ);

        return spawnPosition;
    }


}
