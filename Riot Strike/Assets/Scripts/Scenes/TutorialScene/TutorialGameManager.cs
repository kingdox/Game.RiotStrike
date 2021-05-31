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


        #endregion
        #region Event
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Start()
        {
            string KEY = DataSystem.Get.controlKeys[EControl.PAUSE.ToInt()];

        }
        #endregion
        #region Methods



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