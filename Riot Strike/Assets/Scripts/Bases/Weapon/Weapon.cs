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

    [HideInInspector] public int currentAmmo;

    [Header("Weapon")]
    public string ID;


    public Action<int,int> OnFireAttack; //TODO
    #endregion
    #region Events
    private void Start()
    {
        weaponData = Dat.GetWeaponData(ID);

    }
    private void Update()
    {
        weaponData.CADENCE.TimerFlag(ref flag_canAttack, ref cadenceCount);

    }
    #endregion
    #region Method

    /// <summary>
    /// Check if the weapon can attack
    /// </summary>
    /// <returns></returns>
    protected bool CanAtack(){
        if (!flag_canAttack || currentAmmo.Equals(0) || isReloading) return false; // 🛡
        return false;
    }

    /// <summary>
    /// Do the attack if it have ammo
    /// </summary>
    public virtual void Attack() {
        currentAmmo--;
        OnFireAttack?.Invoke(currentAmmo, weaponData.AMMO);
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
        if (isReloading && currentAmmo.Equals(weaponData.AMMO)) return; // 🛡
        StartCoroutine(Reloading());
    }

    /// <summary>
    /// Do the reloading management
    /// </summary>
    private IEnumerator Reloading()
    {
        $"Start reload: {weaponData.RELOAD_TIME}".Print();
        isReloading = true;
        yield return new WaitForSeconds(weaponData.RELOAD_TIME);
        currentAmmo = weaponData.AMMO;
        isReloading = false;
        $"END reload".Print();

    }
    #endregion
}
