#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
/// <summary>
/// Weapon Katana, 
/// </summary>
public class KatanaWeapon : NearWeapon
{
    #region Variables

    #endregion
    #region Events

    #endregion
    #region Methods
    public override void Attack(int damage)
    {
        if (!CanAtack()) return; // 🛡
        base.Attack(damage);

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
