#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// FetchAction class
///<summary>
[CreateAssetMenu (menuName ="IA/Action/Fetch")]
public class FetchAction : StateAction
{
    #region Variable
    public float stopDistance;
    #endregion
    #region Event
    /// <summary>
    /// Do the fetch
    /// </summary>
    public override void Act(IABody ia) {
        Fetch(ia);
    }
    #endregion
    #region Methods

    /// <summary>
    /// Fetch the target 
    /// </summary>
    private void Fetch(IABody ia) {
        ia.agent.stoppingDistance = stopDistance;

        if (!ia.target) return; // 🛡
        ia.Move(ia.target.position);
        ia.Rotate(ia.target.position);
    }
    #endregion
}
