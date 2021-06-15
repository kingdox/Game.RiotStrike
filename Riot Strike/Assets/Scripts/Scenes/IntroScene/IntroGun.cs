#region Access
using UnityEngine;
# endregion
namespace IntroScene
{
    /// <summary>
    /// Class used to manages the animations in intro scene
    /// in <see cref="Environment.Scenes.INTRO_SCENE"/>
    /// </summary>
    public class IntroGun : MonoBehaviour
    {
        #region Variables
        public ParticleSystem part_shot;
        public AudioClip clipShort;
        #endregion
        #region
        /// <summary>
        /// Shows the shot animation
        /// </summary>
        public void GunAnimShot()
        {

            AudioSystem.PlaySound(clipShort);

            part_shot.Play();
        }
        /// <summary>
        /// Call <see cref="IntroManager"/> to advice and then starts the end animation
        /// </summary>
        public void CallToEnd() => IntroManager.UIAnimEnd();
        #endregion
    }
}