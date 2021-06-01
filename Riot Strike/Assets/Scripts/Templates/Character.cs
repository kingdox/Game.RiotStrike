#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
# endregion

/// <summary>
/// Information of a character and their own elements
/// </summary>
[CreateAssetMenu(menuName = "Template/Character")]
//[RequireComponent(typeof(BodyBase))]
public class Character : ScriptableObject
{
    #region Variable
    [Header("Character")]
    public Weapon weapon;
    [Space]
    public string idStat;
    public string idWeapon;
    public string idSpell;

    #endregion
    #region Method
    #endregion
}
