#region Accesss
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using BuffRefresh.StatBuff;
using Dat = Environment.Data;
# endregion
/// <summary>
/// Buff that add 1 on strenght stat
/// </summary>
public class StatBuff : Buff
{
    #region Variables
    private RefreshController refresh;
    [Header("Stat Buff")]
    public EStat eStat;
    public int qty;
    public float destroyDuration;
    public MeshRenderer render;
    #endregion
    #region Events
    private void Start(){
        this.Component(out refresh);
    }
    #endregion
    #region Methods
    /// <summary>
    /// Adds the buff
    /// </summary>
    public override void DoEffectBuff(in Transform target){

        target.Component(out Body body);
        
        if (body)
        {
            switch (eStat)
            {
                case EStat.ATTACK:
                    body.stat.STRENGHT += qty;
                    break;
                case EStat.DEFENSE:
                    body.stat.DEFENSE += qty;
                    break;
                case EStat.SPEED:
                    body.stat.SPEED += qty;
                    break;
                case EStat.HEALTH:
                    body.AddLife(qty);
                        break;
                default:
                    "Nuevo Stat?, asignarlo aquí".Print("yellow");
                    break;
            }
        }
        else
        {
            $"Error al buscar body en {target.name}".Print("red");
        }
    }

    protected override void CheckMessage(Transform target) {
       // base.CheckMessage(target);
        target.Component(out PlayerBody player, false);
        if (player) {
            player.EmitBuff($"+{qty} {TranslateSystem.Translate(message)} !", color);
        }
    }

    public override void DestroyBuff(in Transform target){
        refresh.RefreshPlayParticle(Particle.CONSTANT,false);
        refresh.RefreshPlayParticle(Particle.DESTRUCTION);
        render.enabled = false;
        Destroy(this.gameObject, destroyDuration);
    }
    #endregion
}
