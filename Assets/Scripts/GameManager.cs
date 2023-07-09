// Name: GameManager.cs
// Author: Connor Larsen
// Date: 07/07/2023
// Description: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Public and Serialized Variables
    [Header("Audio")]
    [SerializeField] AudioClip menuSelect;  // Sound used when clicking a menu button
    [SerializeField] AudioClip trapSelect;  // Sound used when clicking a trap button
    [SerializeField] AudioClip pauseGame;   // Sound used when pausing the game

    [Header("Controls")]
    [SerializeField] KeyCode pauseButton = KeyCode.Escape;  // Reference to the key responsible for pausing the game

    [Header("Game Systems")]
    public int maxRunners = 100;    // Maximum number of runners that can spawn in the level

    [Header("UI")]
    [SerializeField] GameObject gameplayUI; // UI Screen for gameplay
    [SerializeField] GameObject pauseUI;    // UI Screen for when the game is paused
    [SerializeField] GameObject gameOverUI; // UI Screen for loss condition
    [SerializeField] GameObject victoryUI;  // UI Screen for win condition
    [SerializeField] Text runnerCountUI;    // UI element for the runner count
    [SerializeField] Text runnerKillsUI;    // UI element for the runner kills
    [SerializeField] Text runnerGoalsUI;    // UI element for the runner escapes

    [Header("Results Messages")]
    [SerializeField] Text gameOverMessage;  // UI Element for the game over message
    [SerializeField] Text victoryMessage;   // UI Element for the victory message

    // Hidden from inspector
    [HideInInspector] public enum UIScreens { GAME, PAUSE, GAMEOVER, VICTORY, NONE };   // Enum types for each type of screen
    [HideInInspector] public UIScreens currentScreen;                                   // Reference to the currently active screen

    [HideInInspector] public int runnerCount;       // Number of runners that will spawn
    [HideInInspector] public int runnerKillCount;   // Number of runners killed
    [HideInInspector] public int runnerGoalCount;   // Number of runners that reached the goal
    #endregion

    #region Private Variables
    private bool isPaused = false;  // If the game is paused or not
    #endregion

    #region Functions
    // Awake is called on the first possible frame
    void Awake()
    {
        ResetGame();    // Start a fresh game
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pause the game
        if (Input.GetKeyDown(pauseButton))
        {
            // Only function if in GAME or PAUSE state
            if (currentScreen == UIScreens.GAME || currentScreen == UIScreens.PAUSE)
            {
                // If game is paused...
                if (isPaused)
                {
                    ResumeGame();   // Resume the game
                }
                else
                {
                    PauseGame();    // Pause the game
                }
            }
        }
    }

    // Call this function to pause the game
    public void PauseGame()
    {
        // Play pause game audio
        GetComponent<AudioSource>().clip = pauseGame;
        GetComponent<AudioSource>().Play();

        // Switch to the pause screen
        UISwitch(UIScreens.PAUSE);

        // Pause game time
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Call this function to unpause the game
    public void ResumeGame()
    {
        // Play button click audio
        GetComponent<AudioSource>().clip = menuSelect;
        GetComponent<AudioSource>().Play();

        // Switch to the game screen
        UISwitch(UIScreens.GAME);

        // Resume game time
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Call this function to reinitialize the game
    public void ResetGame()
    {
        // Reset variables
        runnerCount = maxRunners;
        runnerKillCount = 0;
        runnerGoalCount = 0;

        // Reset the UI
        UISwitch(UIScreens.GAME);
        Time.timeScale = 1f;
        isPaused = false;
        UpdateRunnerSpawnCount();
        UpdateRunnerKillCount();
        UpdateRunnerGoalCount();
    }

    // Call this function to return to the title screen
    public void ReturnToTitle()
    {
        // Play button click audio
        GetComponent<AudioSource>().clip = menuSelect;
        GetComponent<AudioSource>().Play();

        // Reset time scale
        Time.timeScale = 1f;

        // Load the title screen scene
        SceneManager.LoadScene("TitleScreen");
    }

    // Function for enabling and disabling UI screens
    public void UISwitch(UIScreens screen)
    {
        // If the game screen is selected...
        if (screen == UIScreens.GAME)
        {
            // Activate selected screen
            gameplayUI.SetActive(true);

            // Disable all other screens in the scene
            pauseUI.SetActive(false);
            gameOverUI.SetActive(false);
            victoryUI.SetActive(false);

            // Set currentScreen to the selected screen
            currentScreen = UIScreens.GAME;
        }

        // If the pause screen is selected...
        else if (screen == UIScreens.PAUSE)
        {
            // Activate selected screen
            pauseUI.SetActive(true);

            // Disable all other screens in the scene
            gameplayUI.SetActive(false);
            gameOverUI.SetActive(false);
            victoryUI.SetActive(false);

            // Set currentScreen to the selected screen
            currentScreen = UIScreens.PAUSE;
        }

        // If the game over screen is selected...
        else if (screen == UIScreens.GAMEOVER)
        {
            // Activate selected screen
            gameOverUI.SetActive(true);

            // Disable all other screens in the scene
            gameplayUI.SetActive(false);
            pauseUI.SetActive(false);
            victoryUI.SetActive(false);

            // Set currentScreen to the selected screen
            currentScreen = UIScreens.GAMEOVER;

            // Print the goober message
            gameOverMessage.text = "You failed to kill at least 80% of the goobers!\r\n\r\nYou killed " + runnerKillCount + " goobers (" + Mathf.InverseLerp(0, maxRunners, runnerKillCount) * 100 +"%)";
        }

        // If the victory screen is selected...
        else if (screen == UIScreens.VICTORY)
        {
            // Activate selected screen
            victoryUI.SetActive(true);

            // Disable all other screens in the scene
            gameplayUI.SetActive(false);
            pauseUI.SetActive(false);
            gameOverUI.SetActive(false);

            // Set currentScreen to the selected screen
            currentScreen = UIScreens.VICTORY;

            // Print the goober message
            victoryMessage.text = "You managed to kill at least 80% of the goobers!\r\n\r\nYou killed " + runnerKillCount + " goobers (" + Mathf.InverseLerp(0, maxRunners, runnerKillCount) * 100 + "%)";
        }
    }

    // Checks if all runners have completed
    public void CheckCompletion()
    {
        // Check if last runner
        if ((runnerKillCount + runnerGoalCount) == maxRunners)
        {
            // If player killed 80%...
            if (((Mathf.InverseLerp(0, maxRunners, runnerKillCount)) >= 0.8f))
            {
                UISwitch(UIScreens.VICTORY);    // Swap to victory screen
            }
            else
            {
                UISwitch(UIScreens.GAMEOVER);   // Swap to game over screen
            }

            Time.timeScale = 0f;
        }
    }

    // Updates the runner spawn count UI
    public void UpdateRunnerSpawnCount()
    {
        runnerCountUI.text = " " + runnerCount + " ";
    }

    // Updates the runner kill count UI
    public void UpdateRunnerKillCount()
    {
        runnerKillsUI.text = "Runners Killed: " + runnerKillCount;
    }

    // Updates the runner goal count UI
    public void UpdateRunnerGoalCount()
    {
        runnerGoalsUI.text = "Runners Escaped: " + runnerGoalCount;
    }
    #endregion
}