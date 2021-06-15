#region Access
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Change;
using XavHelpTo.Know;
using XavHelpTo.Set;
using Dat = Environment.Data;
using Switch = OptionRefresh.Switch;
#endregion
/// <summary>
/// Manages every option to configure for player
/// in <see cref="Environment.Scenes.MENU_SCENE"/>
/// </summary>
public class OptionManager : MonoBehaviour
{
    #region Variable
    private static OptionManager _;

    private const string ACTIVE = "active";
    private const string DISABLE = "disabled";
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

    [Header("Languages")]
    public int index_lang = 0;


    [Header("Post Processing")]
    public GameObject obj_postProcessing;

    #endregion
    #region Event
    private void Awake() => this.Singleton(ref _, false);
    private void Start()
    {
        btn_back.onClick.AddListener(SaveConfigurations);
        LoadConfigurations();
    }
    #endregion
    #region Method


    /// <summary>
    /// Change the language to the next idiom
    /// </summary>
    [ContextMenu("Cambiar Idioma")]
    public void ChangeLang()
    {
        index_lang = true.NextIndex(Dat.LANGUAGES.Length, index_lang);
        TranslateSystem.InitLang(Dat.LANGUAGES[index_lang]);

    }

    /// <summary>
    /// Change the information showed based on a condition
    /// </summary>
    private void SwitchItemStatus(RefreshController refresh, bool condition)
    {
        refresh.GetText(Switch.RefreshText.VALUE).transform.Component(out TranslateController transC);
        transC.key = condition ? ACTIVE : DISABLE;
        transC.Translate();
    }

    /// <summary>
    /// Loads the saved data to set all the saved configurations in game
    /// </summary>
    public void LoadConfigurations()
    {
        SavedData saved = DataSystem.Get;

        slider_music.value = saved.musicPercent;
        slider_sound.value = saved.soundPercent;
        slider_sensibility.value = saved.sensibilityPercent;
        index_lang = Know.IndexOf(Dat.LANGUAGES, 0, saved.currentLang).Min(0).Max(Dat.LANGUAGES.Length);



        // SI hay variacion entre los switch actuales y los guardados
        int length = refresh_switchs.Length;
        if (!length.Equals(saved.switch_configs.Length))
        {
            length.NewIn(out saved.switch_configs);
            DataSystem.Set(saved);
            DataSystem.Save();
        }

        // vemos si hay post procesado o no, por defecto no
        obj_postProcessing.SetActive(saved.switch_configs[ESwitchOpt.POST_PROCESSING.ToInt()]);

        for (int i = 0; i < refresh_switchs.Length; i++)
        {
            int c = i;
            refresh_switchs[i].GetButton(Switch.RefreshButton.SWITCH).onClick.AddListener(delegate { SwitchOption(c); });
            SwitchItemStatus(refresh_switchs[i], saved.switch_configs[i]);
        }

        refresh_switchs[ESwitchOpt.POST_PROCESSING.ToInt()]
            .GetButton(Switch.RefreshButton.SWITCH)
            .onClick.AddListener(SetPostProcessing);
    }

    /// <summary>
    /// Sets the status visual of postprocessing
    /// </summary>
    public void SetPostProcessing()
    {
        obj_postProcessing
            .SetActive(!obj_postProcessing.activeInHierarchy);
    }
    /// <summary>
    /// Save every configuration changed in Game
    /// </summary>
    public void SaveConfigurations()
    {
        AudioSystem.SavedBValues();

        SavedData saved = DataSystem.Get;
        saved.currentLang = Dat.LANGUAGES[index_lang];

        DataSystem.Set(saved);
        DataSystem.Save();
    }

    /// <summary>
    ///  Change the option of one of the switchOption
    /// </summary>
    public void SwitchOption(int index)
    {
        //index.Print();
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
        DataSystem.Set(saved);
    }
    #endregion
}




