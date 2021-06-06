#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dat = Environment.Data;
using XavHelpTo.Change;
using XavHelpTo;
using XavHelpTo.Get;
# endregion

/// <summary>
/// Base of every body base (ally or enemy) in game
/// dependency with <seealso cref="GravityController"/>, <seealso cref="CharacterController"/>, <seealso cref="MovementController"/>
/// </summary>
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(RotationController))]
[DisallowMultipleComponent]
public abstract class Body : MonoBehaviour
{
    #region Variables
    private const float BODY_MASS = 10f;
    //protected Body body;
    [Header("Body")]
    [SerializeField] protected int life;
    [SerializeField] protected bool isDead=false;
    [SerializeField] protected bool isInmune=false;
    public Transform tr_head;
    public Transform tr_body;
    public Transform tr_visualWeapon;
    public Transform tr_spells;
    [HideInInspector] public StatData stat;
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public MovementController movement;
    [HideInInspector] public RotationController rotation;
    [HideInInspector] public GravityController gravity;
    
    [Space]
    public Character character;
    public Action OnChangeLife;
    #endregion
    #region Event
    public virtual void Awake()
    {
        //body = this;
        stat = Dat.GetStatData(character.idStat).RealStats;
        life = stat.DEFENSE; //asign the max life
        this.Component(out gravity);
        this.Component(out controller);
        this.Component(out movement);
        this.Component(out rotation);
        character.Init(this);
    }
    public virtual void Start()
    {

    }
    public virtual void OnEnable()
    {
        gravity.OnImpact += FallImpact;
        character.Subscribes(this);
    }
    public virtual void OnDisable()
    {
        gravity.OnImpact -= FallImpact;
        character.UnSubscribes(this);
    }
    #endregion
    #region Method
    /// <summary>
    /// Makes the damage of a impact by a <seealso cref="GravityController"/>
    /// where <seealso cref="BODY_MASS"/> represent the mass
    /// </summary>
    private void FallImpact(float aceleration) => AddLife(BODY_MASS * aceleration);
    /// <summary>
    /// Set the adjustement to death state
    /// </summary>
    protected virtual void Death()
    {
        isDead = true;
        //TODO
    }
    /// <summary>
    /// Do the damage to the body
    /// </summary>
    public virtual void AddLife(int value){
        if (value < 0 && isInmune) return; // no damage, only health if is invincible
        life += value;
        life = life.Min(0).Max(stat.DEFENSE);
        OnChangeLife?.Invoke();
        if (life.Equals(0)) Death();
    }
    public virtual void AddLife(float value) => AddLife(value.ToInt());
    /// <summary>
    /// Shows the Qty of life
    /// </summary>
    public int Life => life;
    #endregion
}
