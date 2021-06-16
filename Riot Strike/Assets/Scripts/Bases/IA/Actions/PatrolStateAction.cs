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
    [Header("Patrol Action")]
    public bool isRandom = false;
    public float distanceWithTarget = 1f;

    public bool firstTime = true;
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
        index = ia.patrol.childCount.ZeroMax();
    }
    /// <summary>
    /// Patrol who where is he in the  state
    /// </summary>
    private void Patrol(IABody ia)
    {
        //Do the patrol
        try
        {
        ia.Move(PatrolPosition(ia));
        ia.Rotate(PatrolPosition(ia));
        }
        catch (System.Exception ex)
        {

        }
        CheckForNextPosition(ia);
    }
    /// <summary>
    /// Check if is at the end of the current position and choose the next
    /// it can be random or not
    /// </summary>
    private void CheckForNextPosition(IABody ia) {
        float distance = Vector3.Distance(
            ia.transform.position,
            PatrolPosition(ia)
        );
        if (distance < distanceWithTarget)
        {
            index = isRandom 
                ? PatrolQty(ia).DifferentIndex(index)
                : Know.NextIndex(true, PatrolQty(ia)-1, index)
            ;
        }
    }
    /// <summary>
    /// get the position of the patrol
    /// </summary>
    private Vector3 PatrolPosition(IABody ia) => ia.patrol.GetChild(index).position;
    /// <summary>
    /// Qty of childs
    /// </summary>
    private int PatrolQty(IABody ia) => ia.patrol.childCount;
    #endregion
}
