#region Access
using System;
using System.Collections;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using XavHelpTo.Know;
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
    [Range(0.1f, 5)]
    public float duration = 1f;
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
    /// DO the dash without moving to any side
    /// </summary>
    private IEnumerator Jump(Body body)
    {
        //Move the body
        float count = 0;
        body.gravity.IgnoreFollowingImpact();
        body.gravity.enabled = false;
        while (!duration.TimerIn(ref count))
        {
            body.controller.Move(movement * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        body.gravity.enabled = true;
    }
    /// <summary>
    /// When the user reach the ground were he was jumped by the <seealso cref="Cast(Body)"/>
    /// <para> <paramref name="magnitude"/> Represents the qty of elements by the jump</para>
    /// </summary>
    private IEnumerator FallImpact(Body body)
    {
        waitFallImpact = true;
        body.gravity.IgnoreFollowingImpact();
        body.gravity.OnImpact += EndWaitImpact;
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
    /// Do the gravity cast, invert it
    /// </summary>
    public override void Cast(Body body){
        if (!CanCast()) return; // 🛡
        //Jump
        refresh.RefreshPlayParticle(Particle.JUMP);
        StartCoroutine(Jump(body));

        //Fall
        StartCoroutine(FallImpact(body));
    }
    #endregion
}
