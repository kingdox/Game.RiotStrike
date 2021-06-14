#region Access
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using IntroRefresh;
#endregion
namespace IntroScene
{
    /// <summary>
    /// Manages the intro video
    /// in <see cref="Environment.Scenes.INTRO_SCENE"/>
    /// </summary>
    public class IntroManager : MonoBehaviour
    {
        #region Variable
        private static IntroManager _;
        public const string TRIGGER_KEY = "Shot";
        [Tooltip("Usado para animar el movimiento del arma y ejecucion")]
        public Animator animator_gun;
        [Space]
        [Tooltip("Usado para el manejo de las pantallas y todo lo que se muestre")]
        public Animator animator_UI;
        [Space]
        public RefreshController refresh_skip;
        [Space]
        public AudioClip clip_music;
        #endregion
        #region Event
        private void Awake(){
            this.Singleton(ref _,false);

            CursorSystem.Hide();
        }
        private void Start()
        {
            AudioSystem.Play(clip_music);

            string MSG_1 = TranslateSystem.Translate("_intro_skip_0");
            string KEY = DataSystem.Get.controlKeys[EControl.PAUSE.ToInt()];
            string MSG_2 = TranslateSystem.Translate("_intro_skip_1");
            refresh_skip.RefreshText(SkipText.SKIP, $"{MSG_1} {KEY} {MSG_2}");
        }
        private void Update()
        {
            if (EControl.PAUSE.IsPressedDown()) GoToMenu();
        }
        #endregion
        #region Method

        /// <summary>
        /// Starts the Gun animation
        /// </summary>
        public static void GunAnimShow() => _.animator_gun.SetTrigger(TRIGGER_KEY);

        /// <summary>
        /// Ends the introduction with the last animation
        /// </summary>
        public static void UIAnimEnd() => _.animator_UI.SetTrigger(TRIGGER_KEY);

        /// <summary>
        /// Changes the scene to the menu
        /// </summary>
        public static void GoToMenu() => Environment.Scenes.MENU_SCENE.ToScene();
        #endregion
    }
}