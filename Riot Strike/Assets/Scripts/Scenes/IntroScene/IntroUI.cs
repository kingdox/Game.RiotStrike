#region Access
using UnityEngine;
# endregion
namespace IntroScene
{
    /// <summary>
    /// Class used to manages the ui animations in intro scene
    /// in <see cref="Environment.Scenes.INTRO_SCENE"/>
    /// </summary>
    public class IntroUI : MonoBehaviour
    {
        #region Method
        /// <summary>
        /// Call the start of the gun using
        /// </summary>
        public void CallStart() => IntroManager.GunAnimShow();
        /// <summary>
        /// advice to go to the menu in <see cref="IntroManager"/>
        /// </summary>
        public void CallToGo() => IntroManager.GoToMenu();
        #endregion
    }
}
