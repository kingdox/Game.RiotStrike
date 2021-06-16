#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Know;
using XavHelpTo.Look;

#endregion
/// <summary>
/// Follow the object and their childs to do the movement
/// </summary>
[CreateAssetMenu(menuName = "IA/Action/Patrol")]
public class PatrolStateAction : StateAction
{
    #region
    private int index = 0;
    private Transform[] childsPatrol;
    private bool firstTime=true;
    [Header("Patrol Action")]
    public bool isRandom = false;
    public float distanceWithTarget = 1f;
    #endregion
    #region Event
    #endregion
    /// <summary>
    /// Do the Patrol
    /// </summary>
    public override void Act(IABody ia) {
        if (firstTime) InitState(ia);
        Patrol(ia);
    }
    #region methods
    /// <summary>
    /// Initializes the status of the patrol
    /// </summary>
    private void InitState(IABody ia){
        firstTime = false;
        ia.patrol.Components(out childsPatrol);
        //"Init, go to base patrol".Print("Red");
        index = childsPatrol.Length.ZeroMax();
    }
    /// <summary>
    /// Patrol who where is he in the  state
    /// </summary>
    private void Patrol(IABody ia)
    {
        //Do the patrol
        ia.Move(childsPatrol[index].position);
        ia.Rotate(PatrolPosition);


        float distance = Vector3.Distance(
            ia.transform.position,
            PatrolPosition
        );

        //si esta dentro del rango
        if (distance < distanceWithTarget)
        {
            if (isRandom)
            {
                //Set a random order
                index = childsPatrol.Length.DifferentIndex(index);
            }
            else
            {
                index = Know.NextIndex(true, childsPatrol.Length, index);

            }
        }
    }


    /// <summary>
    /// get the position of the patrol
    /// </summary>
    private Vector3 PatrolPosition => childsPatrol[index].position;
    #endregion
}
