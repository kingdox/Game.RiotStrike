#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion

/// <summary>
/// Management of the body who player can `play`
/// </summary>
[RequireComponent(typeof(MovementController), typeof(RotationController))]
public class PlayerBody : BodyBase
{
    #region Variable

    private MovementController movement;
    private RotationController rotation;
    [Header("Player Body")]
    public bool canMove = true;
    public bool canRotate = true;

    #endregion
    #region Event
    private void Start()
    {
        this.Component(out movement);
        this.Component(out rotation);
    }
    private void Update()
    {

        Control();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Controls the actions of the player
    /// </summary>
    private void Control()
    {
        //MOVEMENT
        movement.enabled = canMove;

        //ROTATION
        rotation.enabled = canRotate;

        //ATTACK
        if (EControl.ATTACK.IsPressed()) Act(OnAttack);

        //FOCUS
        if (EControl.FOCUS.IsPressed()) Act(OnFocus);

        //RELOAD
        if (EControl.RELOAD.IsPressed()) Act(OnReload);

        //SPELL
        if (EControl.SPELL.IsPressed()) Act(OnSpell);

        //CHAT
        // TODO (Multiplayer) 

        //PAUSE
        // TODO
    }


    /// <summary>
    /// Starts the Invoke of an <see cref="Action"/>
    /// </summary>
    private void Act(Action a) => a?.Invoke();
    #endregion

}
