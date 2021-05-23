﻿#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Set;
using UnityEngine.InputSystem;
using RefreshControl;
using Environment;
# endregion

public class ControlManager : MonoBehaviour
{
    #region Variable
    public RefreshController[] refresh_controls;
    [Header("Control Manager")]
    public Transform tr_parent_controls;
    public GameObject pref_controlItem;
    #endregion
    #region Event
    private void Start()
    {
        SavedData saved = DataSystem.Get;
        GenerateControlOptions(ref saved);
    }
    #endregion
    #region Method

    private int ControlLength => Data.CONTROLS.Length;

    /// <summary>
    /// Generates a list of items in controls
    /// </summary>
    private void GenerateControlOptions(ref SavedData saved)
    {
        refresh_controls = new RefreshController[0];
        int savedLength = saved.controlKeys.Length;
        if (!savedLength.Equals(ControlLength))
        {
            //Rellenamos los campos vacíos
            for (int i = 0; i < ControlLength; i++)
            {

                if (i >= savedLength)
                {
                    Data.CONTROLS[i].KEY.PushIn(ref saved.controlKeys);
                }
                else
                {
                    $"{i}, {savedLength}".Print("red");
                }

            }
            DataSystem.Set(saved);
            DataSystem.Save();
            $"{nameof(OptionManager)} => Asignado nueva dimension a los controles".Print("blue");
        }

        for (int i = 0; i < ControlLength; i++)
        {
            CreateControl(Data.CONTROLS[i], saved.controlKeys[i]);
        }


    }

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
    /// Assign a new key
    /// </summary>
    public void AssignKey(int index)
    {
        $"Funcionando Test {index}".Print();
    }


    /// <summary>
    /// Reset a key
    /// </summary>
    public void ResetKey(int index)
    {
        $"Reset! {index}".Print();
        SavedData saved = DataSystem.Get;
        saved.controlKeys[index] = Data.CONTROLS[index].KEY;
        refresh_controls[index].RefreshText(RefreshText.VALUE, in saved.controlKeys[index]);
    }

    /// <summary>
    /// Reset all the keys
    /// </summary>
    public void ResetAll()
    {
        for (int i = 0; i < ControlLength; i++) ResetKey(i);
    }
    #endregion
}

/// <summary>
/// List of achievements
/// </summary>
[Serializable]
public struct ControlList { public Control[] CONTROLS; }
/// <summary>
/// Structure of the achievements
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