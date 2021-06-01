#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo.Get;
using XavHelpTo.Change;
using XavHelpTo.Know;
# endregion

/// <summary>
/// Control the rotation of a element (used most case for the player rotation)
/// </summary>
public class RotationController : MonoBehaviour
{
    #region Variable

    private Vector3 rotation = Vector3.zero;
    private float rotationHorizontal;
    private float rotationVertical = 0f;
    private const float MAX_ANGLE_Y = 90;
    private const string KEY_AXIS_X = "Mouse X";
    private const string KEY_AXIS_Y = "Mouse Y";
    [Header("Rotation Controller")]
    public Transform tr_head;
    public float magnitude;
    #endregion
    #region Event
    private void Update()
    {
        Controls();
        Rotation();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Check and set values in the rotations changes by axis inputs
    /// </summary>
    private void Controls()
    {
        //returns the value of the mouse and gets the rotation
        rotationHorizontal = Axis(KEY_AXIS_X, ESwitchOpt.INVERT_AXIS_X);
        rotationVertical += Axis(KEY_AXIS_Y, ESwitchOpt.INVERT_AXIS_Y);
    }

    /// <summary>
    /// Do the rotation
    /// </summary>
    private void Rotation()
    {
        transform.Rotate(0f, rotationHorizontal, 0f);
        rotationVertical = rotationVertical.Limit(MAX_ANGLE_Y);
        rotation.x = rotationVertical;
        tr_head.localEulerAngles = rotation;

    }


    /// <summary>
    /// get the axis type, using the sensibility established and the  own invert axis
    /// </summary>
    private float Axis(string key, ESwitchOpt invert ) =>
        Input.GetAxis(key)
        * Sensibility
        * Invert(in invert);

    /// <summary>
    /// Check if the invertion was 1 or -1
    /// </summary>
    private int Invert(in ESwitchOpt invert) => (DataSystem.Get.switch_configs[invert.ToInt()] ? 1 : -1);
    /// <summary>
    /// Gets the sensibility of the character based on saved information in <seealso cref="SavedData"/> and <see cref="magnitude"/>
    /// </summary>
    private float Sensibility => DataSystem.Get.sensibilityPercent * magnitude;
    #endregion
}
