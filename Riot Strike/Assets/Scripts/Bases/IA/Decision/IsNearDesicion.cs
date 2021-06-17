#region
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
# endregion
/// <summary> 
/// Check if the target is near as the last spotted position
/// </summary>
[CreateAssetMenu(menuName ="IA/Decision/IsNear")]
public class IsNearDesicion : Decision
{
    #region Variable
    public float distanceWithTarget = 10f;
    public float distanceBetweenTargetAndLastSpot = 5f;
    #endregion
    #region Event
    /// <summary>
    /// Do the management to know wether is the target still near
    /// </summary>
    public override bool Decide(IABody ia){
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
            < ia.iaStat.viewDepth
        ) return true;


        //si el target esta lo suficientemente cerca
        if (Vector3.Distance(ia.target.position, ia.transform.position) < distanceWithTarget) {
            return true;
        }



        return false;
    }
    #endregion
}
