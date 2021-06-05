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
public abstract class RangedWeapon : Weapon
{
    #region Variables
    [Header("Ranged Weapon")]
    [SerializeField] private RefreshController refresh;
    [SerializeField] private GameObject pref_bullet;
    [SerializeField] private Transform tr_hotSpot;
    public Action<Bullet> OnShot;
    //public
    #endregion
    #region Events

    #endregion
    #region Methods


    /// <summary>
    /// Shot the weapon with the bullet prefab
    /// </summary>
    public override void Attack(int damage){
        base.Attack(damage);

        //takes the bullet
        //TODO, hacer que apunte desde el hotspot hasta el centro de la cámara
         Instantiate(pref_bullet, tr_hotSpot)
            .transform
            .Component(out Bullet bullet);
        bullet.transform.forward = tr_hotSpot.forward;
        bullet.transform.SetParent(null);

        bullet.damage = damage;
        bullet.tag = tag;

        OnShot?.Invoke(bullet);

    }

    #endregion
}
