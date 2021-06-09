#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
namespace Requirement
{
    /// <summary>
    /// Base class to creation of types of Requirements
    /// </summary>
    public abstract class Requirement : MonoBehaviour
    {
        #region Variables
        [Header("Requirement Base")]
        public bool isComplete = false;
        public Action OnComplete;
        #endregion
        #region Events

        #endregion
        #region Methods
        /// <summary>
        /// Check if the <see cref="isComplete"/> was successful
        /// </summary>
        public abstract void Comprobation();
        #endregion
    }
}
