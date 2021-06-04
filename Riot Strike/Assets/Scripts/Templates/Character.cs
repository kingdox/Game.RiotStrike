#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Set;
# endregion

/// <summary>
/// Information of a character and their own elements
/// </summary>
[CreateAssetMenu(menuName = "Template/Character")]
[RequireComponent(typeof(BodyBase))]
public class Character : ScriptableObject
{
    #region Variable
    [HideInInspector] public Weapon weapon;
    [HideInInspector] public Spell spell;
    [Header("Character")]
    public string idStat;
    public GameObject pref_weapon;
    public GameObject pref_spell;

    public Action OnAttack;
    public Action OnAim;
    public Action OnReload;
    public Action OnCast;
    #endregion
    #region Method
    /// <summary>
    /// Do an initialization to get the components
    /// </summary>
    public void Init(BodyBase body)
    {
        Instantiate(pref_spell, body.tr_spells).transform
            .Component(out spell)
        ;
        Instantiate(pref_weapon, body.tr_visualWeapon).transform
            .Component(out weapon)
        ;
    }
    /// <summary>
    /// Do the subscriptions
    /// </summary>
    public void Subscribes(){
        OnAttack += weapon.Attack;
        OnAim += weapon.Aim;
        OnReload += weapon.Reload;
        OnCast += spell.Cast;

    }
    /// <summary>
    /// Do the unsubscriptions
    /// </summary>
    public void UnSubscribes()
    {
        OnAttack -= weapon.Attack;
        OnAim -= weapon.Aim;
        OnReload -= weapon.Reload;
        OnCast -= spell.Cast;
    }
    #endregion
}
