#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XavHelpTo;
using XavHelpTo.Know;
using XavHelpTo.Get;
using Dat = Environment.Data;
# endregion

/// <summary>
/// Body who manages the Enemy States by itself with their own IA
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class IABody : Body
{
    #region Variables

    private bool trySpell = true;
    [HideInInspector] public NavMeshAgent agent;
    [Header("Enemy Body")]
    public bool iaActive = true;
    [Space]
    public State currentState;

    public IAStat iaStat;

    //Parent of the Patrols
    public Transform patrol;

    //Target to chase
    public Transform target;

    #endregion
    #region Events
    public override void Start(){
        this.Component(out agent,false);
        InitNavAgent();
    }
    private void Update(){
        if (Time.timeScale.Equals(0)) return;
        ManageIA();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Starts to adjust the NavAgent internally
    /// </summary>
    private void InitNavAgent()
    {
        agent.updateRotation = false;
        agent.updatePosition = false;
        agent.radius = controller.radius;
    }
    /// <summary>
    /// Resolves the IA Management to start to act, if the ai Is  not enabled then itself will do nothing
    /// </summary>
    private void ManageIA(){
        if (!iaActive) return; // 🛡

        currentState.UpdateState(this);
    }
    /// <summary>
    /// Transition between states 
    /// </summary>
    public void TransitionToState(State newState){
        if (!newState || newState.Equals(currentState)) return;// 🛡

        currentState = newState;
    }
    /// <summary>
    /// Move the IA to the asigned destination
    /// </summary>
    public void Move(Vector3 destination) => movement.Move(
        controller,
        agent,
        stat.RealSpeed,
        destination
    );
    /// <summary>
    /// Rotates the IA to the asigned destination
    /// </summary>
    public void Rotate(Vector3 destination) => rotation.Rotate(
        destination,
        stat.RealSpeed
    );
    /// <summary>
    /// Do an Attack with the weapon
    /// also can reload or cast the spell
    /// </summary>
    public void Attack() {
        "Ataca bot".Print("yellow");

        if (character.weapon.IsEmptyAmmo) {
            character.OnReload?.Invoke();
        } else {
            character.OnAttack?.Invoke(this);
        }


        if (trySpell) {
            trySpell = false;

            //Try cast spell
            float percentMax = 100f.ZeroMax();
            if (iaStat.percentCastSpell > percentMax) {
                percentMax.IsOnBounds(iaStat.percentCastSpell);
                character.OnCast.Invoke(this);
            }
        }
    }
    /// <summary>
    /// Destroy the IA character if is death
    /// </summary>
    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
    #endregion
}

/**
 * Qué hara nuestra IA?
 *  - Patrullar
 *  - Detectar Enemigo por verlo en camara
 *  - Perseguir Enemigo ( si ha un enemigo en el campo de visión pero pierdes visión (por raycast de head a head?)
 *  - Apuntar (si hay un enemigo en el campo de vision
 *  - Disparar (si no ha apuntado)
 *  - Recargar (si no puede disparar
 *  - Habilidad (por porcentaje de posibilidad)
 */
