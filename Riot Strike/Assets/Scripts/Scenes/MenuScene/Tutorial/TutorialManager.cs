#region
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Environment;
using XavHelpTo.Set;
using XavHelpTo;
using XavHelpTo.Change;
using TutorialRefresh;
#endregion

public class TutorialManager : MonoBehaviour
{
    #region Variable
    private Tutorial[] tutorials;
    private int lastTutorial = -1;
    [Header("Achievement Manager")]
    public bool isInMenu;
    [Space]
    public Transform tr_parent_tutorialInfo;
    public GameObject pref_tutorial;
    #endregion
    #region Event
    private void Start()
    {
        lastTutorial = -1;

        if (isInMenu)
        {
            //asigns the menu and goal values
            tutorials = Set.ToArray(Data.TUTORIAL.MENU, Data.TUTORIAL.GOAL);
            LoadTutorialInfo(0);
        }
        else tutorials = Set.ToArray(Data.TUTORIAL.GAME);
        
    }
    #endregion
    #region Method

    /// <summary>
    /// Create a list of information
    /// </summary>
    private void GenerateInfo(in Tutorial tutorialInfo)
    {
        tr_parent_tutorialInfo.ClearChilds();
        //0. modificamos el 0 que es el TITULAR ?
        CreateInfo(tutorialInfo.TITLE, true);

        for (int i = 0; i < tutorialInfo.INFO.Length; i++)
        {
            CreateInfo(tutorialInfo.INFO[i]);
        }
    }

    /// <summary>
    /// Create the tutorial information item
    /// </summary>
    private void CreateInfo(string info, bool isTitle = false)
    {

        RefreshController _refresh = RefreshController.CreateRefresh(in pref_tutorial, in tr_parent_tutorialInfo);
        _refresh.RefreshText(RefreshText.INFO, in info);

        if (isTitle)
        {
            Text txt =  _refresh.GetText(RefreshText.INFO);
            txt.fontSize = 80;
            txt.alignment = TextAnchor.MiddleCenter;
        }
    }

    /// <summary>
    /// Loads the tutorial selected
    /// </summary>
    public void LoadTutorialInfo(int index)
    {
        if (lastTutorial.Equals(index)) return;// 🛡
        lastTutorial = index;
        GenerateInfo(in tutorials[index]);
    }
    /// <summary>
    /// Change the scene to the 
    /// </summary>
    public void PlayTutorial() => Scenes.TUTORIAL_SCENE.ToInt().ToScene();

    #endregion
}
/// <summary>
/// List of achievements
/// </summary>
[Serializable]
public struct TutorialList {
    public Tutorial GAME;
    public Tutorial MENU;
    public Tutorial GOAL;
}
/// <summary>
/// Structure of the achievements
/// </summary>
[Serializable]
public struct Tutorial
{
    public string TITLE;
    public string[] INFO;
}

namespace TutorialRefresh
{
    public enum RefreshText
    {
        INFO=0
    }
}