#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
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
    private const float TIME_SPAWN = 60*3;
    private Transform[][] matrix_buffPoints;
    private Transform[] buffPoints;
    private Vector2 lastIndex;
    private float timerSpawnCount = 0;
    [Header("Buff Manager")]
    [SerializeField] private Transform tr_parent_buffPoints = null;
    [SerializeField] private float timerSpawn = 60 * 3;
    [SerializeField] private GameObject prefs_buffs; // based on EBuff
    #endregion
    #region Event
    private void Start(){

        LoadMatrixPoints();
        GenerateBuff();
    }
    private void Update()
    {
        if (timerSpawn.TimerIn(ref timerSpawnCount)){
            "Spawning".Print("magenta");
            GenerateBuff();
        }
        
    }
    #endregion
    #region Method

    /// <summary>
    /// Refresh the information in the matrix of the buff points to spawn into it
    /// </summary>
    private void LoadMatrixPoints()
    {
        matrix_buffPoints = new Transform[tr_parent_buffPoints.childCount][];

        tr_parent_buffPoints.Components(out buffPoints);

        for (int i = 0; i < buffPoints.Length; i++) buffPoints[i].Components(out matrix_buffPoints[i]);
    }

    /// <summary>
    ///  Manages the generation of the next buff in scene
    /// </summary>
    public bool GenerateBuff()
    {

        int indexToFind = Get.ZeroMax<EBuff>();//TODO en XavLib

        int indexEmptyChild = buffPoints[indexToFind].IsEmptyAnyChild();

        Transform emptyBuffPoint = BuffPoint(indexToFind, indexEmptyChild);

        //si es distinto asigna el nuevo valor
        if (!emptyBuffPoint.Equals(BuffPoint(lastIndex))){
            "Asignamos el nuevo punto".Print("blue");
            lastIndex.Set(indexToFind, indexEmptyChild);
        }
        else
        {

        }




        /*TODO Elegimos uno de los existentes
         *  vemos si esta limpio, sino pa arriba
         *  vemos que no haya sido el ultimo punto de buff, sino pa arriba
         *  
         * 
         * 
         * 
         */




        return false;
    }

    /// <summary>
    /// Find the Point of the buff based on the <seealso cref="EBuff"/> list and the index into their child options
    /// </summary>
    private Transform BuffPoint(int type, int index) => matrix_buffPoints[type][index];
    private Transform BuffPoint(Vector2 index) => matrix_buffPoints[index[0].ToInt()][index[1].ToInt()];
    


    #endregion
}