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
    public static TargetManager _;

    public Transform[] tr_spawnPoints;
    [Header("Target Manager")]
    public Transform tr_parent_spawnPoints;
    public Transform tr_parent_patrols;
    public Transform tr_parent_enemies;

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
    /// Find the instance in the hierarchy
    /// </summary>
    //public static TargetManager Get => FindObjectOfType<TargetManager>();
    #endregion
}
