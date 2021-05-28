#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Change;
using IntroRefresh;
#endregion
/// <summary>
/// Manages the intro video
/// </summary>
public class IntroManager : MonoBehaviour
{
    #region Variable

    [Tooltip("Usado para animar el movimiento del arma y ejecucion")]
    public Animator animator_gun;
    [Space]
    [Tooltip("Usado para el manejo de las pantallas y todo lo que se muestre")]
    public Animator animator_UI;
    [Space]
    public RefreshController refresh_skip;

    #endregion
    #region Event
    private void Awake()
    {
        //Hides the cursor while is seeeing the animation inte
        Cursor.visible = false;

    }
    private void Start()
    {
        string MSG_1 = TranslateSystem.Translate("_intro_skip_0");
        string MSG_KEY = Environment.Data.Control(EControl.PAUSE).KEY;
        string MSG_2 = TranslateSystem.Translate("_intro_skip_1");

        refresh_skip.RefreshText(SkipText.SKIP, $"{MSG_1} {MSG_KEY} {MSG_2}");
    }
    #endregion
    #region Method


    public void GunAnimShow() { }

    public void UiAnimTransparency(bool setTransparent) { }

    public void SkipMessageDisable() { }


    /// <summary>
    /// Changes the scene to the menu
    /// </summary>
    public void GoToMenu() => Environment.Scenes.MENU_SCENE.ToScene();
    #endregion
}

namespace IntroRefresh
{
    enum SkipText
    {
        SKIP
    }
}