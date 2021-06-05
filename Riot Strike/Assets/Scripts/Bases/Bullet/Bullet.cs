#region Variables
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Change;
using XavHelpTo.Set;
using XavHelpTo.Know;
using WeaponRefresh.Ranged;
#endregion
/// <summary>
/// Movement of a bullet with their own effects when it hits and other
/// behaviours
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RefreshController))]
[DisallowMultipleComponent]
public class Bullet : MonoBehaviour
{
    #region Variables
    private RefreshController refresh;
    private Rigidbody body;
    private Vector3 movement;
    private bool isImpacted=false;
    [Header("Bullet Base")]
    [HideInInspector] public float damage;
    public float speed;
    [Space]
    public bool constantSpeed = false;
    [Space]
    public bool effectImpact = true;
    public bool effectMoving = true;
    //public bool SetSplatter = false; //TODO

    public Action<bool> OnImpact;
    public void Start()
    {
        this.Component(out body);
        this.Component(out refresh);

        refresh.GetParticle(Particle.LINE).ActiveParticle(effectMoving);
        Move(true);
    }
    private void Update() =>Move();
    private void OnCollisionEnter(Collision collision){
        if (isImpacted) return;// 🛡
        isImpacted = true;
        if (effectImpact) refresh.RefreshPlayParticle(Particle.IMPACT);
        refresh.GetParticle(Particle.LINE).ActiveParticle(false);

        //CHECK if is Target
        collision.transform.Component(out Body targetBody, false);
        bool isValidTarget = !targetBody.IsNull() && !gameObject.CompareTag(targetBody.tag);
        OnImpact?.Invoke(isValidTarget);

        //DO DAMAGE
        if (isValidTarget)
        {
            targetBody.AddLife(-damage);
        }

        Destroy(gameObject, 10);
    }
    #endregion
    #region Methods
    /// <summary>
    /// Check if the bullet can move forward if is enable the <seealso cref="constantSpeed"/> in true
    /// </summary>
    private bool CanMove => !isImpacted && constantSpeed;
    /// <summary>
    /// Moves the bullet forward
    /// </summary>
    private void Move(bool ignoreCheck=false){
        if (!ignoreCheck && !CanMove) return;
        movement.z = speed * Time.deltaTime; // metters per second
        Vector3 forward = transform.TransformDirection(movement);
        body.velocity = forward;
    }
    #endregion
}
