#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
using XavHelpTo.Set;
using Dat = Environment.Data;
# endregion
/// <summary>
/// Information of the weapon
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    #region Variable
    private WeaponData weaponData;
    private bool isReloading = false;
    private float cadenceCount = 0;
    private bool flag_canAttack = false;
    private int currentAmmo;
    [Header("Weapon")]
    public string ID = "0";
    public Action<int,int> OnFireAttack;
    public Action<float, float> OnReload;
    #endregion
    #region Events
    private void Start()
    {
        weaponData = Dat.GetWeaponData(ID);
        currentAmmo = weaponData.AMMO;
        EmitWeapon();
        EmitReload(0);
    }
    private void Update()
    {
        weaponData.CADENCE.TimerFlag(ref flag_canAttack, ref cadenceCount);
    }
    #endregion
    #region Method
    /// <summary>
    /// Emit the weapon status
    /// </summary>
    private void EmitWeapon() => OnFireAttack?.Invoke(currentAmmo, weaponData.AMMO);
    private void EmitReload(float count) => OnReload?.Invoke(count, weaponData.RELOAD_TIME);
    /// <summary>
    /// Do the reloading management
    /// </summary>
    private IEnumerator Reloading()
    {
        isReloading = true;
        float count = 0;

        while (!weaponData.RELOAD_TIME.TimerIn(ref count))
        {
            EmitReload(count);
            yield return new WaitForEndOfFrame();
        }
        EmitReload(count);

        currentAmmo = weaponData.AMMO;
        isReloading = false;
        EmitWeapon();
    }
    /// <summary>
    /// Check if the weapon can attack
    /// </summary>
    /// <returns></returns>
    protected bool CanAtack(){
        if (!flag_canAttack || currentAmmo.Equals(0) || isReloading) return false; // 🛡
        return true;
    }

    /// <summary>
    /// Do the attack if it have ammo
    /// </summary>
    public virtual void Attack() {
        flag_canAttack = false;
        currentAmmo--;
        EmitWeapon();
    }
    /// <summary>
    /// Do the aim
    /// </summary>
    public virtual void Aim() {

    }
    /// <summary>
    /// Starts to reload the weapon
    /// </summary>
    public virtual void Reload() {
        if (isReloading || currentAmmo.Equals(weaponData.AMMO)) return; // 🛡
        StartCoroutine(Reloading());
    }
    #endregion
}
