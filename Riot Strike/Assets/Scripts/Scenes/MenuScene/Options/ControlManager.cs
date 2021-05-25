#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
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

    private void Update()
    {
       
    }
    private void OnGUI()
    {
        if (isBinding)
        {
            Event e = Event.current;
            //e.keyCode != KeyCode.None &&
            if ( e.isKey || e.isMouse )
            {
                isBinding = false;

                KeyCode k;
                if (e.isMouse) k=(KeyCode)Enum.Parse(typeof(KeyCode), $"mouse{Event.current.button}", true);
                else k = e.keyCode;
                Debug.Log(k);

                SavedData saved = DataSystem.Get;
                saved.controlKeys[actualBindIndex] = k.ToString();

                refresh_controls[actualBindIndex].RefreshText(RefreshText.VALUE, k.ToString());

                //Guardamos el nuevo key
                //refrescamos el texto,
                //hacemos unbind

            }
        }

    }
    #endregion
    #region Method
    /// <summary>
    /// Returns the length of the <seealso cref="Data.CONTROLS"/>
    /// </summary>
    private int ControlLength => Data.CONTROLS.Length;
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
                if (i >= savedLength) Data.CONTROLS[i].KEY.PushIn(ref saved.controlKeys);
            }
            DataSystem.Set(saved);
        }
        for (int i = 0; i < ControlLength; i++) CreateControl(Data.CONTROLS[i], saved.controlKeys[i]);
    }
    /// <summary>
    /// Create a Control and set the values
    /// </summary>
    private void CreateControl(in Control c, string keyControl)
    {
        RefreshController _refresh = RefreshController.CreateRefresh(in pref_controlItem, in tr_parent_controls);
        _refresh.PushIn(ref refresh_controls);
        int index = refresh_controls.Length - 1;

        _refresh.RefreshText(RefreshText.TITLE, in c.NAME);
        _refresh.RefreshText(RefreshText.VALUE, in keyControl);
        //_refresh.RefreshText(RefreshText.DEFAULT, in c.KEY);

        _refresh.GetButton(RefreshButton.KEY).onClick.AddListener(delegate { AssignKey(index); });
        _refresh.GetButton(RefreshButton.RESET).onClick.AddListener(delegate { ResetKey(index); });
    }


    /// <summary>
    /// Reset a key
    /// </summary>
    public void ResetKey(int index){
        SavedData saved = DataSystem.Get;
        saved.controlKeys[index] = Data.CONTROLS[index].KEY;
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
            RefreshButtonInteraction(i, RefreshButton.KEY, toBind.Equals(-1) || toBind.Equals(i));
            RefreshButtonInteraction(i, RefreshButton.RESET, toBind.Equals(-1));
        }
    }

    /// <summary>
    /// Assign a new key
    /// </summary>
    public void AssignKey(int index)
    {
        isBinding = !isBinding;


        //Detectar si se esta asignando algo o no
        if (isBinding){
            Binding();
            btn_back.interactable = true;
            btn_reset.interactable = true;
            //Vuelve todo a la normalidad y guarda los cambios
            refresh_controls[index].RefreshText(RefreshText.VALUE, DataSystem.Get.controlKeys[index]);
        }
        else
        {

            Binding(index);
            btn_back.interactable = false;
            btn_reset.interactable = false;

            refresh_controls[index].RefreshText(RefreshText.VALUE, "...");
            //permite alterar la tecla escogida
            //Hacemos que la persona esté asignando la siguiente tecla

        }

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



