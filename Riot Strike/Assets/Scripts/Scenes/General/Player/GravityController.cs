#region Access
using UnityEngine;
using XavHelpTo;
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
    [Header("Gravity Controller")]
    public const float GRAVITY_EARTH= -9.807f;//9,807 m/s²
    #endregion
    #region Event
    private void Start() => this.Component(out player);
    private void Update() => ApplyGravity();
    #endregion
    #region Methods
    /// <summary>
    /// Apply the gravity force
    /// </summary>
    private void ApplyGravity() => player.Move(transform.up * GRAVITY_EARTH * Time.deltaTime);
    #endregion
}
