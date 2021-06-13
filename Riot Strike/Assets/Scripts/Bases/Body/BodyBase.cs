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
    private const float BODY_MASS = 1f;//prev 10
    //protected Body body;
    [Header("Body")]
    public Color _debugColorIdentifier;
    public Vector3 debug_size;
    public Vector3 debug_center;
    [SerializeField] protected int life;
    [SerializeField] protected bool isDead=false;
    [SerializeField] protected bool isInmune=false;
    public Transform tr_head;
    public Transform tr_eyes;
    public Transform tr_body;
    public Transform tr_visualWeapon;
    public Transform tr_spells;
    [HideInInspector] public StatData stat;
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public MovementController movement;
    [HideInInspector] public RotationController rotation;
    [HideInInspector] public GravityController gravity;
    public Character character;
    [Space]
    public Action OnChangeLife;
    public Action OnDeath;
    #endregion
    #region Event
    public virtual void Awake()
    {
        //body = this;
        SetStat();
        this.Component(out gravity);
        this.Component(out controller);
        this.Component(out movement);
        this.Component(out rotation);
        character.Init(this);
    }
    public virtual void Start(){} // a proposito
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
    private void OnDrawGizmos() {
        Gizmos.color = _debugColorIdentifier;
        Gizmos.DrawCube(transform.position + debug_center, transform.localScale + (debug_size));
    }
    #endregion
    #region Method
    /// <summary>
    /// Set the stat information to the body, using the character id information
    /// </summary>
    public void SetStat()
    {
        stat = Dat.GetStatData(character.idStat);
        life = stat.RealHealth;
    }
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
        OnDeath?.Invoke();
    }
    /// <summary>
    /// Do the damage to the body
    /// </summary>
    public virtual void AddLife(int value){
        if (value < 0 && isInmune) return; // no damage, only health if is invincible
        life += value;
        life = life.Min(0).Max(stat.RealHealth);
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
