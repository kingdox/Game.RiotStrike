#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Change;
using Environment;
# endregion
namespace GameScene
{
    /// <summary>
    /// Manager of the Game Scene
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Variables
        private static GameManager _;
        private CanvasGroup[] canvaScreens;
        [Header("Game Manager")]
        public EGameModal currentModal = EGameModal.HUD;
        public Transform tr_parent_screens;
        [Space]
        public PlayerBody player;
        public Character[] characters;
        public bool IsPause { get; private set; } = false;
        [Space]
        public ImageController imgCtrl_curtain;
        #endregion
        #region Events
        private void Awake()
        {
            this.Singleton(ref _, false);

            imgCtrl_curtain.gameObject.SetActive(true);
            Time.timeScale = 1;
            CursorSystem.Hide();
        }
        private void OnEnable()
        {
            Subscribe();
        }
        private void Start() {
            SetPlayerCharacter();

            tr_parent_screens.Components(out canvaScreens);
            StartCoroutine(ChangeModal(EGameModal.HUD, false));
        }
        private void OnDisable()
        {
            UnSuscribe();
        }
        #endregion
        #region MEthods
        /// <summary>
        /// Do the subscriptions of the actions
        /// </summary>
        private void Subscribe()
        {
            player.OnPause += Pause;
        }
        /// <summary>
        /// Do the unsuscriptions of the actions
        /// </summary>
        private void UnSuscribe()
        {
            player.OnPause -= Pause;
        }
        private void SetPlayerCharacter()
        {
            SavedData saved = DataSystem.Get;
            player.character = characters[saved.characterSelected];
            //$"Character Selected: {(ECharacter)saved.characterSelected}".Print("blue");
            player.gameObject.SetActive(true);
        }
        /// <summary>
        /// Pause the game or not
        /// </summary>
        public void Pause(){
            IsPause = !IsPause;

            Time.timeScale = (!IsPause).ToInt();

            if (IsPause)
            {
                CursorSystem.Show();
                StartCoroutine(ChangeModal(EGameModal.PAUSE, false));
            }
            else
            {
                CursorSystem.Hide();
                StartCoroutine(ChangeModal(EGameModal.HUD, false));
            }

        }
        /// <summary>
        /// Change the modal in GameManager
        /// Do a fade or not.
        /// dependency with <seealso cref="Utils.Fade(bool, CanvasGroup)"/>
        /// </summary>
        IEnumerator ChangeModal(EGameModal toModal, bool fade){
            foreach (CanvasGroup c in canvaScreens)
            {
                //hides the others modals
                if (!c.Equals(GetCanvasOf(toModal))) StartCoroutine(Utils.Fade(true, c));
            }
            StartCoroutine(Utils.Fade(fade, GetCanvasOf(toModal)));
            yield return new WaitForEndOfFrame();
        }


        /// <summary>
        /// Returns the <seealso cref="CanvasGroup"/> of the specified element
        /// </summary>
        private CanvasGroup GetCanvasOf(EGameModal modal)
        {
            tr_parent_screens.GetChild(modal.ToInt()).Component(out CanvasGroup canvas);
            return canvas;
        }


        /// <summary>
        /// Go to the MenuScene
        /// </summary>
        public void GoToMenu()
        {
            Time.timeScale = 1;
            Scenes.MENU_SCENE.ToScene();
        }
        #endregion
    }
}
//!Time.timeScale.Equals(0)