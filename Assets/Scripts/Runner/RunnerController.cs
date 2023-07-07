// Name: RunnerController.cs
// Author: Connor Larsen
// Date: 07/07/2023
// Description: Script which controls the runners' actions

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    #region Private Variables
    GameManager gameManager;    // Reference to the Game Manager game object
    Vector3 move;               // Direction the runner moves in

    float speed;    // Speed of the runner
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        move = new Vector3(-1, 0);      // Move runner along right along x-axis
        speed = Random.Range(3, 6);     // Pick a random speed for the runner to move at
    }

    // Update is called once per frame
    void Update()
    {
        // Move runner forward
        transform.position += move * speed * Time.deltaTime;    // Move the obstacle down the path
    }

    // Check what the runner collides with
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If a runner hits the goal...
        if (collision.tag == "Goal")
        {
            Destroy(gameObject);
            Debug.Log("Runner made it to the end");
        }

        // If a runner hits a hazard...
        else if (collision.tag == "Hazard")
        {
            Destroy(gameObject);
            Debug.Log("Runner was killed by hazard");
        }

        // If a ruunner hits a jump trigger
        else if (collision.tag == "Jump")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 2, ForceMode2D.Impulse);
        }
    }
    #endregion
}