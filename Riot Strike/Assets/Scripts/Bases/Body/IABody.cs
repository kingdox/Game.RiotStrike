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

    [Header("Enemy Body")]
    [HideInInspector] public NavMeshAgent agent;
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
    }
    private void Update(){
        if (!Time.timeScale.Equals(0)) return;
        //state.UpdateState(this);
    }
    #endregion
    #region Methods
    //public  void TransitionToState(State newState){
    //    if (newState != state) return;// 🛡
    //    state = newState;

    //}

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