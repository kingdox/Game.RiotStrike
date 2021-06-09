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
        private CanvasGroup[] canvaScreens;
        [Header("Game Manager")]
        public GameManagerModal currentModal = GameManagerModal.HUD;
        public Transform tr_parent_screens;
        public PlayerBody player;
        [Space]
        public ImageController imgCtrl_curtain;
        #endregion
        #region Events
        private void Awake()
        {
            imgCtrl_curtain.gameObject.SetActive(true);
            Time.timeScale = 1;
            CursorSystem.Hide();
        }
        private void OnEnable()
        {
            Subscribe();
        }
        private void Start() {
            tr_parent_screens.Components(out canvaScreens);
        }
        private void Update()
        {
            
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
        /// <summary>
        /// Pause the game or not
        /// </summary>
        public void Pause(){
            //isPause = !isPause;

            //TIME
            //Time.timeScale = (!isPause).ToInt();

            //DISPLAY
            /*
            canvaScreens[GameManagerModal.PAUSE.ToInt()]
                .gameObject
                .SetActive(isPause);

            if (isPause) CursorSystem.Show();
            else CursorSystem.Hide();
            */

            //    //si hay pausa retorna a la anterior
            //if (currentModal.Equals(GameManagerModal.PAUSE))
            //{
            //    ChangeModal(currentModal, true); // hide
            //    currentModal = GameManagerModal.HUD;
            //    ChangeModal(currentModal, false); //->show

            //}
            //else
            //{
            //    // sino, abre pausa
            //    ChangeModal(currentModal, true); // hide
            //    currentModal = GameManagerModal.PAUSE;
            //    ChangeModal(currentModal, false); // show

            //}

        }
        /// <summary>
        /// Change the modal in GameManager
        /// Do a fade or not.
        /// dependency with <seealso cref="Utils.Fade(bool, CanvasGroup)"/>
        /// </summary>
        IEnumerator ChangeModal( GameManagerModal toModal, bool fade){
            StartCoroutine(Utils.Fade(fade, GetCanvasOf(toModal)));
            yield return new WaitForEndOfFrame();
        }


        /// <summary>
        /// Returns the <seealso cref="CanvasGroup"/> of the specified element
        /// </summary>
        private CanvasGroup GetCanvasOf(GameManagerModal modal)
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


/// <summary>
/// TODO MOVER A UN SCRIPT COMMON
/// </summary>
[SerializeField]
public enum GameManagerModal
{
    HUD=0,
    PAUSE=1,
    END=2,
    CHEAT=3 // esta se cuenta por separado
}

//!Time.timeScale.Equals(0)