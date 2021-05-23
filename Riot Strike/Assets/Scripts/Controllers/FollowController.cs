#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
# endregion
/// <summary>
/// Script that allows to follow the transform target in scene with a speed in the time,
/// also can rotate as the target say
/// </summary>
public class FollowController : MonoBehaviour
{
    #region Variables
    [Header("FollowController")]

    [SerializeField]private Transform tr_target;
    [Range(0.01f,0.1f)]
    public float speed = 0.05f;

    #endregion
    #region Events
    private void Update() => FollowTarget();
    #endregion
    #region Methods
    /// <summary>
    /// Follow the target moving the object and rotating
    /// </summary>
    private void FollowTarget(){
        if (!TargetExist) return; // 🛡
        Move();
        Rotate();
    }

    /// <summary>
    /// Moves the object to the target coordinates
    /// </summary>
    private void Move(){
        if (IsInPosition) return;
        transform.position = Vector3.Lerp(transform.position, tr_target.position, speed);
    }
    /// <summary>
    /// Rotates the object to the target orientation
    /// </summary>
    private void Rotate(){
        if (IsInRotation) return;
        transform.rotation = Quaternion.Lerp(transform.rotation, tr_target.rotation, speed);
    }

    /// <summary>
    /// Assign a target in the target's field
    /// </summary>
    public void SetTarget(Transform tr) => tr_target = tr;

    /// <summary>
    /// Wether if the target exist
    /// </summary>
    public bool TargetExist => !tr_target.IsNull();
    /// <summary>
    /// If the target is in the same position
    /// </summary>
    public bool IsInPosition => transform.position.Equals(tr_target.position);
    /// <summary>
    /// If the target is in the same rotation
    /// </summary>
    public bool IsInRotation => transform.rotation.Equals(tr_target.rotation);

    #endregion

}
