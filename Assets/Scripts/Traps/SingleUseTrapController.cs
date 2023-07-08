// Name: SingleUseTrapController.cs
// Author: Connor Larsen
// Date: 07/07/2023
// Description: Controls the function of the traps with no cooldown

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleUseTrapController : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] Button button;         // Button used to activate the trap
    [SerializeField] float cooldown = 5f;   // Cooldown time for the trap
    #endregion

    #region Private Variables
    Animator anim;  // Reference to the animator controller of the Crush Panel

    bool trapUsed;  // If the trap has been used
    float timer;    // Timer for the cooldown
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    // Grab the animator on the trap
        button.interactable = true;         // Button starts as usable
        timer = cooldown;                   // Initialize timer
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= cooldown)
        {
            timer += Time.deltaTime;    // Increase timer
        }
    }

    // Activate the trap
    public void ActivateTrap()
    {
        if (trapUsed == false)
        {
            anim.SetTrigger("ActivateTrap");    // Activate the trap
            timer = 0f;                         // Reset timer
            button.interactable = false;        // Disable the button
            trapUsed = true;                    // Trap has been used
        }
    }
    #endregion
}
