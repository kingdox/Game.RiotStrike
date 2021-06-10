#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
///<summary>
/// ContactComponent class
///<summary>
public class ContactComponent : MonoBehaviour
{
    #region Variables
    private bool isAlreadyContact = false;
    private Collider col;
        [Header("Contact Component")]
    public Transform transformToCheck;
    public Action OnContact;
    #endregion
    #region Event
    private void Start() {
        this.Component(out col);
    }
    private void OnCollisionEnter(Collision collision) {
        Contact(collision.transform,false);
    }
    private void OnTriggerEnter(Collider other) {
        Contact(other.transform,true);
    }
    #endregion
    #region Methods

    /// <summary>
    /// Manages the contact
    /// </summary>
    private void Contact(Transform tr, bool isTrigger) {
        if (isAlreadyContact 
            ||  !isTrigger.Equals(col.isTrigger) 
            ||  !tr.Equals(transformToCheck)) return; // 🛡
       // $"{isTrigger.Equals(col.isTrigger)}. {tr.Equals(transformToCheck)}, {tr.name}, {transformToCheck.name} ".Print();
        isAlreadyContact = true;
        OnContact?.Invoke();
    }
    #endregion
}
