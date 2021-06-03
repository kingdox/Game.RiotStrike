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
/// </summary>
public abstract class BodyBase : MonoBehaviour
{
    #region Variables

    private GravityController gravity;
    private const float BODY_MASS = 10f;
    [Header("Body")]
    public int life;
    public bool isDead;
    [HideInInspector] public StatData stat;
    [Space]
    public Character character;

    protected Action OnAttack;
    protected Action OnFocus;
    protected Action OnReload;
    protected Action OnSpell;
    #endregion
    #region Event
    public virtual void Awake()
    {

        stat = Dat.GetStatData(character.idStat).RealStats;
        life = stat.DEFENSE; //asign the max life
        this.Component(out gravity);
    }
    public virtual void OnEnable()
    {
        gravity.OnImpact += FallImpact;
    }
    public virtual void OnDisable()
    {
        gravity.OnImpact -= FallImpact;
    }
    #endregion
    #region Method

    /// <summary>
    /// Do the damage to the body
    /// </summary>
    public virtual void TakeDamage(int damage)
    {
        life = (life - damage).Min(0);
        if (life.Equals(0)) Death();
    }
    public virtual void TakeDamage(float damage) => TakeDamage(damage.ToInt());

    /// <summary>
    /// Set the adjustement to death state
    /// </summary>
    protected virtual void Death() {
        isDead = true;
    }


    /// <summary>
    /// Makes the damage of a impact by a <seealso cref="GravityController"/>
    /// where <seealso cref="BODY_MASS"/> represent the mass
    /// </summary>
    private void FallImpact(float aceleration) => TakeDamage(BODY_MASS * aceleration);
    #endregion
}
