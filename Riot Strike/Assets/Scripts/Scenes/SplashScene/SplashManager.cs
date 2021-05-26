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
    public CanvasGroup canvasGroup;
    [Range(1,5)]
    public float speed=1;
    #endregion
    #region Event
    private void Start()
    {
        langSelected = false;
        canvasGroup.alpha = 0;
    }
    private void Update()
    {
        if (!flag_splash) ManageSplash();
        else if (!flag_exit) ManageExit();
    }
    #endregion
    #region Method

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
            GoTo(true);
        }
        
    }


    /// <summary>
    /// Displays
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisplayLanguageModal()
    {

        //Mostramos el canvas
        yield return Fade(false, canvasGroup);


        //esperamos que le de aceptar y esto cambiará con el tiempo a invisible
        while (!langSelected)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return Fade(true, canvasGroup);

    }

    private IEnumerator Fade(bool fade, CanvasGroup canvasGroup)
    {
        int init = fade.ToInt();
        int end = (!fade).ToInt();

        while (!end.Equals(canvasGroup.alpha))
        {
            canvasGroup.alpha += Mathf.Lerp(init, end, Time.deltaTime * speed).Min(0).Max(1);
            yield return new WaitForEndOfFrame();
        }

        $"Fade {fade}, {init} to {end}".Print();
    }


    /// <summary>
    /// Change the scene to the menu or to the tutorial if is first time
    /// </summary>
    /// <param name="toMenu"></param>
    private void GoTo(bool toMenu)
    {
        if (toMenu) Scenes.MENU_SCENE.ToScene();
        else Scenes.TUTORIAL_SCENE.ToScene();
    }


    public void SetLanguage(string lang)
    {

    }
    #endregion
}



