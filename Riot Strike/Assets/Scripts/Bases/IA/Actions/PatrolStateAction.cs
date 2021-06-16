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
    #region Variables
    private int index = 0;
    private Transform[] childsPatrol;
    private bool firstTime=true;
    [Header("Patrol Action")]
    public bool isRandom = false;
    public float distanceWithTarget = 1f;
    #endregion
    #region Methods
    /// <summary>
    /// Do the Patrol
    /// </summary>
    public override void Act(IABody ia) {
        if (firstTime) InitState(ia);
        Patrol(ia);
    }
    /// Initializes the status of the patrol
    /// </summary>
    private void InitState(IABody ia){
        firstTime = false;
        ia.patrol.Components(out childsPatrol);
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
        CheckForNextPosition(ia);
    }
    /// <summary>
    /// Check if is at the end of the current position and choose the next
    /// it can be random or not
    /// </summary>
    private void CheckForNextPosition(IABody ia) {
           float distance = Vector3.Distance(
            ia.transform.position,
            PatrolPosition
        );
        if (distance < distanceWithTarget)
        {
            index = isRandom 
                ? childsPatrol.Length.DifferentIndex(index)
                : Know.NextIndex(true, childsPatrol.Length, index)
            ;
        }
    }
    /// <summary>
    /// get the position of the patrol
    /// </summary>
    private Vector3 PatrolPosition => childsPatrol[index].position;
    #endregion
}
