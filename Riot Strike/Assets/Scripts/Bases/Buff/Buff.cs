#region
using System;
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
    [Header("Buff")]
    public string tagToDetect;
    public Color color;
    public string message;
    public AudioClip clip;
    public Action OnDestroyed;
    #endregion
    #region Events
    private void Awake()
    {
        this.Component(out col,false);
        if (!col) $"Fallo de toma de colisión en {name}".Print("red");
    }
    private void OnTriggerEnter(Collider other) {
        other.transform.Component(out Body body, false);
        if (
            isBuffTaked
            || !other.CompareTag(tagToDetect)
            || !body // si no posee body
            ) return; // 🛡, only can get the same tag of the buff
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
        if (clip) AudioSystem.PlaySound(clip);

        CheckMessage(target);

        DoEffectBuff(in target);
        DestroyBuff(in target);
    }

    /// <summary>
    /// Do the management of the ianformation to show in HUD wether is a player the target
    /// </summary>
    protected virtual void CheckMessage(Transform target) {

        target.Component(out PlayerBody player, false);
        if (player)  player.EmitBuff(message, color); 
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
        OnDestroyed?.Invoke();
        Destroy(this.gameObject);
    }
    #endregion
}
