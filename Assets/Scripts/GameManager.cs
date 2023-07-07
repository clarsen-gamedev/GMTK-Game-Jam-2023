// Name: GameManager.cs
// Author: Connor Larsen
// Date: 07/07/2023
// Description: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Public and Serialized Variables
    [Header("UI")]
    [SerializeField] Text runnerCountUI;    // UI element for the runner count

    [Header("Game Systems")]
    public int runnerCount = 100;   // Maximum number of runners that can spawn in the level
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRunnerCount()
    {
        runnerCountUI.text = "Runners Left: " + runnerCount;
    }
    #endregion
}