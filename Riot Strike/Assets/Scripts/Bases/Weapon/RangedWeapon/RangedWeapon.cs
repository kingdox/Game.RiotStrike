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
    public float bulletSpeed;
    public Action<Bullet> OnShot;
    #endregion
    #region Events

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(tr_hotSpot.position, HotSpotInWorld);

    }
    #endregion
    #region Methods

    /// <summary>
    /// Returns the information of the hotspot based on the world
    /// </summary>
    public Vector3 HotSpotInWorld => tr_hotSpot.position + tr_hotSpot.forward;

    /// <summary>
    /// Shot the weapon with the bullet prefab
    /// </summary>
    public override void Attack(Body body){
        if (!CanAtack()) return; // 🛡
        base.Attack(body);

        Shot(body, pref_bullet);
    }

    /// <summary>
    /// Do a shot
    /// </summary>
    public Bullet Shot(Body body, GameObject pref)
    {


        //takes the bullet
        Instantiate(
            pref, 
            tr_hotSpot.position, 
            Quaternion.Euler(body.tr_head.forward)
             //Quaternion.LookRotation(body.tr_head.forward)
           // Quaternion.identity
            ).transform
           .Component(out Bullet bullet);



        //TODO
        bullet.transform.forward = body.tr_head.forward;

        Transform _parent = TargetManager.Exist
            ? TargetManager.GetParentTemporalElements
            : null
        ;

        bullet.transform.SetParent(_parent);

        //Settings data
        bullet.speed = bulletSpeed;
        bullet.damage = body.stat.RealStrength;
        bullet.tag = tag;

        bullet.OnImpact += EmitTargetImpactWeapon;

        // emits the init of the shot
        OnShot?.Invoke(bullet);

        return bullet;
    }
    #endregion
}
 