#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using SpellsRefresh.ElectroSpell;
#endregion
/// <summary>
/// Do a dash jump forward
/// </summary>
public class SuperDashSpell : Spell
{
    #region Variable
    private RefreshController refresh;
    [Header("Super Dash Spell")]
    public Vector3 movement = new Vector3(0,10,5);
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
    /// 
    /// </summary>
    private IEnumerator Dashing(Body body)
    {
        //Move the body

        //while (true)
        //{
        yield return new WaitForEndOfFrame();
        //}



    }
    /// <summary>
    /// Do the heal cast
    /// </summary>
    public override void Cast(Body body)
    {
        if (!CanCast()) return; // 🛡

        //Moves
        body.controller.Move(movement);
        refresh.RefreshPlayParticle(Particle.ELECTRO);


        //TODO Enviar estadod e paralizis al arma
        //TODO HAcer daño x2 Y quitarlo trás hacer el ataque


        //Start the dash
        StartCoroutine(Dashing(body));
    }
    #endregion
}
