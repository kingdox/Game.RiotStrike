#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
/// <summary>
/// Spell to jump so high and evade fall damage
/// </summary>
public class AntiGravityJumpSpell : Spell
{
    #region Variable

    #endregion
    #region Event

    #endregion
    #region Method
    /// <summary>
    /// Do the heal cast
    /// </summary>
    public override void Cast()
    {
        if (!CanCast()) return; // 🛡
        "CAST!".Print("blue");
    }
    #endregion
}
