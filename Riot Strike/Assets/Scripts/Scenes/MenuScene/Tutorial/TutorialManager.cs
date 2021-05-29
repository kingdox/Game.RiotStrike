#region
using UnityEngine;
using UnityEngine.UI;
using System;
using Environment;
using XavHelpTo.Set;
using XavHelpTo;
using XavHelpTo.Change;
using TutorialRefresh;
#endregion

namespace MenuScene
{
    /// <summary>
    /// Manages the tutorial information showed in <see cref="Environment.Scenes.MENU_SCENE"/>
    /// </summary>
    public class TutorialManager : MonoBehaviour
    {
        #region Variable
        private int lastTutorial = -1;

        private const int QTY_MENU = 3;
        private const int QTY_GOAL = 2;

        public Tutorial[] tutorials;
        private Tutorial tutorialMenu = new Tutorial("_tutorials_opt_how", "_tutorials_menu", QTY_MENU);
        private Tutorial tutorialGoal = new Tutorial("_tutorials_opt_goal", "_tutorials_goal", QTY_GOAL);



        [Header("Achievement Manager")]
        public Transform tr_parent_tutorialInfo;
        public GameObject pref_tutorial;
        #endregion
        #region Event
        private void Start()
        {
            lastTutorial = -1;

            tutorialMenu.PushIn(ref tutorials);
            tutorialGoal.PushIn(ref tutorials);
            //asigns the menu and goal values
            LoadTutorialInfo(0);
        
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
            CreateInfo(tutorialInfo.titleKey, true);

            for (int i = 0; i < tutorialInfo.qty; i++)
            {
                CreateInfo($"{tutorialInfo.infoKey}_{i}");
            }
        }
    
        /// <summary>
        /// Create the tutorial information item
        /// </summary>
        private void CreateInfo(string info, bool isTitle = false)
        {

            RefreshController _refresh = RefreshController.CreateRefresh(in pref_tutorial, in tr_parent_tutorialInfo);
            //_refresh.RefreshText(RefreshText.INFO, in info);
            _refresh.Translate(RefreshText.INFO,info);

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
}

[Serializable]
public struct Tutorial
{
    public string titleKey;
    public string infoKey;
    public int qty;

    public Tutorial(string titleKey,string infoKey, int qty)
    {
        this.titleKey = titleKey;
        this.infoKey = infoKey;
        this.qty = qty;
    }
}

