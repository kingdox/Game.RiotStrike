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
public class PlayerBody : Body
{
    #region Variable
    private MovementController movement;
    private RotationController rotation;
    private const string KEY_AXIS_X = "Mouse X";
    private const string KEY_AXIS_Y = "Mouse Y";
    [Header("Player Body")]
    public HUDController ctrl_HUD;
    public bool canMove = true;
    public bool canRotate = true;
    #endregion
    #region Event
    public override void Awake() {
        base.Awake();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        Subscribes();
    }
    private void Start()
    {
        this.Component(out movement);
        this.Component(out rotation);
        EmitLife();
        ctrl_HUD.RefreshShotCursor(character.weapon.ID);
    }
    private void Update()
    {
        Control();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        UnSubscribes();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Do the subscriptions
    /// </summary>
    public void Subscribes()
    {
        character.spell.OnTimer += ctrl_HUD.RefreshSpell;
        character.weapon.OnFireAttack += ctrl_HUD.RefreshWeapon;
        character.weapon.OnReload += ctrl_HUD.RefreshReload;
    }
    /// <summary>
    /// Do the UnSubscriptions
    /// </summary>
    public void UnSubscribes()
    {
        character.spell.OnTimer -= ctrl_HUD.RefreshSpell;
        character.weapon.OnFireAttack -= ctrl_HUD.RefreshWeapon;
        character.weapon.OnReload -= ctrl_HUD.RefreshReload;
    }
    /// <summary>
    /// Emit the Life status
    /// </summary>
    private void EmitLife() => ctrl_HUD.RefreshLife(life, stat.DEFENSE);

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
        CheckPress(EControl.ATTACK, character.OnAttack);

        //FOCUS
        CheckPress(EControl.AIM, character.OnAim);//TODO FIXME

        //RELOAD
        CheckPressDown(EControl.RELOAD, character.OnReload);

        //SPELL
        CheckPressDown(EControl.CAST, character.OnCast);

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
    /// <summary>
    /// as <seealso cref="CheckPress(EControl, Action)"/> just in the frame
    /// </summary>
    private void CheckPressDown(EControl e, Action a)
    {
        if (e.IsPressedDown()) a?.Invoke();
    }


    /// <summary>
    /// Do the base take damage and refresh the HUD of the player
    /// </summary>
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        EmitLife();
    }


    #endregion

}
