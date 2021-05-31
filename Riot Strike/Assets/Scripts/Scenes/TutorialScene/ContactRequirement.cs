#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Requirement;
using Dat = Environment.Data;

#endregion
namespace Requirement
{
    /// <summary>
    /// Class used to Know if the 
    /// </summary>
    public class ContactRequirement : RequirementBase
    {
        #region Variable
        [Tooltip("Tag usado para comprobar si ha sido checkeado")]
        public string tagToCheck = Dat.TAG_PLAYER;
        //public Transform target
        //    p

        #endregion
        #region Event
        public void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag(tagToCheck)) return;//🛡
            isComplete = true;
        }
        #endregion
        #region Method

        /// <summary>
        /// Check if the contact was successfull
        /// </summary>
        public override void Comprobation()
        {
            //throw new System.NotImplementedException();

        }
        #endregion
    }
}
