#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using UnityEngine.InputSystem;
#endregion
/// <summary>
/// Manages every option to configure for player
/// </summary>
public class OptionManager : MonoBehaviour
{
    #region Variable

    [Header("")]
    public Scrollbar scroll;

    [Header("Music And Sound")]
    public Slider slider_music;
    public Slider slider_sound;
    public Slider slider_sensibility;

    [Header("Controls")]
    public Button btn_back;


    #endregion
    #region Event
    private void Start(){
        scroll.value = 1;

        //Loads the saved data 
        SavedData saved = DataSystem.Get;
        slider_sensibility.value = saved.sensibilityPercent;
        slider_music.value = saved.musicPercent;
        slider_sound.value = saved.soundPercent;
    }
    #endregion
    #region Method


    public void SetNewKey(InputAction.CallbackContext ctx)
    {
        //ctx.action.name
    }


    /// <summary>
    /// Save every configuration changed in Game
    /// </summary>
    public void SaveConfigurations()
    {
        AudioSystem.SavedBValues();

    }


    /// <summary>
    /// Change Sensibility
    /// </summary>
    public static void SetSensibility(float percent)
    {
        SavedData saved = DataSystem.Get;
        saved.sensibilityPercent = percent;
    }

    #endregion
}


public enum OptionSwitch
{
    POST_PROCESS
}
/* Opciones
 * 
 * Cambiar la musica
 * Cambiar el sonido
 * 
 * Cambiar los controles (Levantar un modal 3D)
 * Ver Como Jugar (levantar un modal 3D) 
 * Jugar el Tutorial (Cambiar Escena al tutorial)
 *
 *
 * Nota: los modales 3D que se levanten ya existen, solo mueves a la persona por el mapa
 */
