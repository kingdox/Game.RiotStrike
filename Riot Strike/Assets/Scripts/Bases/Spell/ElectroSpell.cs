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
    IEnumerator BuffDuration(Body body)
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
    /// <summary>
    /// Do the heal cast
    /// </summary>
    public override void Cast(Body body)
    {
        if (!CanCast()) return; // 🛡

        //TODO Hacer un Action en Weapon de cuando Hace contacto con un objetivo
        // nos suscribimos aquí y podremos pintar por separado las lineas
        //body.character.weapon.OnShooted += DrawLinerr(Vector3)

        /**
         * 1. colocar en el weapon /o seguir las balas que hagan un LineTrail para mostrar un efecto de bala de rayo o zap
         * 2. Aumentar la fuerza del usuario por 2 segundos
         */

        //Set the buff and manages itself
        StartCoroutine(BuffDuration(body));
    }


    

    #endregion
}
