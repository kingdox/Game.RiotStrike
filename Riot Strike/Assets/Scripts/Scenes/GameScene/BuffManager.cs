#region Access
using System;
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
    private Transform[][] matrix_buffPoints;
    private Transform[] buffPoints;
    private Vector2 lastIndex;
    private float timerSpawnCount = 0;
    private int buffCounts = 0;
    private int totalBuffPoints=0;
    private float timerSpawn = 60 * 3;
    [Header("Buff Manager")]
    [SerializeField] private Transform tr_parent_buffPoints = null;
    [SerializeField] private GameObject[] prefs_buffs = null; // based on EBuff
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
        totalBuffPoints = 0;
        for (int i = 0; i < buffPoints.Length; i++)
        {
            buffPoints[i].Components(out matrix_buffPoints[i]);

            totalBuffPoints += buffPoints[i].childCount;
        }
    }

    /// <summary>
    ///  Manages the generation of the next buff in scene
    ///  TODO refactor returns
    /// </summary>
    [ContextMenu("Generate Buff")]
    public bool GenerateBuff()
    {
        bool keepFinding = true;
    findBuff:
        int indexToFind = Get.ZeroMax<EBuff>(lastIndex[0].ToInt());
        if (indexToFind.Equals(-1)) $"Problemas con el enum de BUffs ó exceso de ignoraciones de indice?".Print("red");

        int indexEmptyChild = buffPoints[indexToFind].IsEmptyAnyChild();

        //checkear de alguna manera si el maximo de buffs ha sido instanciado para anular los siguientes
        if (buffCounts.Equals(totalBuffPoints)){
            $"Warn: Se ha alcanzado el limite de buffs en juego, estan lleno todas".Print("yellow");
            return false;
        }

        //si no encontró sitio vacío y NO es el anterior buscamos en otra parte de la matriz
        if (indexEmptyChild.Equals(-1))
        {
            if (!keepFinding) return keepFinding;
            keepFinding = false;
            goto findBuff;
        }

        Transform emptyBuffPoint = BuffPoint(indexToFind, indexEmptyChild);

        //si es distinto asigna el nuevo valor
        if (!emptyBuffPoint.Equals(BuffPoint(lastIndex))){
            //$"Asignamos el nuevo punto {(EBuff)indexToFind}".Print("blue");
            lastIndex.Set(indexToFind, indexEmptyChild);

            Instantiate(prefs_buffs[indexToFind], emptyBuffPoint.position, Quaternion.identity, emptyBuffPoint)
                .transform
                .Component(out Buff newBuff);

            //añadimos
            buffCounts++;
            //si se destruye lo quitamos
            newBuff.OnDestroyed += delegate { buffCounts--; };
            
        }

        return true;
    }
    /// <summary>
    /// Find the Point of the buff based on the <seealso cref="EBuff"/> list and the index into their child options
    /// </summary>
    private Transform BuffPoint(int type, int index) => matrix_buffPoints[type][index];
    private Transform BuffPoint(Vector2 index) => matrix_buffPoints[index[0].ToInt()][index[1].ToInt()];
    


    #endregion
}