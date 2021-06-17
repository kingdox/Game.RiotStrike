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

    //TODO Peligro con colocar campos con cambios dinamicos pues aplica global


    //TODO mover las del json EnemyGeneratorData para acá
    // ya que aqui forman parte del tipo correspondiesnte

    //Spell Casting
    [HideInInspector]  public float percentCastSpell;

    //Aiming
    [HideInInspector] public float delayAttack;
    
    
    //LOOK
    [HideInInspector] public Vector3 lastSeenTargetLocation;
    public float viewDepth = 100;
    public float viewWidth = 0;//0 or 1, TODO check this


    #endregion
    #region Methods

    #endregion
}
