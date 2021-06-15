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
using OWO;
#endregion
/// <summary>
/// Movement of a bullet with their own effects when it hits and other
/// behaviours
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RefreshController))]
[DisallowMultipleComponent]
public class Bullet : MonoBehaviour, ITargetImpact
{
    #region Variables
    private RefreshController refresh;
    private Rigidbody body;
    private Vector3 movement;
    private Vector3 initPosition;
    private bool isImpacted=false;
    private const ushort OWO_ID_BULLET = 9;
    [Header("Bullet Base")]
    [HideInInspector] public float damage;
    public float speed;
    [SerializeField] private EBulletBehaviour behaviour = EBulletBehaviour.CONSTANT;
    public Transform tr_target; // actualmente usado solo para la buscqueda, pero puede extenderse a especificaciones de las demás
    [Space]
    public bool effectImpact = true;
    public bool effectMoving = true;
    public float destroyDelay = 5;
    public Action<Body, int> OnImpact;
    public void Start()
    {
        initPosition = transform.position;
        this.Component(out body);
        this.Component(out refresh);

        refresh.GetParticle(Particle.LINE).ActiveParticle(effectMoving);

        ActionBehaviour(true);
    }
    private void FixedUpdate() {
        if (Time.timeScale == 0) return;
        ActionBehaviour();
    }
    private void OnCollisionEnter(Collision collision){
        if (isImpacted) return;// 🛡
        isImpacted = true;
        if (effectImpact) refresh.RefreshPlayParticle(Particle.IMPACT);
        refresh.GetParticle(Particle.LINE).ActiveParticle(false);


        CheckTarget(collision.transform);
        

        DestroyBullet();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Do the action based on the <seealso cref="behaviour"/>
    /// </summary>
    private void ActionBehaviour(bool firstTime = false){
        if (isImpacted) return; // 🛡

        // mejorable....
        switch (behaviour){
            case EBulletBehaviour.IMPULSE:
                if (firstTime){
                    body.useGravity = true;
                    Move();
                }
                return;
            case EBulletBehaviour.CONSTANT:
                if (firstTime) body.useGravity = false;
                Move();
                break;
            case EBulletBehaviour.INSTANT:
                if (firstTime)
                {
                    body.useGravity = false;
                    StartCoroutine(InstantMove());
                }
                break;
            case EBulletBehaviour.FOLLOW:
                if (firstTime) body.useGravity = false;
                Orientate();
                Move();
                break;
        }
    }
    /// <summary>
    /// Moves the bullet forward
    /// </summary>
    private void Move(){
        /* ANtes
        //FIXME Tabare flare problemas
        movement.z = speed; // metters per second
        Vector3 forward = transform.TransformDirection(movement);//transform.forward
        body.velocity = forward * Time .deltaTime ;//(forward * Time.deltaTime).Print("yellow");
        */

        //body.AddRelativeForce(Vector3.forward * 5 * Time.deltaTime);
        body.AddRelativeForce(0,0,10,ForceMode.VelocityChange);
        //si nos pasamos del recorrido limite que peude llegar nuestra bala
        //if (Vector3.Distance(initPosition,transform.position) > speed){
        //    DestroyBullet();
        //}
    }
    /// <summary>
    /// Move instantaneously fetching a target 'til end
    /// </summary>
    IEnumerator InstantMove()
    {
        yield return new WaitForEndOfFrame();
        //where speed represent the max distance
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, speed))
        {
            transform.position = hit.point;
            //CheckTarget(hit.transform);
        }
        else
        {
            transform.position = transform.forward * speed;
        }
            DestroyBullet();
    }
    /// <summary>
    /// Orientate the forward of the bullet to the target position
    /// </summary>
    private void Orientate(){
        if (!tr_target) return;//🛡
        transform.rotation = Quaternion.LookRotation(tr_target.position - transform.position);
        //transform.rotation.SetLookRotation(tr_target.position);
    }
    /// <summary>
    /// Do the comprobation if is a valid target, if is right then do the damage and it shows
    /// the qty of damage
    /// </summary>
    /// <param name="tr_target"></param>
    public void CheckTarget(Transform tr_target)
    {
        //CHECK if is Target
        tr_target.Component(out Body targetBody, false);
        bool isValidTarget = !targetBody.IsNull() && !gameObject.CompareTag(targetBody.tag);
        //DO DAMAGE
        if (isValidTarget)
        {
            OWOCheckPlayer(targetBody);
            //Invoke first, next the calc
            OnImpact?.Invoke(targetBody, damage.ToInt());
            targetBody.AddLife(-damage);
        }
    }


    /// <summary>
    ///  Check if the target is a player and then apply the owo damage
    /// </summary>
    public void OWOCheckPlayer(Body body) {
        body.Component(out PlayerBody player);
        if (!player) return; // 🛡

        OWOMuscles part = (OWOMuscles)Supply.Lenght<OWOMuscles>().ZeroMax();
        OWOSytem.SendSensation(OWO_ID_BULLET, part);
    }
    /// <summary>
    /// Destroys the bullet
    /// </summary>
    private void DestroyBullet() => Destroy(gameObject, destroyDelay);
    #endregion
}
