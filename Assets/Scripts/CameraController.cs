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