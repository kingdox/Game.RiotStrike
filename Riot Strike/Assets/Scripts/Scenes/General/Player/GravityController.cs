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
    private bool ignorefollowingImpact=false;
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

        // if is semi-grounded / grounded
        if (player.velocity.y < -0.5f)
        {
            gravityAcelerator += (-GRAVITY_EARTH / 2) * Time.deltaTime;
        }
        else // if falling
        {
            if (!ignorefollowingImpact)
            {
                // if aceleration reach the min damage then emits the impact
                if (!gravityAcelerator.ToInt().Equals(1))
                {
                    OnImpact?.Invoke(-gravityAcelerator);
                }
            }
            else
            {
                OnImpact?.Invoke(0);
                ignorefollowingImpact = false;
            }
            gravityAcelerator = 1;
        }
    }

    /// <summary>
    /// Ignores the following impact
    /// </summary>
    public void IgnoreFollowingImpact() => ignorefollowingImpact = true;
    #endregion
}
