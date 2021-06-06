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
[RequireComponent(typeof(Body))]
[DisallowMultipleComponent]
public class Character : ScriptableObject
{
    #region Variable
    [HideInInspector] public Weapon weapon;
    [HideInInspector] public Spell spell;
    [HideInInspector] public string tag;
    [HideInInspector] public int layer;
    [Header("Character")]
    public string idStat;
    public GameObject pref_body;
    public GameObject pref_weapon;
    public GameObject pref_spell;
    public Action<Body> OnAttack;
    public Action<Body> OnAim;
    public Action<Body> OnDisAim;
    public Action OnReload;
    public Action<Body> OnCast;
    #endregion
    #region Method
    /// <summary>
    /// Do an initialization to get the components
    /// </summary>
    public void Init(Body body){
        tag = body.tag;
        layer = body.gameObject.layer;
        //SPELL
        Instantiate(pref_spell, body.tr_spells).transform
            .Component(out spell)
        ;
        spell.tag = tag;
        spell.gameObject.layer = layer;

        //WEAPON 
        Instantiate(pref_weapon, body.tr_visualWeapon).transform
            .Component(out weapon)
        ;
        weapon.tag = tag;
        weapon.gameObject.layer = layer;

        //VISUAL MODEL
        GameObject model = Instantiate(pref_body, body.tr_body);
        model.tag = tag;
        model.layer = layer;
    }
    /// <summary>
    /// Do the subscriptions
    /// </summary>
    public void Subscribes(Body body){
        OnAttack += weapon.Attack;
        OnAim += weapon.Aim;
        OnDisAim += weapon.DisAim;
        OnReload += weapon.Reload;
        OnCast += spell.Cast;

    }
    /// <summary>
    /// Do the unsubscriptions
    /// </summary>
    public void UnSubscribes(Body body)
    {
        OnAttack -= weapon.Attack;
        OnAim -= weapon.Aim;
        OnDisAim += weapon.DisAim;
        OnReload -= weapon.Reload;
        OnCast -= spell.Cast;
    }
    #endregion
}
