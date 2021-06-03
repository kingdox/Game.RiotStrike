#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using Environment;
# endregion

namespace TutorialScene
{
    /// <summary>
	/// Manages the requirements of the player, checking if each requirement was completed to then go to the next scene
	/// in <see cref="Environment.Scenes.TUTORIAL_SCENE"/>
	/// </summary>
    public class TutorialGameManager : MonoBehaviour
    {
        #region Variables
        private KeyCode skipCode;
        [Header("Tutorial Game Manager")]
        public RefreshController refresh_skip;
        public GameObject obj_shotCursor; 

        #endregion
        #region Event
        private void OnEnable()
        {
            CursorSystem.OnCursor += DisplayShotCursor;
        }
        private void Start()
        {
            CursorSystem.Hide();
            string KEY = DataSystem.Get.controlKeys[EControl.PAUSE.ToInt()];

        }
        private void OnDisable()
        {
            CursorSystem.OnCursor -= DisplayShotCursor;
        }
        #endregion
        #region Methods

        /// <summary>
        /// Displays the Cursor Shot or not
        /// </summary>
        public void DisplayShotCursor(bool condition) => obj_shotCursor.SetActive(condition);


        /// <summary>
        /// Go to the menu
        /// </summary>
        public void GoToMenu() => Scenes.MENU_SCENE.ToScene();
        #endregion
    }
}

/*TODO
 * - Script Base de requerimiento
 * - Script de contacto
 * - Script de Input tecleado
 * 
 * 
 * 
 * 
 * 
 */