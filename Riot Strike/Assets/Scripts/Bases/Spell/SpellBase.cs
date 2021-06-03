#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Know;
using Dat = Environment.Data;
# endregion
/// <summary>
/// Get the info of the general spells
/// </summary>
public abstract class Spell : MonoBehaviour
{
    #region Variables
    private SpellData spellData;
    //[HideInInspector] public float cooldown;
    [HideInInspector] public float timer;
    [Header("Spell")]
    public string ID;

    #endregion
    #region Event
    private void Start()
    {
        spellData = Dat.GetSpellData(ID);
        timer = spellData.COOLDOWN;

    }
    #endregion
    #region Method

    /// <summary>
    /// Do the cast of the spell
    /// </summary>
    public abstract void Cast();

    /// <summary>
    /// Do the spell wether is the spell ready
    /// </summary>
    public bool CanCast => spellData.COOLDOWN.Timer(ref timer);
    #endregion
}
