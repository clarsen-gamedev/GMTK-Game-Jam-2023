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
    Vector3 climb;              // Direction the runner climbs

    bool climbingLadder;    // If climbing a ladder
    float speed;            // Speed of the runner
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        move = new Vector3(-1, 0);      // Move runner left along x-axis
        climb = new Vector3(0, 1);      // Move runner up along y-axis
        speed = Random.Range(3, 10);    // Pick a random speed for the runner to move at
        climbingLadder = false;         // Runner starts off ladder
    }

    // Update is called once per frame
    void Update()
    {
        // Move runner forward when grounded
        if (GetComponent<Rigidbody2D>().velocity.y == 0 && climbingLadder == false)
        {
            transform.position += move * speed * Time.deltaTime;    // Move the runner left
        }

        // Move runner up when climbing
        else if (climbingLadder == true)
        {
            transform.position += climb * 2 * Time.deltaTime;   // Move the up the ladder
        }
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

        // If a runner hits a jump trigger
        else if (collision.tag == "Jump")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5, ForceMode2D.Impulse);
        }

        // If a runner hits a ladder trigger
        else if (collision.tag == "Ladder")
        {
            climbingLadder = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            Debug.Log("Ladder");
        }

        // If a runner hits a ladder jump trigger
        else if (collision.tag == "LadderJump")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 2, ForceMode2D.Impulse);
            climbingLadder = false;
        }

        // If a runner hits a stair jump trigger
        else if (collision.tag == "Stair")
        {
            transform.position += new Vector3(0, 2f); // Move runner up
            transform.position += new Vector3(-0.5f, 0); // Move runner left
        }
    }
    #endregion
}