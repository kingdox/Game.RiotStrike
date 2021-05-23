#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
# endregion
/// <summary>
/// Focuses on do the management of the positions to follow the camera
/// </summary>
public class CameraComponent : MonoBehaviour
{
    #region Variable
    private int lastIndex = -1;
    private Transform[] cameraPoints;
    [Header("Camera Component")]
    public FollowController ctrl_follow;
    public Transform parentCameraPoints;

    #endregion
    #region Events

    private void Start()
    {
        parentCameraPoints.Components(out cameraPoints);
    }
    #endregion
    #region Methods

    /// <summary>
    /// Insert the target into the follow controller to track the camera with it.
    /// </summary>
    /// <param name="index"></param>
    public void SetCameraPoint(int index = -1){
        if (index.Equals(-1)) return; // 🛡

        ctrl_follow.SetTarget(cameraPoints[index]);

        lastIndex = index;
    }

    #endregion
}
