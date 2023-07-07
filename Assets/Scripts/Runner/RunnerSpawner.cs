// Name: RunnerSpawner.cs
// Author: Connor Larsen
// Date: 07/07/2023
// Description: Controls the spawning of the runners in the level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawner : MonoBehaviour
{
    #region Serialized Variables
    // Visible
    [SerializeField] GameObject runner; // Reference to the runner prefab

    [SerializeField] float minTime;     // Minimum wait time between spawns
    [SerializeField] float maxTime;     // Maximum wait time between spawns
    #endregion

    #region Private Variables
    GameManager gameManager;    // Reference to the Game Manager game object

    bool isSpawning;            // Check if the spawner is spawning a runner
    float spawnTimer;           // Time between runner spawns
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        isSpawning = false; // Initialize isSpawning variable
    }

    // Update is called once per frame
    void Update()
    {
        // If spawner is not currently in use...
        if (isSpawning == false && gameManager.runnerCount > 0)
        {
            SpawnRunner();                                  // Spawn a runner
            spawnTimer = Random.Range(minTime, maxTime);    // Set a random timer
            isSpawning = true;                              // Set isSpawning to true
        }

        // If spawner has been used...
        else if (isSpawning == true)
        {
            if (spawnTimer >= 0)
            {
                spawnTimer -= Time.deltaTime;   // Countdown timer
            }

            else
            {
                isSpawning = false; // Spawner can be used again
            }
        }
    }

    // Spawns a runner
    void SpawnRunner()
    {
        GameObject newRunner = Instantiate(runner, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);    // Spawn the runner
        gameManager.runnerCount--;                                                                                                      // Decrease runner count on spawn
        gameManager.UpdateRunnerCount();                                                                                                // Update the runner count
    }
    #endregion
}