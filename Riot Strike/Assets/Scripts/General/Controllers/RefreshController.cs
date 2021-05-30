#region Access
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo.Change;
using XavHelpTo;
#endregion
/// <summary>
/// Controls the information an refresh it if is required
/// </summary>
public class RefreshController : MonoBehaviour
{
    #region Variable
    [Header("RefreshController")]
    [SerializeField] private Image[] imgs = new Image[0];
    [SerializeField] private Text[] txts = new Text[0];
    [SerializeField] private Button[] btns = new Button[0];

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


    public Button GetButton<T>(T index) => btns[index.ToInt()];
    /// <summary>
    /// Gets the image
    /// </summary>
    public Image GetImg<T>(T index) => imgs[index.ToInt()];
    /// <summary>
    /// Sets the new image
    /// </summary>
    public void SetImg<T>(T index, Image img) => imgs[index.ToInt()] =img;
    /// <summary>
    /// Gets the text info
    /// </summary>
    public Text GetText<T>(T index) => txts[index.ToInt()];
    /// <summary>
    /// Sets the new text info
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


