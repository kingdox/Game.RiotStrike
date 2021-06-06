#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
using SpellsRefresh.ElectroSpell;
#endregion
/// <summary>
/// Spell who doubles the damage (without the dmg base) for a short time
/// </summary>
public class ElectroSpell : Spell
{
    #region Variable
    private RefreshController refresh;
    [Header("Electro Spell")]
    public int buffMultiplier=2;
    public float duration = 2f;
    #endregion
    #region Event
    protected override void Start(){
        base.Start();
        this.Component(out refresh);
    }
    #endregion
    #region Method

    /// <summary>
    /// Do the eltro spell cast, doubles in x2
    /// </summary>
    public override void Cast(Body body)
    {
        if (!CanCast()) return; // 🛡

        //Set the buff and manages itself
        StartCoroutine(SpellStrenght(body));
    }

    /// <summary>
    /// 
    /// </summary>
    IEnumerator SpellStrenght(Body body)
    {
        refresh.RefreshPlayParticle(Particle.ELECTRO);
        //set the augments
        int amt = body.stat.STRENGHT * buffMultiplier;
        body.stat.STRENGHT += amt;
        //wait the duration
        float count = 0;
        while (!duration.TimerIn(ref count)) yield return new WaitForEndOfFrame();
        //reduce the buff
        body.stat.STRENGHT -= amt;

    }
    #endregion
}
