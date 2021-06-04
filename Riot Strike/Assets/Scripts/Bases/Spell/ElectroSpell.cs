#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
/// <summary>
/// Spell who doubles the damage for a short time
/// </summary>
public class ElectroSpell : Spell
{
    #region Variable

    #endregion
    #region Event

    #endregion
    #region Method
    /// <summary>
    /// Do the heal cast
    /// </summary>
    public override void Cast(Body body)
    {
        if (!CanCast()) return; // 🛡
        "CAST!".Print("blue");
    }
    #endregion
}
