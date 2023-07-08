// Name: SpikeController.cs
// Author: Connor Larsen
// Date: 07/07/2023
// Description: Controls the function of the Crush Panel trap

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpikeController : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] Image cooldownImage;   // Image for the cooldown bar

    [SerializeField] float cooldown = 5f;   // Cooldown time for the trap
    #endregion

    #region Private Variables
    Animator anim;          // Reference to the animator controller of the trap

    bool cooldownActive;    // If the trap needs a cooldown
    float timer;            // Timer for the cooldown
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    // Grab the animator on the trap
        timer = cooldown;                   // Initialize timer
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= cooldown)
        {
            timer += Time.deltaTime;    // Increase timer
            cooldownImage.fillAmount = (timer / cooldown);  // Fill the progress bar
        }
        else
        {
            cooldownActive = false;
        }
    }

    // Activate the trap
    public void ActivateTrap()
    {
        if (cooldownActive == false)
        {
            anim.SetTrigger("ActivateTrap");    // Activate the trap
            timer = 0f;                         // Reset timer
            cooldownActive = true;              // Trap needs to cooldown
        }
    }
    #endregion
}