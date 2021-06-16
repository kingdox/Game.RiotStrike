#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
# endregion
/// <summary>
/// Manages the stat of the IA
/// </summary>
[CreateAssetMenu(menuName = "IA/IAStat")]
public class IAStat: ScriptableObject
{
    #region Variables
    [Header("IA Stat")]
    
    //TODO mover las del json EnemyGeneratorData para acá
    // ya que aqui forman parte del tipo correspondiente

    //Spell Casting
    [HideInInspector]  public float percentCastSpell;

    //Aiming
    [HideInInspector] public float delayAttack;
    [HideInInspector] public Vector3 lastSeenTargetLocation;
    public float viewDepth = 100;
    public float viewWidth = 0;//0 or 1, TODO study this


    #endregion
    #region Methods

    #endregion
}
