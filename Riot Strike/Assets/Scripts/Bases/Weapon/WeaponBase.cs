#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Change;
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
    private bool isReloading = false;
    private bool isAiming = false;
    private float cadenceCount = 0;
    private bool flag_canAttack = false;
    [Header("Weapon")]
    protected WeaponData weaponData;
    protected int currentAmmo;
    [HideInInspector] public bool canUseWeapon=true;
    public string ID = "0";
    [Range(0, 1)] public float aimZoomPercent = 1;

    public AudioClip clip_attack;
    public AudioClip clip_impact;

    public Action<int,int>      OnFireAttack;
    public Action<float, float> OnReload;
    public Action<Body, int>    OnTargetImpactWeapon; 
    public Action<float>        OnZoom;
    #endregion
    #region Events
    private void Start()
    {
        canUseWeapon = true;
        weaponData = Dat.GetWeaponData(ID);
        currentAmmo = weaponData.AMMO;
        EmitWeapon();
        EmitReload(0);
    }
    protected virtual void Update()
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
    /// returns values of the target and then shows the qty of life and the body of the target
    /// </summary>
    protected void EmitTargetImpactWeapon(Body target, int damage)
    {
        if (clip_impact) AudioSystem.PlaySound(clip_impact);

        OnTargetImpactWeapon?.Invoke(target, damage);
    }
    /// <summary>
    /// Check if the weapon can attack
    /// </summary>
    /// <returns></returns>
    public bool CanAtack(){
        if (!canUseWeapon
            || !flag_canAttack
            || currentAmmo.Equals(0)
            || isReloading)
            return false; // 🛡
        return true;
    }
    
    /// <summary>
    /// Do the attack if it have ammo
    /// </summary>a
    public virtual void Attack(Body body) {
        flag_canAttack = false;
        if (clip_attack) AudioSystem.PlaySound(clip_attack);

        currentAmmo--;
        EmitWeapon();
    }
    /// <summary>
    /// Do the aim
    /// </summary>
    public virtual void Aim(Body body) {
        if (isReloading || isAiming) return; // 🛡
        //"aim".Print("yellow");
        OnZoom.Invoke(-aimZoomPercent);
        isAiming = true;
    }
    /// <summary>
    /// Do the disAim
    /// </summary>
    public virtual void DisAim(Body body) {
        if (!isAiming) return; // 🛡
        //"disaim".Print("yellow");
        OnZoom.Invoke(aimZoomPercent);
        isAiming = false;
    }
    /// <summary>
    /// Starts to reload the weapon
    /// </summary>
    public virtual void Reload() {
        if (isReloading || isAiming || currentAmmo.Equals(weaponData.AMMO)) return; // 🛡
        StartCoroutine(Reloading());
    }
    #endregion
}
