#region
using UnityEngine;
using System;
using Environment;
using XavHelpTo;
using XavHelpTo.Change;
using CreditRefresh;
#endregion
public class CreditManager : MonoBehaviour
{
    #region Variable
    [Header("Achievement Manager")]
    public Transform tr_parent_credits;
    public GameObject pref_creditItem;

    #endregion
    #region Event
    private void Start()
    {
        foreach (var c in Data.CREDITS) CreateCreditList(in c);
    }
    #endregion
    #region Method

    /// <summary>
    /// Generates the list of a creditList, with title and their names
    /// </summary>
    /// <param name="c"></param>
    private void CreateCreditList(in Credit c){
        //1. creamos el titulo
        CreateCredit(c.TEXT, c.BACKGROUND.ToColor());
        //2. creamos un for de los nombres
        foreach (string n in c.NAMES) CreateCredit(n, Color.black);
    }

    /// <summary>
    /// Create a credit
    /// </summary>
    private void CreateCredit(string name, Color color)
    {
        RefreshController _refresh = RefreshController.CreateRefresh(in pref_creditItem, in tr_parent_credits);

        _refresh.RefreshText(RefreshText.NAME, name);
        _refresh.RefreshImgColor(RefreshImage.BACKGROUND, color);
    }

    #endregion
}
/// <summary>
/// Structure of the achievements
/// </summary>
[Serializable]
public struct Credit
{
    public float[] BACKGROUND;//[float,float,float]
    public string TEXT;
    public string[] NAMES;
}


namespace CreditRefresh
{
    public enum RefreshImage
    {
        BACKGROUND = 0
    }
    public enum RefreshText
    {
        NAME = 0,
    }
}