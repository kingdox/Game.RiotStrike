#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo.Get;
#endregion
///<summary>
/// ClickShot class
///<summary>
[RequireComponent(typeof(Rigidbody))]
public class ClickShotController : MonoBehaviour {
    #region Variables
    private Rigidbody body;
    public float intensity = 10;
    #endregion
    #region Event
    private void Awake() {
        body = GetComponent<Rigidbody>();
    }
    private void OnMouseDown() {
        body.AddForce(new Vector3(RandomValue, intensity.ZeroMax(), RandomValue), ForceMode.Impulse);

    }
    #endregion
    #region Methods
    /// <summary>
    /// Set a random value
    /// </summary>
    private float RandomValue => (intensity / 2).MinusMax();
    #endregion
}
