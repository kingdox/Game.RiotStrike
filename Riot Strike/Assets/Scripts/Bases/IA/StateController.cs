#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#endregion
///<summary>
/// StateController
///</summary>
public class StateController : MonoBehaviour
{
    #region Variables
    [Header("State Controller")]

    public State currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public List<Transform> wayPointList;
    public int nextWayPoint;

    public bool aiActive = true;

    public EnemyStats enemyStats;
    public Transform eyes;
    public Transform target;
    public Vector3 lastSpotedPosition;

    #endregion
    #region Event
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();


    }
    private void Update() {
        if (!aiActive) return;
       // if (!currentState) return;

        currentState.UpdateState(this);


    }
    #endregion
    #region Methods

    public void TransitionToState(State nextState)
    {

        if (nextState != currentState)
        {
            currentState = nextState;
        }
    }


    #endregion
}
