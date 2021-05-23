#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
# endregion
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

    #endregion
    #region Event
    private void Start(){
        scroll.value = 1;

        //Loads the saved data 
        SavedData saved = DataSystem.Get;
        slider_music.value = saved.musicPercent;
        slider_sound.value = saved.soundPercent;
    }
    #endregion
    #region Method



    ///// <summary>
    ///// Save the changes and sets the new value
    ///// percent is normalized in 0-1
    ///// </summary>
    //public static void SetMusic(float percent) {
    //    AudioSystem.SetSound(percent);
    //    AudioSystem.SavedBValues();

    //}
    ///// <summary>
    ///// Save the changes and sets the new value
    ///// percent is normalized in 0-1
    ///// </summary>
    //public static void SetSound(float percent)
    //{
    //    AudioSystem.SetSound(percent);
    //    AudioSystem.SavedBValues();
    //}

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
