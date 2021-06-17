#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
///<summary>
/// AttackAction class
///<summary>
[CreateAssetMenu (menuName ="IA/Action/Attack")]
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

        //Si esta en el rango de visión
        if (!!ia.target && ia.agent.remainingDistance <= ia.iaStat.viewDepth) {

            ia.target.Component(out Body targetBody, false);

            Quaternion rot =Quaternion.LookRotation(targetBody.tr_head.position - ia.tr_head.position);

            ia.tr_head.rotation = rot;
            //ia.tr_head.rotation = Quaternion.LookRotation(targetBody.tr_head.position - ia.tr_head.position);

            //Mover los ojos para apuntar

            ia.Attack();
        }
    }
    #endregion
}
