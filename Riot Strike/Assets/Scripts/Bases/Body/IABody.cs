#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using XavHelpTo;
using Dat = Environment.Data;
# endregion

/// <summary>
/// Body who manages the Enemy States by itself with their own IA
/// </summary>
public class IABody : Body
{
    #region Variables

    [HideInInspector] public NavMeshAgent agent;
    [Header("Enemy Body")]
    public bool aiActive = true;


    [Header("IA Stat")]
    [Tooltip("Patrulla que hará la IA por defecto")]
    public Transform patrol;
    [Tooltip("Posibilidad de usar habilidad cuando localize al jugador")]
    public float percentCastSpell;
    [Tooltip("Tiempo que le toma al enemigo en disparar con el arma mientras esta apuntando")]
    public float delayAttack;




    #endregion
    #region Events
    public override void Start(){
        this.Component(out agent);


        //TODO temporal
        //agent.SetDestination(patrol.position);
        agent.updatePosition = false;
        agent.updateRotation = false;

        agent.radius = controller.radius;
        //agent.isStopped=true;
    }
    private void Update(){
        if (Time.timeScale.Equals(0)) return;
        
        ManageIA();
        //state.UpdateState(this);
    }
    #endregion
    #region Methods

    /// <summary>
    /// Resolves the IA Management to start to act, if the ai Is  not enabled then itself will do nothing
    /// </summary>
    private void ManageIA(){
        if (!aiActive) return; // 🛡


        Move(patrol.position);
        

    }
    //public  void TransitionToState(State newState){
    //    if (newState != state) return;// 🛡
    //    state = newState;

    //}


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

/*
 
    public State currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public List<Transform> wayPointList;
    public int nextWayPoint;

    public bool aiActive = true;

    public EnemyStats enemyStats;
    public Transform eyes;
    public Transform target;
    public Vector3 lastSpotedPosition;
 
 
 */