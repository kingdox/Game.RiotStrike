#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using XavHelpTo.Set;
using XavHelpTo.Get;
#endregion

/// <summary>
/// Management of the body who player can `play`
/// </summary>
[RequireComponent(typeof(MovementController), typeof(RotationController), typeof(GravityController))]
public class PlayerBody : BodyBase
{
    #region Variable

    private MovementController movement;
    private RotationController rotation;
    private const string KEY_AXIS_X = "Mouse X";
    private const string KEY_AXIS_Y = "Mouse Y";
    [Header("Player Body")]
    public bool canMove = true;
    public bool canRotate = true;

    #endregion
    #region Event
    public override void Awake() {
        base.Awake();
    }
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
        if (canMove) movement.Move(stat.SPEED,
            Utils.Axis(EControl.RIGHT, EControl.LEFT),
            0f,
            Utils.Axis(EControl.FORWARD, EControl.BACK)
        );

        //ROTATION
        if (canRotate) rotation.Rotate(
            Utils.Axis(KEY_AXIS_X, ESwitchOpt.INVERT_AXIS_X, stat.SPEED),
            Utils.Axis(KEY_AXIS_Y, ESwitchOpt.INVERT_AXIS_Y, stat.SPEED)
        );

        //ATTACK
        CheckPress(EControl.ATTACK, OnAttack);

        //FOCUS
        CheckPress(EControl.FOCUS, OnFocus);

        //RELOAD
        CheckPress(EControl.RELOAD, OnReload);

        //SPELL
        CheckPress(EControl.SPELL, OnSpell);

        //CHAT
        // TODO (Multiplayer) 

        //PAUSE
        // TODO
    }

    /// <summary>
    /// fire the <seealso cref="Action"/> if <seealso cref="EControl"/> is pressed
    /// </summary>
    private void CheckPress(EControl e, Action a){
        if (e.IsPressed()) a?.Invoke();
    }
    #endregion

}
