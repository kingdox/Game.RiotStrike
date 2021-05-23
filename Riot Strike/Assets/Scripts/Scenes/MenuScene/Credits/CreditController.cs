#region Access
using UnityEngine;
using UnityEngine.UI;
# endregion
/// <summary>
/// Controls the information t¡in the Credit items
/// </summary>
public class CreditController : MonoBehaviour
{
    #region Variable
    public Text txt_name;
    public Image img_background;
    #endregion
    #region Events
    #endregion
    #region Methods
    /// <summary>
    /// Refresh the information of the credit
    /// </summary>
    public void Refresh(string name, Color colorBG = default){
        img_background.color = colorBG;
        txt_name.text = name;
    }
    #endregion
}