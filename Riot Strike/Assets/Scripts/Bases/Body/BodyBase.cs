﻿#region Access
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
public abstract class Body : MonoBehaviour
{
    #region Variables

    private GravityController gravity;
    private const float BODY_MASS = 10f;
    [Header("Body")]
    public int life;
    public bool isDead=false;
    public bool isInmune=false;
    public Transform tr_body;
    public Transform tr_visualWeapon;
    public Transform tr_spells;
    [HideInInspector] public StatData stat;
    [Space]
    public Character character;

    
    
    #endregion
    #region Event
    public virtual void Awake()
    {

        stat = Dat.GetStatData(character.idStat).RealStats;
        life = stat.DEFENSE; //asign the max life
        this.Component(out gravity);

        character.Init(this);
    }
    public virtual void OnEnable()
    {
        gravity.OnImpact += FallImpact;
        character.Subscribes();
    }
    public virtual void OnDisable()
    {
        gravity.OnImpact -= FallImpact;
        character.UnSubscribes();
    }
    #endregion
    #region Method

    /// <summary>
    /// Do the damage to the body
    /// </summary>
    public virtual void TakeDamage(int damage){
        if (isInmune) return; 
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
