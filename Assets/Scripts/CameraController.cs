// Name: CameraController.cs
// Author: Connor Larsen
// Date: 07/07/2023
// Description: Control the camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Private Variables
    float speed;    // Speed the camera moves
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        // Grab the input of the player
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Move the player based on player input
        transform.position += move * speed * Time.deltaTime;

        // If the player clicks one of the snap buttons...
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = new Vector3(-17, -12, -10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = new Vector3(-84, -12, -10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = new Vector3(-143, -12, -10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.position = new Vector3(-207, -12, -10);
        }

        //if (GetComponent<Camera>().orthographicSize >= 2)
        //{
        //    GetComponent<Camera>().orthographicSize += -Input.mouseScrollDelta.y;
        //}
        //else
        //{
        //    GetComponent<Camera>().orthographicSize = 2;
        //}
    }
    #endregion
}