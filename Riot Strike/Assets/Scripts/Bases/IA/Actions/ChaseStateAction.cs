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
    /// Chase the last position saw
    /// </summary>
    private void Chase(IABody ia) {
        Fetch(ia, ia.lastSeenTargetLocation);
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
