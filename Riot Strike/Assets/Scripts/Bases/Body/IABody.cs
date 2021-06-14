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
    /*
     * TODO Hacer un patrón de estados para el manejo de comportamientos
     *  - Esta debe poder ser que llame al padre para manejo de los invoke___
     *  
     *  
     *  TODO TODO COLOCAR LAS STATS de IA AQUI
     */

    [Header("Enemy Body")]
    [HideInInspector] public NavMeshAgent agent;
    public bool aiActive = true; // ya tenemos body set active? //TODO
    //public State state;


    [Header("IA Stat")]
    public Transform patrol;
    public float percentCastSpell; // cuanto es probable que use la habilidad cuando localize al jugador? (spell 1 sola vez)
    public float delayAttack; // cuanto tiempo dura para atacar al enemigo

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