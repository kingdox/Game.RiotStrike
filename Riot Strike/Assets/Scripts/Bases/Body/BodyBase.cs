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
/// Base of every body base (ally or enemy) in game
/// </summary>
public abstract class BodyBase : MonoBehaviour
{
    #region Variables
    [Header("Character")]
    public StatData stat;
    public int life;
    public bool isDead;
    //[Space]
    //public Character character;TODO
    #endregion
    #region Event

    #endregion
    #region Method
    //TODO
    private void InvokeReload() { }
    private void InvokeAttack() { }
    private void InvokeFocus() { }
    private void InvokeSpell() { }

    protected virtual void Death() { }
    #endregion
}
