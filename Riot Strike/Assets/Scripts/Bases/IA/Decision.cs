#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// Decision
///</summary>
public abstract class Decision : ScriptableObject
{
    #region Variables

    /// <summary>
    /// Toma la desición basandose en una logica dada
    /// </summary>
    public abstract bool Decide(StateController controller);
    
#endregion
#region Events
#endregion
#region Methods
#endregion
}
