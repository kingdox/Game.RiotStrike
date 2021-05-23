#region Access
using System;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Set;
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
    private void Start()
    {
        SavedData saved = DataSystem.Get;
        GenerateAchievements(ref saved);
    }
    #endregion
    #region Methods


    /// <summary>
    /// Creates the acheivements
    /// </summary>
    private void GenerateAchievements(ref SavedData saved)
    {

        if (!saved.achievementsPoints.Length.Equals(AchievementQty))
        {   
            //Set the new length
            saved.achievementsPoints = Set.Length(in saved.achievementsPoints, AchievementQty);
            DataSystem.Set(saved);
            DataSystem.Save();
            $"{nameof(AchievementManager)} => Asignado nueva dimension a los logros".Print("blue");
        }

        for (int i = 0; i < AchievementQty; i++) CreateAchievement(in Data.ACHIEVEMENTS[i], saved.achievementsPoints[i]);
    }

    /// <summary>
    /// Create a achievement
    /// </summary>
    private void CreateAchievement(in Achievement achievement, int pts)
    {
        GameObject _obj = Instantiate<GameObject>(pref_achievementItem, tr_parent_achievements);
        AchievementController _ctrl_achievement;
        _obj.transform.Component(out _ctrl_achievement);

        _ctrl_achievement.RefreshAchievement(achievement, pts);
    }

    /// <summary>
    /// Qty of achievements
    /// </summary>
    private int AchievementQty => Data.ACHIEVEMENTS.Length;
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