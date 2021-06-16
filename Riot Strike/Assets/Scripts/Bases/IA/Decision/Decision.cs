#region Imports
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
    public abstract bool Decide(IABody ia);
#endregion
}
