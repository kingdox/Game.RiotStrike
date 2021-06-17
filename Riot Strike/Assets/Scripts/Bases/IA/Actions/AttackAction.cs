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
    #region Methods
    /// <summary>
    /// Do the action attack
    /// rotates with the last spotted position and keeps ion the same position as he saw it
    /// </summary>
    public override void Act(IABody ia) {
        Attack(ia);
    }

    /// <summary>
    /// Check if is in the range to attack
    /// </summary>
    private void Attack(IABody ia) {
        ia.agent.updatePosition= false;
        ia.Rotate(ia.lastSeenTargetLocation);
        ia.Attack();
    }
    #endregion
}
