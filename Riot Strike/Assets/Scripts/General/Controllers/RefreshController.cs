#region Access
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo.Change;
using XavHelpTo;
using XavHelpTo.Set;
using XavHelpTo.Get;
#endregion
/// <summary>
/// Controls the information an refresh it if is required
/// Mostly Ui like <seealso cref="Image"/>, <seealso cref="Button"/> or <seealso cref="Text"/> elements but can also look for <seealso cref="ParticleSystem"/>
/// </summary>
public class RefreshController : MonoBehaviour
{
    #region Variable
    [Header("RefreshController")]
    [SerializeField] private Image[] imgs = new Image[0];
    [SerializeField] private Text[] txts = new Text[0];
    [SerializeField] private Button[] btns = new Button[0];
    [SerializeField] private InputField[] inputs = new InputField[0];
    [SerializeField] private ParticleSystem[] parts = new ParticleSystem[0];


    #endregion
    #region Event

    #endregion
    #region Methods

    /// <summary>
    /// Changes the color of a image
    /// </summary>
    public void RefreshImgColor<T>(T index, in Color color) => imgs[index.ToInt()].color = color;
    /// <summary>
    /// Changes the sprite of the image
    /// </summary>
    public void RefreshImgSprite<T>(T index, in Sprite sprite) => imgs[index.ToInt()].sprite = sprite;
    /// <summary>
    /// Changes the information of the text
    /// </summary>
    public void RefreshText<T>(T index, in string value) => txts[index.ToInt()].text = value;
    /// <summary>
    /// Sets the interaction of the button
    /// </summary>
    public void RefreshButtonInteraction<T>(T index, bool condition) => btns[index.ToInt()].interactable = condition;


    /// <summary>
    /// Plays the particle if it can
    /// </summary>
    public void RefreshPlayParticle<T>(T index, bool play=true) => parts[index.ToInt()].ActiveParticle(play);


    /// <summary>
    /// Get the <seealso cref="InputField"/>
    /// </summary>
    public InputField GetInput<T>(T index) => inputs[index.ToInt()];
    /// Get the <seealso cref="ParticleSystem"/>
    /// </summary>
    public ParticleSystem GetParticle<T>(T index) => parts[index.ToInt()];
    /// <summary>
    /// Get the <seealso cref="Button"/>
    /// </summary>
    public Button GetButton<T>(T index) => btns[index.ToInt()];
    /// <summary>
    /// Gets the <seealso cref="Image"/>
    /// </summary>
    public Image GetImg<T>(T index) => imgs[index.ToInt()];
    /// <summary>
    /// Sets the new <seealso cref="Image"/>
    /// </summary>
    public void SetImg<T>(T index, Image img) => imgs[index.ToInt()] =img;
    /// <summary>
    /// Gets the <seealso cref="Text"/> info
    /// </summary>
    public Text GetText<T>(T index) => txts[index.ToInt()];
    /// <summary>
    /// Sets the new <seealso cref="Text"/> info
    /// </summary>
    public void SetText<T>(T index, Text txt) => txts[index.ToInt()] = txt;



    /// <summary>
    /// Create a object and returns the reference of the <seealso cref="RefreshController"/>
    /// </summary>
    public static RefreshController CreateRefresh(in GameObject obj, in Transform parent)
    {
        GameObject _obj = Instantiate<GameObject>(obj, parent);
        RefreshController refresh;
        _obj.transform.Component(out refresh);

        return refresh;
    }
    #endregion
}


