#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// ClickShot class
///<summary>
public class ClickShotController : MonoBehaviour
{
    #region Variables
    private Rigidbody body;
    #endregion
    #region Event
    private void Awake() {
        body = GetComponent<Rigidbody>();    
    }
    private void OnMouseDown() {
        print("Click");
        body.AddForce( new Vector3(0,5,0), ForceMode.Impulse);
    }
    #endregion
    #region Methods
    #endregion
}
