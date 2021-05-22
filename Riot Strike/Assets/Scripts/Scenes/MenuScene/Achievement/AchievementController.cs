#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
# endregion
/// <summary>
/// Controls the info into the achivement
/// </summary>
public class AchievementController : MonoBehaviour
{
    #region Variable
    public Image img_icon;
    public Text txt_title;
    public Text txt_description;
    #endregion
    #region Event
    private void Awake()
    {
        img_icon.enabled = false;
    }
    #endregion
    #region Methods

    /// <summary>
    /// Refresh all the information in Achievement
    /// </summary>
    /// <param name="achievement"></param>
    public void RefreshAchievement(Achievement achievement, int requirementValue)
    {
        bool isDone = requirementValue >= achievement.REQUIREMENT; ;

        txt_title.text = achievement.NAME;
        txt_description.text = achievement.DESCRIPTION;
        //if surpass the requirements
        img_icon.enabled = isDone;

    }
    #endregion
}
