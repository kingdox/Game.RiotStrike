#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
# endregion
/// <summary>
/// Weapons who gets a gun behaviour
/// </summary>
public  class RangedWeapon : Weapon
{
    #region Variables
    [Header("Ranged Weapon")]
    [SerializeField] private RefreshController refresh;
    [SerializeField] private GameObject pref_bullet = null;
    [SerializeField] private Transform tr_hotSpot = null;
    public Action<Bullet> OnShot;

    #endregion
    #region Events
  
    #endregion
    #region Methods


    /// <summary>
    /// Shot the weapon with the bullet prefab
    /// </summary>
    public override void Attack(Body body){
        if (!CanAtack()) return; // 🛡
        base.Attack(body);

        Shot(body);
    }

    /// <summary>
    /// Do a shot
    /// </summary>
    private void Shot(Body body)
    {
        //takes the bullet
        Instantiate(pref_bullet, tr_hotSpot)
           .transform
           .Component(out Bullet bullet);
        bullet.transform.forward = body.tr_head.forward;
        bullet.transform.SetParent(null); // todo no dejar fuera en la jerarquía


        //Settings data
        bullet.damage = body.stat.STRENGHT;
        bullet.tag = tag;

        bullet.OnImpact += EmitTargetImpactWeapon;

        // emits the init of the shot
        OnShot?.Invoke(bullet);
    }
    #endregion
}
 