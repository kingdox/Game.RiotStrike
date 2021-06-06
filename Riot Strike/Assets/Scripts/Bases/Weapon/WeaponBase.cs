#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Know;
using XavHelpTo.Set;
using Dat = Environment.Data;
# endregion
/// <summary>
/// Information of the weapon
/// </summary>
[DisallowMultipleComponent]
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
    public Action<int, int> OnTargetImpactWeapon; // target life and max target life

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
    /// returns values of the target and then shows the qty of life and max life
    /// </summary>
    protected void EmitTargetImpactWeapon(int life, int max) => OnTargetImpactWeapon?.Invoke(life, max);//TODO
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
    /// </summary>a
    public virtual void Attack(Body body) {
        flag_canAttack = false;
        currentAmmo--;
        EmitWeapon();
    }
    /// <summary>
    /// Do the aim
    /// </summary>
    public virtual void Aim(Body body) {
        "aim".Print("yellow");
        //TODO
        // enfocar con el FOV una cantidad y quitarla cuando ya no se encuentra
    }
    /// <summary>
    /// Do the disAim
    /// </summary>
    public virtual void DisAim(Body body) {
        "disaim".Print("yellow");
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
