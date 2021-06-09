#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
# endregion
/// <summary>
/// Manage the menu in main menu
/// </summary>
public class MenuManager : MonoBehaviour
{
    #region Variables
    private enum MenuOpt
    {
        SINGLEPLAYER,
        MULTIPLAYER,
        OPTIONS,
        ACHIEVEMENTS,
        CREDITS,
        EXIT
    }

    [Header("Menu Manager")]

    public CameraComponent comp_cam;
    [Range(1, 10)]
    public int timeToExit;
    [Space]
    public ImageController imgCtrl_curtain;
    #endregion
    #region Events
    private void Awake()
    {
        imgCtrl_curtain.gameObject.SetActive(true);
    }
    private void Start()
    {
       // $"Bienvenido a {TranslateSystem.Translate("game")}".Print("blue");
        CursorSystem.Show();
    }
    #endregion
    #region Methods


    /// <summary>
    /// Move the camera and wait until the camera reach at the end of the Scene
    /// </summary>
    public void ExitGame(){
        imgCtrl_curtain.Invert();
        comp_cam.SetCameraPoint(MenuOpt.EXIT.ToInt());
        StartCoroutine(WaitToExit());
    }

    /// <summary>
    /// Waits to player to then
    /// </summary>
    IEnumerator WaitToExit()
    {
        yield return new WaitForSeconds(timeToExit);
        //"Adiós !".Print("magenta");
        Application.Quit();
    }
    #endregion
}
