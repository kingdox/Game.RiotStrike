#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
using Environment;
# endregion
/// <summary>
/// Loads the json and then check if the saved data surpass the requirements to set a check
/// </summary>
public class AchievementManager : MonoBehaviour
{
    #region Variable
    [Header("Achievement Manager")]
    public Transform tr_parent_achievements;
    public GameObject pref_achievementItem;
    
    #endregion
    #region Events
    private void Awake()
    {
        
    }
    private void Start()
    {
        //LoadAchievements();


        foreach (Achievement achievement in Data.ACHIEVEMENT_DATA.ACHIEVEMENTS)
        {
            CreateAchievement(in achievement);
        }
    }
    #endregion
    #region Methods


    ///// <summary>
    ///// Load the achievements
    ///// </summary>
    //private void LoadAchievements() => "AchievementData".LoadJson(out achievementList);

    /// <summary>
    /// Create a achievement
    /// </summary>
    private void CreateAchievement(in Achievement achievement)
    {
        GameObject _obj = Instantiate<GameObject>(pref_achievementItem, tr_parent_achievements);
        AchievementController _ctrl_achievement;
        _obj.transform.Component(out _ctrl_achievement);

        _ctrl_achievement.RefreshAchievement(achievement, 0);


    }
    #endregion
}

/// <summary>
/// List of achievements
/// </summary>
[Serializable]
public struct AchievementList{public Achievement[] ACHIEVEMENTS; }
/// <summary>
/// Structure of the achievements
/// </summary>
[Serializable]
public struct Achievement
{
    public string NAME;
    public string DESCRIPTION;
    public int REQUIREMENT;
    public string DEBUG_REQUIREMENT;
}