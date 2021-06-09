#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using Environment;
using TutorialGameRefresh;
#endregion

namespace TutorialScene
{
    /// <summary>
	/// Manages the requirements of the player, checking if each requirement was completed to then go to the next scene
	/// in <see cref="Environment.Scenes.TUTORIAL_SCENE"/>
	/// </summary>
    public class TutorialGameManager : MonoBehaviour
    {
        #region Variables
        private const int QTY_GAME_TUTORIALS = 4; // TODO multiplayer
        //private Tutorial tutorialGame = new Tutorial("_tutorials_title", "_tutorials_menu", QTY_GAME_TUTORIALS);
        [Header("Tutorial Game Manager")]
        public RefreshController refresh_skip;
        public PlayerBody player;
        public ImageController imgCtrl_curtain;

        #endregion
        #region Event
        private void Awake()
        {
            CursorSystem.Hide();
            imgCtrl_curtain.gameObject.SetActive(true);
        }
        private void OnEnable()
        {
            Subscribe();
        }
        private void Start()
        {
            string MSG_1 = "_intro_skip_0".Translate();
            string KEY = DataSystem.Get.controlKeys[EControl.PAUSE.ToInt()];
            string MSG_2 = "_intro_skip_1".Translate();

            refresh_skip.RefreshText(Text.SKIP, $"{MSG_1} {KEY} {MSG_2}");

        }
        private void OnDisable()
        {
            UnSuscribe();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Do the subscriptions of the actions
        /// </summary>
        private void Subscribe()
        {
            player.OnPause += GoToMenu;
        }
        /// <summary>
        /// Do the unsuscriptions of the actions
        /// </summary>
        private void UnSuscribe()
        {
            player.OnPause -= GoToMenu;
        }

        /// <summary>
        /// Go to the menu
        /// </summary>
        public void GoToMenu() => Scenes.MENU_SCENE.ToScene();
        #endregion
    }
}

