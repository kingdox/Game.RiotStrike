#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// StateManager class
/// manages the status of the game
///<summary>
public class StateManager : MonoBehaviour
{
    #region Variables

    [Header("State Manager")]
    private EState state = EState.Play;
    #endregion
    #region Event
    private void Awake() {
        
    }
    private void Update() {

        StateSettings();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Set the state actual of the game
    /// </summary>
    private void StateSettings() {

        switch (state) {
            case EState.Pause:
                Time.timeScale = 0;
                break;
            case EState.Play:
                Time.timeScale = 1;
                break;
            case EState.End:
                Time.timeScale = 0;
                break;
        }
    }
    /// <summary>
    /// Changes the state and do some modifications based on the last state
    /// </summary>
    private void StateChange(EState state) {
        switch (state) {
            case EState.Pause:
                break;
            case EState.Play:
                break;
            case EState.End:
                break;
        }
        //TODO
    }

    /// <summary>
    /// Look if the time scale is stopped
    /// </summary>
    public bool isStopped => Time.timeScale.Equals(0);
    #endregion
}
