#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using Dat = Environment.Data;
# endregion
/// <summary>
/// Information of the weapon
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    #region Variable
    private WeaponData weaponData;
    [HideInInspector] public int currentAmmo;
    [HideInInspector] public int maxAmmo;
    [HideInInspector] public float reloadTime;
    [HideInInspector] public float cadence;
    [Header("Weapon")]
    public string ID;
    #endregion
    #region Events
    private void Start()
    {
        weaponData = Dat.GetWeaponData(ID);
    }
    #endregion
    #region Method

    public virtual void Attack() { }
    public virtual void Aim() { }
    public virtual void Reload() { }

    #endregion
}
