#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Set;
using XavHelpTo.Change;
using Environment;
using Dat = Environment.Data;
# endregion
namespace MenuScene
{
    /// <summary>
    /// Manage the menu in main menu
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        #region Variables
        private enum MenuOpt
        {
            SINGLEPLAYER,
            MULTIPLAYER,
            OPTIONS,
            ACHIEVEMENTS,
            CREDITS,
            EXIT
        }

        [Header("Menu Manager")]
        public Text txt_version;
        [Space]
        public CameraComponent comp_cam;
        [Range(1, 10)]
        public int timeToExit;
        [Space]
        public ImageController imgCtrl_curtain;
        [Space]
        public AudioClip clip_music;
        #endregion
        #region Events
        private void Awake()
        {
            Time.timeScale = 1;
            imgCtrl_curtain.gameObject.SetActive(true);
        }
        private void Start()
        {
            txt_version.text = $"V-1.0.0";//Application.version

            AudioSystem.Play(clip_music);
           // $"Bienvenido a {TranslateSystem.Translate("game")}".Print("blue");
            CursorSystem.Show();
        }
        #endregion
        #region Methods


        /// <summary>
        /// Moves the camera and wait until the camera reach at the end of the Scene
        /// </summary>
        public void ExitGame(){
            imgCtrl_curtain.Invert();
            comp_cam.SetCameraPoint(MenuOpt.EXIT.ToInt());
            StartCoroutine(WaitToExit());
        }

        /// <summary>
        /// Waits to player to then
        /// </summary>
        IEnumerator WaitToExit()
        {
            yield return new WaitForSeconds(timeToExit);
            //"Adiós !".Print("magenta");
            Application.Quit();
        }

        /// <summary>
        /// Starts to play the game
        /// </summary>
        public void PlayGame() => StartCoroutine(WaitToPlayGame());

        /// <summary>
        /// Do a delay, hidding the screen to change to <seealso cref="Scenes.GAME_SCENE"/>
        /// </summary>
        IEnumerator WaitToPlayGame()
        {

            imgCtrl_curtain.Invert();
            DataSystem.Save();
            yield return new WaitForSeconds(timeToExit);
            Scenes.GAME_SCENE.ToScene();
        }


        /// <summary>
        /// Opens a link
        /// </summary>
        public void OpenLink(string url)
        {
            try
            {
                Application.OpenURL(url);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }
        }
        #endregion
    }
}
