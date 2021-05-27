#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Change;
# endregion
/// <summary>
/// Manages the intro video
/// </summary>
public class IntroManager : MonoBehaviour
{
    #region Variable

    [Tooltip("Usado para animar el movimiento del arma y ejecucion")]
    public Animator animator_gun;
    [Tooltip("Usado para el manejo de las pantallas y todo lo que se muestre")]
    public Animator animator_UI;

    #endregion
    #region Event

    #endregion
    #region Method




    /// <summary>
    /// Changes the scene to the menu
    /// </summary>
    public void GoToMenu() => Environment.Scenes.MENU_SCENE.ToScene();
    #endregion
}
