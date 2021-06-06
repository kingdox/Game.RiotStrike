#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
#endregion

/// <summary>
/// Helper to actions in this game and their own structure
/// </summary>
public static class Utils 
{
#region Utils Methods
    /// <summary>
    /// Allows to translate and set the information
    /// can create a <seealso cref="TranslateController"/> in the obj
    /// <seealso cref="TranslateSystem"/>
    /// </summary>
    public static void Translate<T>(this RefreshController refresh, T r, string key)
    {
        refresh
            .GetText(r)
            .Component(out TranslateController transC, true);
        transC.key = key;
        transC.Translate();
    }

    /// <summary>
    /// Transform the <see cref="EControl"/> to
    /// <seealso cref="KeyCode"/> with the <see cref="SavedData"/> values
    /// </summary>
    public static KeyCode ToKeyCode(this EControl e) => DataSystem.Get.controlKeys[e.ToInt()].ToKeyCode();


    /// <summary>
    /// Check if is pressed or not
    /// </summary>
    public static bool IsPressed(this EControl e) => Input.GetKey(e.ToKeyCode());
    /// <summary>
    /// Check if is holding in the frame
    /// </summary>
    public static bool IsPressedDown(this EControl e) => Input.GetKeyDown(e.ToKeyCode());
    /// <summary>
    /// Check if stop holding in the frame
    /// </summary>
    public static bool IsPressedUp(this EControl e) => Input.GetKeyUp(e.ToKeyCode());
    /// <summary>
    /// Check if is pressed, returning 1 if is true or else 0
    /// </summary>
    public static int Pressed(this EControl e) => e.IsPressed().ToInt();
    /// <summary>
    /// Check if is pressed in the frame
    /// </summary>
    public static int PressedDown(this EControl e) => e.IsPressedDown().ToInt();


    /// <summary>
    /// Returns the status of the axis, where it can be 1,0 or -1
    /// </summary>
    public static int Axis(EControl ePositive, EControl eNegative) => ePositive.Pressed() + -eNegative.Pressed();


    /// <summary>
    /// get the axis type, using the sensibility established and the  own invert axis
    /// </summary>
    public static float Axis(string key, ESwitchOpt invert, float aimSpeed) =>
        Input.GetAxis(key)
        * (DataSystem.Get.sensibilityPercent * aimSpeed)
        * (DataSystem.Get.switch_configs[invert.ToInt()] ? 1 : -1);

    #endregion
}
