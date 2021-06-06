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
    private bool isWaitForReset = false;
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
    /// DO the dash without moving to any side
    /// </summary>
    private IEnumerator Dashing(Body body)
    {
        //Move the body
        float count = 0;
        Vector3 forward = body.transform.TransformDirection(Vector3.forward);
        Vector3 plus = body.transform.TransformDirection(movement);
        body.movement.enabled = false;
        body.gravity.IgnoreFollowingImpact();
        while (!duration.TimerIn(ref count))
        {
            body.controller.Move( (forward+plus) *Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        body.movement.enabled = true;

    }
    /// <summary>
    /// Reset the effects of the body when it finishes t
    /// </summary>
    private void ResetBuffDamage(Body targetBody, int damage){

        targetBody.character.weapon.canUseWeapon =true ;
        isWaitForReset = false;
    }
    /// <summary>
    /// Do the dash cast
    /// </summary>
    public override void Cast(Body body)
    {
        if (!CanCast()) return; // 🛡

        //Moves
        //body.controller.SimpleMove(movement);
        refresh.RefreshPlayParticle(Particle.ELECTRO);

        //Enviar estadod e paralizis al arma
        //HAcer daño x2 Y quitarlo trás hacer el ataque

        //if is not used the spell and waits for the next it cant do that
        if (isWaitForReset)
        {
            body.character.weapon.canUseWeapon = false;

            isWaitForReset = true;
            StartCoroutine(WaitForSpellReset(body, body.stat.STRENGHT));
            body.stat.STRENGHT *= 2;
            body.character.weapon.OnTargetImpactWeapon += ResetBuffDamage;
        }


        //Start the dash
        StartCoroutine(Dashing(body));
    }

    /// <summary>
    /// Wait the end of the second part of the effect, stores the init Strenght
    /// </summary>
    IEnumerator WaitForSpellReset(Body body, int initStrenght)
    {

        //wait until the impact was done
        while (isWaitForReset) yield return new WaitForEndOfFrame();

        body.stat.STRENGHT -= initStrenght;

        body.character.weapon.OnTargetImpactWeapon -= ResetBuffDamage;
    }

    #endregion
}
