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
    private float rotationVertical = 0f;
    private const float MAX_ANGLE_Y = 90;
    [Header("Rotation Controller")]
    public Transform tr_head;
    #endregion
    #region Methods
    /// <summary>
    /// Do the rotation
    /// </summary>
    public void Rotate(float horizontal, float vertical)
    {
        //Body
        this.transform.Rotate(0f, horizontal, 0f);
        //Head
        rotationVertical += vertical;
        rotationVertical = rotationVertical.Limit(MAX_ANGLE_Y);
        rotation.x = rotationVertical;
        tr_head.localEulerAngles = rotation;
    }
    #endregion
}
