#region
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
# endregion
/// <summary>
/// Buff base to do the use of variations,
/// The buff structure is:
///  - do collision
///  - do effect
///  - Destroy itself
/// </summary>
public abstract class Buff : MonoBehaviour
{
    #region Variables
    private Collider col;
    private bool isBuffTaked = false;
    //public Action OnBuff;
    #endregion
    #region Events
    private void Awake()
    {
        this.Component(out col,false);
        if (!col) $"Fallo de toma de colisión en {name}".Print("red");
    }
    private void OnTriggerEnter(Collider other) {
        if (isBuffTaked || !other.CompareTag(tag)) return; // 🛡, only can get the same tag of the buff
        isBuffTaked = true;
        BuffTaking(other.transform);
    }
    #endregion
    #region Method
    /// <summary>
    /// manages the actions to do the effect and resolve the base of the buffs
    /// </summary>
    private void BuffTaking(in Transform target)
    {
        //OnBuff?.Invoke();
        DoEffectBuff(in target);
        DestroyBuff(in target);
    }
    /// <summary>
    /// Do the effect of the buff
    /// </summary>
    public abstract void DoEffectBuff(in Transform target);
    /// <summary>
    /// Destroys the buff
    /// </summary>
    public virtual void DestroyBuff(in Transform target)
    {
        Destroy(this.gameObject);
    }
    #endregion
}
