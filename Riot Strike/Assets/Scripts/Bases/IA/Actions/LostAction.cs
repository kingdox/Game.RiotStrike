#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// LostAction
///</summary>
[CreateAssetMenu(menuName = "IA/Actions/Lost")]
public class LostAction : StateAction
{
    #region Variables   
    public float turnSpeed = 3f;
    public float lostDuration = 2f;
    #endregion
    #region Methods
    public override void Act(IABody ia) {
        Lost(ia);
    }

    /// <summary>
    /// Rotates by itself looking for the target 
    /// </summary>
    private void Lost(IABody ia) {
     /**   if (!ia.timerCounting) {
            ia.stateTimer = lostDuration;
            ia.timerCounting = true;


        }


        ia.agent.isStopped = true;
        ia.transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
        ia.stateTimer -= Time.deltaTime;
     **/

    }
    #endregion
}
