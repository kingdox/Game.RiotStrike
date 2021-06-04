#region Access
using System;
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
    [Header("Spell")]
    public string ID;
    [HideInInspector] public float timer;
    public bool flagReady;

    public Action<float, float> OnTimer;
    public Action OnFireCast;
    #endregion
    #region Event
    private void Start()
    {
        spellData = Dat.GetSpellData(ID);
        timer = spellData.COOLDOWN;
    }
    private void Update()
    {
        //Timer flag
        spellData.COOLDOWN.TimerFlag(ref flagReady, ref timer);
        OnTimer?.Invoke(timer, spellData.COOLDOWN);
    }
    #endregion
    #region Method
    /// <summary>
    /// Do the spell wether is the spell ready
    /// in case to be called and it's succedd then se the flag in valse 
    /// </summary>
    protected bool CanCast()
    {
        bool result = flagReady;
        if (flagReady)
        {
            OnFireCast?.Invoke();
            flagReady = false;
        }
        return result;
    }

    /// <summary>
    /// Do the cast of the spell
    /// </summary>
    public abstract void Cast();
    #endregion
}
