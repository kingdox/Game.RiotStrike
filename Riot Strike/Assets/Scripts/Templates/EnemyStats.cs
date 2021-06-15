#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// EnemyStats
///</summary>
[CreateAssetMenu(menuName = "Template/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    #region Variables
    public float patrolSpeed = 1f;
    public float attackSpeed = 2.5f;
    public float reach = 50f;
    public float timeToDisengage = 20f;
    public float minAttackRange = 5f;
    public float lookSphereCastRadius = 3f;

#endregion
#region Events
#endregion
#region Methods
#endregion
}
