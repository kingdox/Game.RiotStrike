#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// NoTargetDesicion
///</summary>
[CreateAssetMenu(menuName ="IA/Desicions/NoTarget")]
public class NoTargetDesicion : Decision
{
    #region Variables
    #endregion
    #region Events
    public override bool Decide(IABody ia) {
        return CheckTarget(ia);
    }

    #endregion
    #region Methods
    private bool CheckTarget (IABody ia) {
        return (ia.target == null);
    }
    #endregion
}
