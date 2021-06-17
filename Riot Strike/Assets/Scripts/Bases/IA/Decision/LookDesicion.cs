#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
///<summary>
/// LoopDecision
///</summary>
[CreateAssetMenu(menuName ="IA/Decision/Look")]
public class LookDesicion : Decision
{
    #region Variables
    public string targetTag;
    public LayerMask layer;
    #endregion
    #region Events
    public override bool Decide(IABody ia) {
        return Look(ia);
    }
    #endregion
    #region Methods
    /// <summary>
    /// Evalua si hay un posible objetivo a la vista
    /// </summary>
    private bool Look(IABody ia) {
        bool condition = false;
        //Visualize the raycast
        Debug.DrawRay(
            ia.tr_eyes.position, 
            ia.tr_eyes.forward.normalized 
            * ia.iaStat.viewDepth,
            Color.red
        );

        RaycastHit hit;
        Ray ray = new Ray(ia.tr_eyes.position, ia.tr_eyes.forward);

        if (Physics.SphereCast(
            ray,
            ia.iaStat.viewWidth,
            out hit,
            ia.iaStat.viewDepth
            ) && hit.collider.CompareTag(targetTag)
        ) {
            ia.target = hit.transform;
            ia.lastSeenTargetLocation = hit.transform.position;
            condition = true;
        }


      /*  Debug.DrawRay(
            hit.point, 
            ia.tr_eyes.forward.normalized * 10,
            Color.magenta
        );*/


        return condition;

    }
    #endregion
}
