#region Access
using System;
using UnityEngine;
#endregion
#region Interfaces


/// <summary>
/// contract where you check the target by an impact
/// </summary>
public interface ITargetImpact{
    /// <summary>
    /// Combrobates if the CheckTarget is with a important element
    /// </summary>
    void CheckTarget(Transform s);
    //Action<Body, int> OnImpact { set; }
}

#endregion