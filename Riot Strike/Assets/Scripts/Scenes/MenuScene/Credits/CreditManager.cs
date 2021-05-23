#region
using UnityEngine;
using System;
using Environment;
using XavHelpTo;
using XavHelpTo.Change;
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
        GameObject _obj = Instantiate<GameObject>(pref_creditItem, tr_parent_credits);
        CreditController _ctrl_credit;
        _obj.transform.Component(out _ctrl_credit);
        _ctrl_credit.Refresh(name, color);

    }

    #endregion
}

/// <summary>
/// List of achievements
/// </summary>
[Serializable]
public struct CreditList { public Credit[] CREDITS; }
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