#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
///<summary>
/// ChaseStateAction class
/// Follow the target wheter if is near
///<summary>
[CreateAssetMenu(menuName = "IA/Action/Chase")]
public class ChaseStateAction : StateAction
{
    #region Variables
    public float stopDistance = 3;
    //ChaseOnlyInCamera
    #endregion
    #region Methods
    /// <summary>
    /// Do the Chase
    /// </summary>
    public override void Act(IABody ia) {
        Chase(ia);
    }
    /// <summary>
    /// Fetch the last position saw
    /// </summary>
    private void Chase(IABody ia) {

        // Si tiene target
        if (ia.target) {
            Fetch(ia, ia.iaStat.lastSeenTargetLocation);
        }


        // si la distancia para llegar es menor que la cual deberá detenerse
        // se quita la referencia del objetivo
        if (ia.agent.remainingDistance <= stopDistance) {
            ia.target = null;
        }
    }

    /// <summary>
    /// Do the movement and rotation
    /// </summary>
    private void Fetch(IABody ia, Vector3 pos) {
        ia.Move(pos);
        ia.Rotate(pos);
    }
    #endregion
}
