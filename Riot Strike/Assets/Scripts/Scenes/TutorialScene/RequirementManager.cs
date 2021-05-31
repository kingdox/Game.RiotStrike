#region Access
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Dat = Environment.Data;
# endregion

namespace TutorialScene
{
    /// <summary>
    /// manager of every requirement
    /// </summary>
    public class RequirementManager : MonoBehaviour
    {
        #region Variables
        [Tooltip("Requirements to check")]
        public RequirementCheck[] requirements;

        #endregion
        #region Events
        private void Start()
        {
            
        }
        private void Update()
        {
            
        }
        #endregion
        #region Method

        #endregion
    }
}

/*
 * TODO
 * 
 * Crear un recorrido que corrija cada Requirement,
 * 
 * Donde los requirement poseen:
 *  - Un arreglo de Inputs (enums), donde checkean si todos fueron tocados
 *  - Un arreglo de elementos a los cuales el jugador debe de hacer contacto con ellos, y en cuantos segundos, checkea si fueron tocados 
 *  -
 */

/// <summary>
/// Checker of inputs, contacts
/// </summary>
[Serializable]
public struct RequirementCheck
{
    #region Variables
    public string debugName;
    [Space(5)]
    [Header("Input check")]
    public EControl[] controlsToCheck;
    [Space(5)]
    [Header("Contact Check")]
    public string tagToCheck;
    public float timeToWait;
    public GameObject[] objectsToCheck;
    private float count;
    [Space(20)]
    [Header("Objects to Resolve")]
    public GameObject[] objectToEnable;
    public GameObject[] objectToDisable;
    #endregion
}