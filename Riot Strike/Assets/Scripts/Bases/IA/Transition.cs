#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// Transition
///</summary>
[System.Serializable]
public class Transition 
{
    #region Variables
    [SerializeField] private string description;
    public Decision decision;
    public State trueState;
    public State falseState;

#endregion
}
