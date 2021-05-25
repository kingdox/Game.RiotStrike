#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Change;
using XavHelpTo.Set;
using RefreshControl;
using Environment;
# endregion

public class ControlManager : MonoBehaviour
{
    #region Variable
    private RefreshController[] refresh_controls;
    private bool isBinding=false;
    private int actualBindIndex = -1;
    [Header("Control Manager")]
    public Transform tr_parent_controls;
    public GameObject pref_controlItem;
    [Space]
    public Button btn_back;
    public Button btn_reset;
    #endregion
    #region Event
    private void Start()
    {
        isBinding = false;
        SavedData saved = DataSystem.Get;
        GenerateControlOptions(ref saved);
        
        
    }

    private void OnGUI()
    {
        if (isBinding) BindResponseEvent();
    }
    #endregion
    #region Method
    /// <summary>
    /// Returns the length of the <seealso cref="Data.CONTROLS"/>
    /// </summary>
    private int ControlLength => Environment.Data.CONTROLS.Length;
    private void RefreshButtonInteraction(int i, RefreshButton btn, bool condition) => refresh_controls[i].GetButton(btn).interactable = condition;

    /// <summary>
    /// Generates a list of items in controls
    /// </summary>
    private void GenerateControlOptions(ref SavedData saved)
    {
        refresh_controls = new RefreshController[0];
        int savedLength = saved.controlKeys.Length;
        if (!savedLength.Equals(ControlLength)){
            //Rellenamos los campos vacíos
            for (int i = 0; i < ControlLength; i++){
                if (i >= savedLength) Environment.Data.CONTROLS[i].KEY.PushIn(ref saved.controlKeys);
            }
            DataSystem.Set(saved);
        }
        for (int i = 0; i < ControlLength; i++) CreateControl(Environment.Data.CONTROLS[i], saved.controlKeys[i]);
    }
    /// <summary>
    /// Create a Control and set the values
    /// </summary>
    private void CreateControl(in Control c, string keyControl)
    {
        RefreshController _refresh = RefreshController.CreateRefresh(in pref_controlItem, in tr_parent_controls);
        _refresh.PushIn(ref refresh_controls);
        int index = refresh_controls.Length - 1;

        _refresh.Translate(RefreshText.TITLE, c.NAME);
        _refresh.RefreshText(RefreshText.VALUE, in keyControl);

        _refresh.GetButton(RefreshButton.KEY).onClick.AddListener(delegate { AssignKey(index); });
        _refresh.GetButton(RefreshButton.RESET).onClick.AddListener(delegate { ResetKey(index); });
    }


    /// <summary>
    /// Reset a key
    /// </summary>
    public void ResetKey(int index){
        SavedData saved = DataSystem.Get;
        saved.controlKeys[index] = Environment.Data.CONTROLS[index].KEY;
        DataSystem.Set(saved);
        refresh_controls[index].RefreshText(RefreshText.VALUE, in saved.controlKeys[index]);
    }

    /// <summary>
    /// Reset all the keys
    /// </summary>
    public void ResetAll()
    {
        for (int i = 0; i < ControlLength; i++) ResetKey(i);
    }

    /// <summary>
    /// Disable all the buttons in screen if is binding, else enable all
    /// </summary>
    /// <param name="toBind"></param>
    public void Binding(int toBind= -1)
    {
        for (int i = 0; i < ControlLength; i++)
        {
            RefreshButtonInteraction(i, RefreshButton.KEY, toBind.Equals(-1) );//|| toBind.Equals(i)
            RefreshButtonInteraction(i, RefreshButton.RESET, toBind.Equals(-1));
        }
    }

    /// <summary>
    /// Creates the edition to the new key, disabling buttons
    /// </summary>
    public void AssignKey(int index)
    {
        if (isBinding) return;//🛡

        isBinding = true;
        actualBindIndex = index;

        //Detectar si se esta asignando algo o no
        Binding(index);
        btn_back.interactable = false;
        btn_reset.interactable = false;

        //Coloca cualquier texto para que asignemos algo...
        refresh_controls[index].RefreshText(RefreshText.VALUE, "...");
    }

    /// <summary>
    /// Wait the response to change the binding,
    /// if is correct key then invoke <seealso cref="SetNewKey(KeyCode)"/>
    /// </summary>
    public void BindResponseEvent()
    {
        Event e = Event.current;
        //e.keyCode != KeyCode.None &&
        if (e.isKey || e.isMouse)
        {
            e.ToKeyCode(out KeyCode k);
            //if (e.isMouse) k = $"mouse{Event.current.button}".ToKeyCode();
            //else k = e.keyCode;

            if (k != KeyCode.None) SetNewKey(k);
        }
    }

    /// <summary>
    /// Assign the last <seealso cref="actualBindIndex"/>
    /// </summary>
    public void SetNewKey(KeyCode k)
    {
        if (!isBinding) return;//🛡
        isBinding = false;

        //Guardamos
        SavedData saved = DataSystem.Get;
        saved.controlKeys[actualBindIndex] = k.ToString();
        DataSystem.Set(saved);

        refresh_controls[actualBindIndex]
            .RefreshText(RefreshText.VALUE,
            saved.controlKeys[actualBindIndex]);

        Binding();
        btn_back.interactable = true;
        btn_reset.interactable = true;
        actualBindIndex = -1; //resets the index
    }

    #endregion
}
/// <summary>
/// Structure of the control info
/// </summary>
[Serializable]
public struct Control
{
    public string NAME;
    public string KEY;
    public string DEBUG_DESCRIPTION;
}

namespace RefreshControl
{
    public enum RefreshText
    {
        TITLE = 0,
        VALUE = 1,
        DEFAULT = 2,
    }
    public enum RefreshButton
    {
        KEY = 0,
        RESET = 1
    }
}



