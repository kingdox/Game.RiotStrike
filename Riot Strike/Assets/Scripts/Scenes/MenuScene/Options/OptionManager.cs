#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Set;
using Environment;
using Switch = OptionRefresh.Switch;
#endregion
/// <summary>
/// Manages every option to configure for player
/// </summary>
public class OptionManager : MonoBehaviour
{
    #region Variable
    private const string ACTIVE = "Activado";
    private const string DISABLE = "Desactivado";
    [Header("Option Manager")]
    public Scrollbar scroll;

    [Header("Music And Sound")]
    public Slider slider_music;
    public Slider slider_sound;
    public Slider slider_sensibility;



    [Header("Switch Options")]
    public RefreshController[] refresh_switchs;

    [Header("Controls")]
    public Button btn_back;


    #endregion
    #region Event
    private void Start(){
        btn_back.onClick.AddListener(SaveConfigurations);
        LoadConfigurations();
    }
    #endregion
    #region Method
    /// <summary>
    /// Change the information showed based on a condition
    /// </summary>
    private void SwitchItemStatus(RefreshController refresh, bool condition){refresh.GetText(Switch.RefreshText.VALUE).text = condition ? ACTIVE : DISABLE;}

    /// <summary>
    /// Loads the saved data to set all the saved configurations in game
    /// </summary>
    public void LoadConfigurations()
    {
        SavedData saved = DataSystem.Get;

        slider_music.value = saved.musicPercent;
        slider_sound.value = saved.soundPercent;
        slider_sensibility.value = saved.sensibilityPercent;

        int length = refresh_switchs.Length;
        if ( !length.Equals(saved.switch_configs.Length)){
            length.NewIn(out saved.switch_configs);
            DataSystem.Set(saved);
        }


        for (int i = 0; i < refresh_switchs.Length; i++)
        {
            int c = i;
            refresh_switchs[i].GetButton(Switch.RefreshButton.SWITCH).onClick.AddListener(delegate { SwitchOption(c); });
            SwitchItemStatus(refresh_switchs[i], saved.switch_configs[i]);
        }
    }
    /// <summary>
    /// Save every configuration changed in Game
    /// </summary>
    public void SaveConfigurations()
    {
        AudioSystem.SavedBValues();
        
    }

    /// <summary>
    ///  Change the option of one of the switchOption
    /// </summary>
    public void SwitchOption(int index){
        index.Print();
        RefreshController refresh = refresh_switchs[index];
        SavedData saved = DataSystem.Get;
        bool status = !saved.switch_configs[index];
        saved.switch_configs[index] = status;
        DataSystem.Set(saved);
        SwitchItemStatus(refresh, status);
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


namespace OptionRefresh
{

    namespace Switch
    {
        public enum RefreshButton
        {
            SWITCH=0
        }

        public enum RefreshText
        {
            INFO = 0,
            VALUE=1
        }
    }
}

