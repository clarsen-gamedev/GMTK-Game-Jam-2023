// Name: TrapController.cs
// Author: Connor Larsen
// Date: 07/07/2023
// Description: Controls the function of the traps

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TrapController : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] AudioClip sound;       // Audio clip for the sound of the trap
    [SerializeField] Button button;         // Button used to activate the trap
    [SerializeField] Image cooldownImage;   // Image for the cooldown bar

    [SerializeField] float cooldown = 5f;   // Cooldown time for the trap
    #endregion

    #region Private Variables
    Animator anim;          // Reference to the animator controller of the Crush Panel

    bool cooldownActive;    // If the trap needs a cooldown
    float timer;            // Timer for the cooldown
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
            timer += Time.deltaTime;                        // Increase timer
            cooldownImage.fillAmount = (timer / cooldown);  // Fill the progress bar
        }
        else
        {
            cooldownActive = false;     // Cooldown is completed
            button.interactable = true; // Enable the button
        }
    }

    // Activate the trap
    public void ActivateTrap()
    {
        if (cooldownActive == false)
        {
            anim.SetTrigger("ActivateTrap");            // Activate the trap
            timer = 0f;                                 // Reset timer
            button.interactable = false;                // Disable the button
            cooldownActive = true;                      // Trap needs to cooldown
        }
    }

    // Play trap sound
    public void PlaySound()
    {
        GetComponent<AudioSource>().clip = sound;   // Load sound
        GetComponent<AudioSource>().Play();         // Play sound
    }


    #endregion
}