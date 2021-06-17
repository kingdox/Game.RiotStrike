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
    #region Variables

    #endregion
    #region Event
    public override void Act(IABody ia) {
        Fetch(ia);
    }
    #endregion
    #region Methods

    /// <summary>
    /// Fetch the target 
    /// </summary>
    private void Fetch(IABody ia) {
        if (!ia.target) return; // 🛡

        ia.Move(ia.target.position);
        ia.Rotate(ia.agent.nextPosition);
    }
    #endregion
}
