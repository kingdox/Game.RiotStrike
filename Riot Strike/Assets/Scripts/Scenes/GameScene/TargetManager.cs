#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
# endregion
/// <summary>
/// Manager the targets in game, as a pointer
/// </summary>
public class TargetManager : MonoBehaviour
{
    #region Variables
    private static TargetManager _;

    [HideInInspector] private Transform[] tr_spawnPoints;
    [Header("Target Manager")]
    public Transform tr_parent_spawnPoints;
    [Space]
    public Transform tr_parent_patrols;
    public Transform tr_parent_enemies;
    [Space]
    public Transform tr_parent_temporalElements;

    #endregion
    #region Event
    private void Awake()
    {
        this.Singleton(ref _, false);
        tr_parent_spawnPoints.Components(out tr_spawnPoints);
    }
    private void Start()
    {

        
    }
    #endregion
    #region Methods
    /// <summary>
    /// Check if exist the Target manager
    /// </summary>
    public static bool Exist => !!_;
    /// <summary>
    /// Get the parent of the patrols
    /// </summary>
    public static Transform GetParentPatrols => _.tr_parent_patrols;

    /// <summary>
    /// Get the parent where all the enemies are setted
    /// </summary>
    public static Transform GetParentEnemies => _.tr_parent_enemies;

    /// <summary>
    /// Returns the Points to spawn characters, player or enemies
    /// </summary>
    public static Transform[] SpawnPoints => _.tr_spawnPoints;

    /// <summary>
    /// Returns the container of the temporal elements
    /// </summary>
    public static Transform GetParentTemporalElements => _.tr_parent_temporalElements;

    /// <summary>
    /// Find the instance in the hierarchy
    /// </summary>
    //public static TargetManager Get => FindObjectOfType<TargetManager>();
    #endregion
}
