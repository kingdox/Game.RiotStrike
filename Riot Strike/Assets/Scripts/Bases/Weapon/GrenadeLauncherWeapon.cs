﻿#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
/// <summary>
/// Weapon who throws grenades
/// </summary>
public class GrenadeLauncherWeapon : Weapon
{
    #region Variables

    #endregion
    #region Events

    #endregion
    #region Methods
    public override void Attack()
    {
        if (!CanAtack()) return; // 🛡
        base.Attack();

        "ATTACK".Print("red");
    }
    public override void Aim()
    {
        base.Aim();
        "AIM".Print("yellow");

    }
    public override void Reload()
    {
        base.Reload();
        "RELOAD".Print("red");
    }
    #endregion
}
