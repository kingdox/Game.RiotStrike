#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Requirement;
using XavHelpTo;
#endregion
/// <summary>
/// Requirement thath chekc if the element was destroyed
/// </summary>
public class DestroyedRequirement : Requirement.Requirement
{
    #region Variable
    #endregion
    #region Event
    private void OnDestroy()
    {
        "Hola".Print();
        if (!isComplete) return; // 🛡
        isComplete = true;
        Comprobation();
    }
    #endregion
    #region Methods
    public override void Comprobation()
    {
        $"COMPLETADO EN {name}".Print("blue");
        OnComplete?.Invoke();
    }
    #endregion
}
