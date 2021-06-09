#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OWO;
#endregion
///<summary>
/// OWOController class
///<summary>
public class OWOController : MonoBehaviour
{
    #region Variables
    public OWOMuscles muscle;
    [SerializeField] private ushort defaultId=0;
    #endregion
    #region Event
    #endregion
    #region Methods
    /// <summary>
    /// Send the sensation
    /// </summary>
    public void Send(ushort id = default) {
        if (id == default) id = defaultId;
        OWOSytem.SendSensation(id, muscle);
    }
    #endregion
}
