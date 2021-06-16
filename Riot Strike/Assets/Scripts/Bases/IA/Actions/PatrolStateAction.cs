#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
using XavHelpTo.Look;

#endregion
/// <summary>
/// Follow the object and their childs to do the movement
/// </summary>
[CreateAssetMenu(menuName = "AISystem/Actions/Patrol")]
public class PatrolStateAction : StateAction
{
    #region
    private int index = 0;
    private Transform[] childsPatrol;
    private Transform toGo;

    public float distanceWithTarget = 1f;
    #endregion
    #region Event
    #endregion
    /// <summary>
    /// Do the Patrol
    /// </summary>
    public override void Act(IABody ia) {

        if (!ia.isStateActionInited) InitState(ia);
        

        Patrol(ia);
    }
    #region methods
    private void InitState(IABody ia){
        ia.isStateActionInited = true;
        ia.patrol.Components(out childsPatrol);
        //"Init, go to base patrol".Print("Red");
        toGo = ia.patrol;
    }
    /// <summary>
    /// Patrol who where is he in the  state
    /// </summary>
    private void Patrol(IABody ia)
    {
        //Do the patrol
        ia.Move(toGo.position);
        ia.Rotate(toGo.position);


        float distance = Vector3.Distance(
            ia.transform.position,
            toGo.position
        );

        //si esta dentro del rango
        if (distance < distanceWithTarget)
        {
            index = Know.NextIndex(true, childsPatrol.Length, index);
            toGo = childsPatrol[index];
        }


    }
    #endregion
}
