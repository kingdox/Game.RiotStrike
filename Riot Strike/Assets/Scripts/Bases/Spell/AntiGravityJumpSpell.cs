#region Access
using System;
using System.Collections;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using SpellsRefresh.AntiGravityJumpSpell;
#endregion
/// <summary>
/// Spell to jump so high and evade fall damage
/// </summary>
public class AntiGravityJumpSpell : Spell
{
    #region Variable
    private RefreshController refresh;
    private bool waitFallImpact;
    private float lastMagnitude;
    [Header("Anti Gravity Jump Spell")]
    public Vector3 movement = new Vector3(0, 2, 0);
    public Action<Body> ac;
    #endregion
    #region Event
    protected override void Start()
    {
        base.Start();
        this.Component(out refresh);
    }
    #endregion
    #region Method

    /// <summary>
    /// When the user reach the ground were he was jumped by the <seealso cref="Cast(Body)"/>
    /// <para> <paramref name="magnitude"/> Represents the qty of elements by the jump</para>
    /// </summary>
    private IEnumerator FallImpact(Body body)
    {
        while (waitFallImpact) yield return new WaitForEndOfFrame();
        refresh.GetParticle(Particle.FALL).Emit(lastMagnitude.ToInt());
        refresh.RefreshPlayParticle(Particle.FALL);
        body.gravity.OnImpact -= EndWaitImpact;
    }
    /// <summary>
    /// Set the last aceleration and stop to wait to fall;
    /// </summary>
    private void EndWaitImpact(float magnitude) {
        lastMagnitude =magnitude;
        waitFallImpact = false;
    }
    /// <summary>
    /// Do the heal cast
    /// </summary>
    public override void Cast(Body body){
        if (!CanCast()) return; // 🛡
        //Moves
        //body.controller.SimpleMove(movement);
        body.controller.Move(movement);
        refresh.RefreshPlayParticle(Particle.JUMP);

        //Set fall protection
        body.gravity.IgnoreFollowingImpact();
        waitFallImpact = true;
        StartCoroutine(FallImpact(body));

        body.gravity.OnImpact += EndWaitImpact;
    }
    #endregion
}
