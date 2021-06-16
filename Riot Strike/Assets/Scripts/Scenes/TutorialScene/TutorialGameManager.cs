#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
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
        private const int QTY_GAME_TUTORIALS = 10;
        private const string KEY_TUTORIAL_GAME = "_tutorials_game_";
        [SerializeField] private int lastIndexHint=-1;
        //private Tutorial tutorialGame = new Tutorial("_tutorials_title", "_tutorials_menu", QTY_GAME_TUTORIALS);
        [Header("Tutorial Game Manager")]
        public RefreshController refresh_tutorialScreen;
        public PlayerBody player;
        public ImageController imgCtrl_curtain;
        public ContactComponent contact_exit;
        [Space]
        public GameObject obj_postProcessing;
         [Space]
        public AudioClip clip_music;
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

            refresh_tutorialScreen.RefreshText(Text.SKIP, $"{MSG_1} {KEY} {MSG_2}");

            obj_postProcessing.SetActive(DataSystem.Get.switch_configs[ESwitchOpt.POST_PROCESSING.ToInt()]);

            AudioSystem.Play(clip_music);
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
            contact_exit.OnContact += GoToMenu;
            player.OnInsert += ShowNextHint;
            player.OnPause += GoToMenu;
        }
        /// <summary>
        /// Do the unsuscriptions of the actions
        /// </summary>
        private void UnSuscribe()
        {
            contact_exit.OnContact += GoToMenu;
            player.OnInsert -= ShowNextHint;
            player.OnPause -= GoToMenu;
        }

        /// <summary>
        /// Go to the menu
        /// </summary>
        public void GoToMenu()
        {
            SavedData saved = DataSystem.Get;
            //Achievement Tutorial Done
            saved.achievementsPoints["tutorial_done".IndexAchievement()]++;

            bool tutorialWasDone = saved.tutorialDone;

            saved.tutorialDone = true;
            DataSystem.Set(saved);
            DataSystem.Save();


            if (tutorialWasDone) Scenes.MENU_SCENE.ToScene();
            else Scenes.INTRO_SCENE.ToScene();

        }

        /// <summary>
        /// Show the following hint based on a index
        /// </summary>
        [ContextMenu("Next Hint")]
        public void ShowNextHint() {
            lastIndexHint = Know.NextIndex(true, QTY_GAME_TUTORIALS, lastIndexHint);
            refresh_tutorialScreen.Translate(Text.HINT_MESSAGE, (KEY_TUTORIAL_GAME + lastIndexHint));
        }
        #endregion
    }
}

