#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
#endregion
///<summary>
/// LostAction
///</summary>
[CreateAssetMenu(menuName = "IA/Action/Lost")]
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

        //Checkea si se encuentra perdido
        lostDuration.TimerFlag(ref ia.isLost, ref ia.lostTimeCount);

        ia.agent.isStopped = true;
        Vector3 toRotate = new Vector3(0f, turnSpeed * Time.deltaTime, 0f);
        ia.Rotate(toRotate);

    }
    #endregion
}
