#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
using XavHelpTo.Change;
using SpellsRefresh.ElectroSpell;
#endregion
/// <summary>
/// Do a dash jump forward
/// </summary>
public class SuperDashSpell : Spell
{
    #region Variable
    private RefreshController refresh;
    private bool isWaitImpact = false;
    [Header("Super Dash Spell")]
    public Vector3 movement = new Vector3(0, 10, 5);
    [Range(0.1f,5)]
    public float duration = 1f;
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
    /// Do the dash cast
    /// When is Dashing disables rotation and movement
    /// </summary>
    public override void Cast(Body body)
    {
        if (!CanCast()) return; // 🛡

        //EFFECT
        refresh.RefreshPlayParticle(Particle.ELECTRO);

        //START BUFF AND SPELLS EFFECTS
        StartCoroutine(SetSpells(body));
    }

    /// <summary>
    /// Manages the buffs
    ///  - Adds the x2 strenght buff
    ///  - Start the dash effect
    /// </summary>
    private IEnumerator SetSpells(Body body)
    {
        // START DASH
        StartCoroutine(SpellDash(body));

        // If is waiting to impact, stays
        if (!isWaitImpact){
            isWaitImpact = true;

            // START STRENGHT
            StartCoroutine(SpellStrenght(body));

            // SUSCRIBE IMPACT 
            //"SUBSCRIBE".Print();
            body.character.weapon.OnTargetImpactWeapon += ResolveImpactEffects;
            yield return new WaitUntil(IsImpact);
            //"UNSUBSCRIBE".Print();
            body.character.weapon.OnTargetImpactWeapon -= ResolveImpactEffects;
            isWaitImpact = false;
        }
    }
    /// <summary>
    /// DO the dash without moving to any side
    /// </summary>
    private IEnumerator SpellDash(Body body)
    {
        //DISABLE MOVEMENT, IGNORE NEXT GRAVITY IMPACT
        Vector3 forward = body.transform.TransformDirection(Vector3.forward);
        Vector3 aditionalMotion = body.transform.TransformDirection(movement);
        body.movement.enabled = false;
        body.gravity.IgnoreFollowingImpact();

        // MOVE
        float count = 0;
        while (!duration.TimerIn(ref count))
        {
            body.controller.Move((forward + aditionalMotion) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        // ENABLE MOVEMENT
        body.movement.enabled = true;
    }
    /// <summary>
    /// Do the manages of the Strenght and returns to normality when is usedAttack 
    /// </summary>
    private IEnumerator SpellStrenght(Body body)
    {
        //  ADD STRENGHT
        int savedStrenght = body.stat.STRENGHT; // Stores the last strenght
        body.stat.STRENGHT += savedStrenght; // set the x2 damage of the buff

        // SUSCRIBE TO Remove Buff when Impact
        yield return new WaitUntil(IsImpact);

        body.stat.STRENGHT -= savedStrenght;
    }
    /// <summary>
    /// Resolve the extra behaviours of the spell
    /// - Stun target for 3 seconds
    /// </summary>
    private void ResolveImpactEffects(Body targetBody, int damage)
    {
        //"RESOLVE".Print("green");

        // START STUN
        StartCoroutine(SpellStun(targetBody));

        //end wait impact
        isWaitImpact = false;
    }
    /// <summary>
    /// Do the management of the target stun
    /// </summary>
    private IEnumerator SpellStun(Body targetBody){
        void TargetStop(bool condition){
            targetBody.movement.enabled = condition;
            targetBody.rotation.enabled = condition;
        }
        TargetStop(false);
        yield return new WaitForSeconds(3);
        TargetStop(true);
    }
    /// <summary>
    /// Returns if is waiting for the impact
    /// </summary>
    private bool IsImpact() => !isWaitImpact;
    #endregion
}
