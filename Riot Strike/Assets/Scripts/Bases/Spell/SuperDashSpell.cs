#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
/// <summary>
/// Do a dash jump forward
/// </summary>
public class SuperDashSpell : Spell
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
