#region
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
# endregion
/// <summary> 
/// Check if the target is near as the last spotted position
/// </summary>
public class IsNearDesicion : Decision
{
    #region Variable
    public float distanceLimit = 5;
    #endregion
    #region Event
    public override bool Decide(IABody ia)
    {
        return IsTargetNear(ia);
    }
    #endregion
    #region Method
    /// <summary>
    /// Check if the player is near
    /// </summary>
    private bool IsTargetNear(IABody ia){
        if (!ia.target) return false;

        if (Vector3.Distance(ia.lastSeenTargetLocation,ia.target.position)
            < distanceLimit
        ){
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
