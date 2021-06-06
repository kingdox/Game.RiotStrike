#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using XavHelpTo.Know;
# endregion
/// <summary>
/// Physical Weapon, with near attacks behaviour
/// </summary> 
public class NearWeapon : Weapon, ITargetImpact
{
    #region Variables
    private const string KEY_ATTACK = "Attack";
    private int lastDamage;
    private bool isImpactingTarget;
    [Header("Near Weapom")]
    public Collider collide;
    public Animator animator;
    public Action<Body, int> OnImpact;
    #endregion
    private void Awake()
    {
        this.Component(out collide);
        collide.enabled = false;
        isImpactingTarget = false;
    }
    protected override void Update()
    {
        base.Update();
        if (currentAmmo.Equals(0)) Reload();
    }
    private void OnTriggerEnter(Collider other){
        if (isImpactingTarget) return;
        CheckTarget(other.transform);
    }
    #region Methods
    /// <summary>
    /// Shot the weapon with the bullet prefab
    /// </summary>
    public override void Attack(Body body)
    {
        if (!CanAtack()) return; // 🛡
        base.Attack(body);
        lastDamage = body.stat.STRENGHT;

        //Suscribes
        OnImpact += EmitTargetImpactWeapon;


        collide.enabled = true;
        //Do the animation
        animator.SetTrigger(KEY_ATTACK);

        //Exe the collision default timer
        StartCoroutine(CollisionOpenUntil(body));
    }
    /// <summary>
    /// Check if the target is a <seealso cref="Body"/>
    /// and then resolves the damage
    /// </summary>
    public void CheckTarget(Transform tr_target)
    {
        //CHECK if is Target
        tr_target.Component(out Body targetBody, false);
        bool isValidTarget = !targetBody.IsNull()
            && !gameObject.CompareTag(targetBody.tag);
        //DO DAMAGE
        if (isValidTarget)
        {
            collide.enabled = false;
            isImpactingTarget = true;

            OnImpact?.Invoke(targetBody, lastDamage);//targetBody.Life > lastDamage ? lastDamage : targetBody.Life
            targetBody.AddLife(-lastDamage);

            OnImpact -= EmitTargetImpactWeapon;

        }


    }

    /// <summary>
    /// Enables the collision to do attacks until the indicadors say to end
    /// used to return the default value if the weapon cannot find a target
    /// </summary>
    IEnumerator CollisionOpenUntil(Body body)
    {
        //waits until the player can attack again
        yield return new WaitUntil(body.character.weapon.CanAtack);
        collide.enabled = false;
        isImpactingTarget = false;
        OnImpact -= EmitTargetImpactWeapon;
    }
    #endregion
}
