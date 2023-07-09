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
    #region Public and Serialized Variables
    public bool isDead;             // If the runner is dead
    [SerializeField] Animator anim; // Reference to the animator for the sprite
    #endregion

    #region Private Variables
    GameManager gameManager;    // Reference to the Game Manager game object
    Vector3 move;               // Direction the runner moves in
    Vector3 climb;              // Direction the runner climbs

    bool isWalking;         // If the runner is walking
    bool climbingLadder;    // If climbing a ladder
    bool skipLadder;        // Whether or not the runner skips the ladder check
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
        isWalking = false;              // Runner starts off idle
        isDead = false;                 // Runner starts off alive
        climbingLadder = false;         // Runner starts off ladder

        // Do they skip the ladder?
        int chance = Random.Range(1, 10);
        if (chance <= 2)
        {
            skipLadder = true;
        }
        else
        {
            skipLadder = false;
        }

        // Grab reference to the game manager and animator
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move runner forward when grounded
        if (GetComponent<Rigidbody2D>().velocity.y == 0 && climbingLadder == false && isDead == false)
        {
            anim.SetBool("isWalking", true);
            transform.position += move * speed * Time.deltaTime;    // Move the runner left
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (isDead)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    // Check what the runner collides with
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If a runner hits the goal...
        if (collision.tag == "Goal")
        {
            Destroy(gameObject);
            gameManager.runnerGoalCount++;
            gameManager.UpdateRunnerGoalCount();
            gameManager.CheckCompletion();
        }

        // If a runner hits a hazard...
        else if (collision.tag == "Hazard")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetTrigger("Death");
            gameManager.runnerKillCount++;
            gameManager.UpdateRunnerKillCount();
            gameManager.CheckCompletion();
        }

        // If a runner hits a jump trigger
        else if (collision.tag == "Jump")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5, ForceMode2D.Impulse);
            anim.SetTrigger("Jumping");
        }

        // If a runner hits a ladder trigger
        else if (collision.tag == "Ladder")
        {
            if (skipLadder == false)
            {
                // Climb the ladder
                climbingLadder = true;
                GetComponent<Rigidbody2D>().gravityScale = 0f;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                anim.SetTrigger("Climbing");
            }
        }

        // If a runner hits a ladder jump trigger
        else if (collision.tag == "LadderJump")
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 2, ForceMode2D.Impulse);
            climbingLadder = false;
            anim.SetTrigger("Jumping");
        }

        else if (collision.tag == "LadderDown")
        {
            anim.SetTrigger("Climbing");
        }

        // If a runner hits a stair jump trigger
        else if (collision.tag == "Stair")
        {
            transform.position += new Vector3(0, 2f); // Move runner up
            transform.position += new Vector3(-0.5f, 0); // Move runner left
        }

        else if (collision.tag == "Launcher")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 150, ForceMode2D.Impulse);
        }
    }
    #endregion
}