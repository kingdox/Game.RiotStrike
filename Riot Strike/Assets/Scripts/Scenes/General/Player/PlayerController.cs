#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dat = Environment.Data;
#endregion
/// <summary>
/// Controls the Player and the manages of their movement
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Player Controller")]
    public PlayerCharacter character;

    #endregion
    #region Events
    private void Start()
    {
        Dat.GetStatData(character.statID);
    }
    #endregion
    #region Methods
    #endregion
}
