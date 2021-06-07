#region Access
using System.Collections;
using UnityEngine;
using XavHelpTo.Know;
using Environment;
using Dat = Environment.Data;
using XavHelpTo.Change;
using LangRefresh;
#endregion
namespace SplashScene
{
    /// <summary>
    /// Manages the splash and if is new adjust the data to set the default info
    /// in <see cref="Environment.Scenes.SPLASH_SCENE"/>
    /// </summary>
    public class SplashManager : MonoBehaviour
    {
        #region Variable
        private float count;
        private bool flag_splash;
        private bool flag_exit;
        private float countToGo;
        private bool langSelected;
        [Header("SplashManager")]
        [Tooltip("Cuanto tiempo se esperará?")]
        [Range(2, 10)]
        public float timeInSplash;
        [Range(2, 10)]
        public float timeInToGo;
        [Tooltip("el Controlador que activaremos tras pasar el tiempo")]
        public ImageController imgCtrl_Splash;

        [Header("Lang Modal")]
        public GameObject pref_button;
        public Transform tr_parent_buttons;
        public CanvasGroup canvasGroup;
        //[Range(0.1f,5f)]
        //public float speed=.5f;
        #endregion
        #region Event
        private void Start()
        {
            CursorSystem.Hide();
            langSelected = false;
            canvasGroup.alpha = 0;
            GenerateModalButtons();
        }
        private void Update()
        {
            if (!flag_splash) ManageSplash();
            else if (!flag_exit) ManageExit();
        }
        #endregion
        #region Method
        /// <summary>
        /// Generate the list of buttons
        /// </summary>
        private void GenerateModalButtons() {
            foreach (string lang in Environment.Data.LANGUAGES){
                CreateButton(lang);
            }
        }
        /// <summary>
        /// Generate a button language
        /// </summary>
        /// <param name="lang"></param>
        private void CreateButton(string lang) {
            RefreshController _refresh = RefreshController.CreateRefresh(in pref_button, in tr_parent_buttons);
            _refresh.RefreshText(RefreshText.LANG, lang);

            //se le añade un actualizador de traducción
            _refresh.GetButton(RefreshButton.BUTTON).onClick.AddListener(delegate { SetLang(lang); });
        }

        /// <summary>
        /// Updates the information and save the language of the user
        /// </summary>
        /// <param name="lang"></param>
        private void SetLang(string lang)
        {
            TranslateSystem.InitLang(lang);
            SavedData saved = DataSystem.Get;
            saved.currentLang = lang;
            DataSystem.Set(saved);
        }

        /// <summary>
        /// Manages he display of the splash
        /// </summary>
        private void ManageSplash()
        {
            if (timeInSplash.TimerFlag(ref flag_splash, ref count)) imgCtrl_Splash.Invert();
        }

        /// <summary>
        /// Manage the actions to go then it's done the time to wait
        /// </summary>
        private void ManageExit()
        {
            if (!timeInToGo.TimerFlag(ref flag_exit, ref countToGo)) return;


            SavedData saved = DataSystem.Get;

            // if is a new user
            if (!saved.isOld)
            {

                saved.achievementsPoints = new int[Dat.ACHIEVEMENTS.Length];
                saved.musicPercent = 0.5f;
                saved.soundPercent = 0.5f;
                saved.sensibilityPercent = 0.5f;
                saved.controlKeys = new string[Dat.CONTROLS.Length];
                for (int i = 0; i < saved.controlKeys.Length; i++) saved.controlKeys[i]=Dat.CONTROLS[i].KEY;
                DataSystem.Set(saved);
                
                StartCoroutine(DisplayLanguageModal());
            }
            else GoTo(saved.tutorialDone);
        
        }

        /// <summary>
        /// Indice that the lang was selected
        /// </summary>
        public void AcceptLang() => langSelected = !langSelected;

        /// <summary>
        /// Displays
        /// </summary>
        /// <returns></returns>
        private IEnumerator DisplayLanguageModal()
        {
            CursorSystem.Show();

            //Mostramos el canvas
            yield return Utils.Fade(false, canvasGroup);
            //esperamos que le de aceptar y esto cambiará con el tiempo a invisible
            while (!langSelected){yield return new WaitForEndOfFrame();}
            yield return Utils.Fade(true, canvasGroup);


            SavedData saved = DataSystem.Get;
            saved.isOld = true;
            if (saved.currentLang.Equals("")) saved.currentLang = TranslateSystem.DEFAULT_LANG;
            DataSystem.Set(saved);
            DataSystem.Save();
            //Ver intro
            GoTo(false);
        }



        /// <summary>
        /// Change the scene to the intro video or to the tutorial if is first time
        /// </summary>
        private void GoTo(bool toIntro)
        {
            if (toIntro) Scenes.INTRO_SCENE.ToScene();
            else Scenes.TUTORIAL_SCENE.ToScene();
        }


   
        #endregion
    }
}

