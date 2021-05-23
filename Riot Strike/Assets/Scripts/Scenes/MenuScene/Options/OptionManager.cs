#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Set;
using Environment;
#endregion
/// <summary>
/// Manages every option to configure for player
/// </summary>
public class OptionManager : MonoBehaviour
{
    #region Variable
    [Header("Option Manager")]
    public Scrollbar scroll;

    [Header("Music And Sound")]
    public Slider slider_music;
    public Slider slider_sound;
    public Slider slider_sensibility;

    [Header("Switch Options")]
    public bool[] switchConfigs; //Post processing, Invert X, Invert Y

    [Header("Controls")]
    public Button btn_back;


    #endregion
    #region Event
    private void Start(){

        btn_back.onClick.AddListener(SaveConfigurations);

        //Loads the saved data 
        SavedData saved = DataSystem.Get;
        slider_music.value = saved.musicPercent;
        slider_sound.value = saved.soundPercent;
        slider_sensibility.value = saved.sensibilityPercent;



        scroll.value = 1;
    }
    #endregion
    #region Method



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




