#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// StateAction
///</summary>
public abstract class StateAction : ScriptableObject
{
    #region Variables
    public abstract void Act(IABody ia);
#endregion
}
