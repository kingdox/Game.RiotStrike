#region Access
using System;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
#endregion
/// <summary>
/// Set gravity to the player
/// dependency with <seealso cref="CharacterController"/>
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class GravityController : MonoBehaviour
{
    #region Variable
    private CharacterController player;
    private float gravityAcelerator;
    [Header("Gravity Controller")]
    public const float GRAVITY_EARTH= -9.807f;//9,807 m/s²
    public Action<float> OnImpact;
    #endregion
    #region Event
    private void Start()
    {
        this.Component(out player);
        gravityAcelerator = 1;
    }
    private void Update()
    {

        ApplyGravity();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Apply the gravity force
    /// </summary>
    private void ApplyGravity()
    {
        //Physics.gravity

        //apply the fall
        player.Move(transform.up * GRAVITY_EARTH * gravityAcelerator * Time.deltaTime);
        CheckDistance();
    }

    /// <summary>
    /// Check the distance and amplify the <seealso cref="gravityAcelerator"/>
    /// </summary>
    private void CheckDistance(){
        if (player.velocity.y < -0.5f) gravityAcelerator += (-GRAVITY_EARTH/2) * Time.deltaTime;
        else
        {
            if (!gravityAcelerator.ToInt().Equals(1)) OnImpact?.Invoke(-gravityAcelerator);
            gravityAcelerator = 1;
        }
    }
    #endregion
}
