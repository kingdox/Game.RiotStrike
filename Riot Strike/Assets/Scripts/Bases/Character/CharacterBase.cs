#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dat = Environment.Data;
using XavHelpTo.Change;
using XavHelpTo;
using XavHelpTo.Get;
# endregion

/// <summary>
/// Base of every character base (ally or enemy) in game
/// </summary>
public abstract class CharacterBase : ScriptableObject
{
    #region Variables
    [Header("Character")]
    public StatData stat;
    public string statID;
    [Space]
    public bool isDead;
    #endregion
    #region Method


    /// <summary>
    /// the character receive a qty of damage
    /// </summary>
    private void Damage(float qty)
    {
        //TODO hacer manejo de vida
        if (isDead)
        {

        }
    }

    #endregion
}
