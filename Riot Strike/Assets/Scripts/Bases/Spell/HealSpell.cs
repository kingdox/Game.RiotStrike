#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
# endregion
/// <summary>
/// Heal the allies and yourself
/// </summary>
public class HealSpell : Spell
{
    #region Variable

    #endregion
    #region Event

    #endregion
    #region Method
    /// <summary>
    /// Do the heal cast
    /// </summary>
    public override void Cast(){
        if (!CanCast) return; // 🛡
        "CAST!".Print();
    }
    #endregion
}
