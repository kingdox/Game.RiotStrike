#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// AttackAction class
///<summary>
[CreateAssetMenu (menuName ="IA/Actions/Attack")]
public class AttackAction : StateAction
{
    #region Variables
    #endregion
    #region Methods
    public override void Act(IABody ia) {
        Attack(ia);
    }

    /// <summary>
    /// Check if is in the range to attack
    /// </summary>
    private void Attack(IABody ia) {

        //TODO, saber si tiene balas

        //Si esta en el rango de visión
        if (ia.agent.remainingDistance <= ia.iaStat.viewDepth) {
            ia.Attack();
        }
    }
    #endregion
}
