#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
///<summary>
/// AchieveContact class
///<summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class AchieveContact : MonoBehaviour
{
    #region Variables
    private bool isContacted = false;
    [Header("Acheive Contact")]
    public string key;
    public string tagToDetect;
    public bool contactOnce = false;
    public bool saveOnContact = false;
    #endregion
    #region Event
    private void OnCollisionEnter(Collision collision) {

        CheckCollidder(collision.collider);
    }
    private void OnTriggerEnter(Collider other) {
        CheckCollidder(other);
    }
    #endregion
    #region Methods
    /// <summary>
    /// Check the collider and then resolves it if is a weapon
    /// </summary>
    private void CheckCollidder(Collider collide) {
        if (tagToDetect.Equals(collide.tag)) {
            collide.Component(out NearWeapon nearWeapon, false);
            collide.Component(out Bullet bullet, false);
            if (!( bullet || nearWeapon )) return;
            if (contactOnce && isContacted) return;
            isContacted = true;

            $"Contacted ! with {name} to {key}".Print("green");
            //TODO
            //Achievement set
            SavedData saved = DataSystem.Get;
            saved.achievementsPoints[key.IndexAchievement()]++;
            DataSystem.Set(saved);
            if (saveOnContact) DataSystem.Save();
        }
    }
    #endregion
}
