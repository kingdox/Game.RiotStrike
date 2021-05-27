#region Access
using System.Collections;
using System;
using UnityEngine;
using XavHelpTo.Know;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Set;
using Environment;
using XavHelpTo.Change;
using LangRefresh;
#endregion
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
        //_refresh.Translate(RefreshText.LANG, lang); Esto no se traduce para tenerlo para cada idioma

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

        if (!saved.isOld)
        {
            StartCoroutine(DisplayLanguageModal());
        }
        else
        {
            GoTo(saved.tutorialDone);
        }
        
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
        //Mostramos el canvas
        yield return Fade(false, canvasGroup);
        //esperamos que le de aceptar y esto cambiará con el tiempo a invisible
        while (!langSelected){yield return new WaitForEndOfFrame();}
        yield return Fade(true, canvasGroup);


        SavedData saved = DataSystem.Get;
        saved.isOld = true;
        DataSystem.Set(saved);
        DataSystem.Save();
        //Ver intro
        GoTo(false);
    }

    private IEnumerator Fade(bool fade, CanvasGroup canvasGroup)
    {
        float end = (!fade).ToInt();
        while (!canvasGroup.alpha.Equals(end))
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, end, (Time.time*Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
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



namespace LangRefresh
{
    public enum RefreshButton
    {
        BUTTON=0
    }
    public enum RefreshText
    {
        LANG = 0,
    }
}