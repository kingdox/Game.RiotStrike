#region Access
using System;
using UnityEngine;
using XavHelpTo;
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
        // SPELL
        SetCharacterPart(body.tr_spells, pref_spell, out spell);
        // WEAPON
        SetCharacterPart(body.tr_visualWeapon, pref_weapon, out weapon);
        // BODY
        SetCharacterPart(body.tr_body, pref_body, out Transform model);
    }

    /// <summary>
    /// Clear and set the part with their character values like tag and layer
    /// </summary>
    private void SetCharacterPart<T>(Transform tr_parent, GameObject pref, out T part)
        where T: Component
    {
        tr_parent.ClearChilds();
        Instantiate(pref, tr_parent).transform
            .Component(out part)
        ;
        part.tag = tag;
        part.gameObject.layer = layer;
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
