#region Access
using UnityEngine;
using XavHelpTo;
using SpellsRefresh.HealSpell;
# endregion
/// <summary>
/// Heal the allies and yourself
/// </summary>
public class HealSpell : Spell
{
    #region Variable
    private RefreshController refresh;
    [Header("Heal Spell")]
    public float healQty;
    #endregion
    #region Event
    protected override void Start()
    {
        base.Start();
        this.Component(out refresh);
    }
    #endregion
    #region Method
    /// <summary>
    /// Do the heal cast
    /// </summary>
    public override void Cast(Body body){
        if (!CanCast()) return; // 🛡
        body.AddLife(healQty);
        refresh.RefreshPlayParticle(Particle.HEAL);
        refresh.RefreshPlayParticle(Particle.HEAL_AREA);
    }
    #endregion
}
