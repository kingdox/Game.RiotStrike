#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
using XavHelpTo.Set;
using XavHelpTo.Get;
# endregion
/// <summary>
/// Manager to set in game the information of the prefabs Buffs
/// </summary>
public class BuffManager : MonoBehaviour
{

    #region Start
    [Tooltip("matrix donde asignamos toda la información en cada punto de manera ordenada")]
    private Transform[][] jagged_buffs = default; 
    [SerializeField] private Transform tr_parent_buffs;

    #endregion
    #region Event
    private void Start(){
        LoadTheInformation();

    }
    #endregion
    #region Method


    /// <summary>
    /// 
    /// </summary>
    public void LoadTheInformation(){
        "Cargando información".Print("green");
        foreach (Transform[] tr_buff in jagged_buffs){
            //console.log/()
            //Map(tr_buff);

        }
    }

    /// <summary>
    /// Maps the ui
    /// </summary>
    public void Map(Transform toMap)
    {
        //TODO en map te permitira revisar cadad uno por e
    }

    /// <summary>
    /// 
    /// </summary>
    public void Generate()
    {

    }
    #endregion
}