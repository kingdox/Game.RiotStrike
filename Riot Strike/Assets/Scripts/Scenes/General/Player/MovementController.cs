#region Access
using UnityEngine;
using XavHelpTo;
#endregion
/// <summary>
/// Controller of the movement based on the saved inputs
/// dependency with <seealso cref="CharacterController"/>
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    #region Variables
    private CharacterController player;
    private Vector3 movement = new Vector3();
    [Header("Movement Controller")]
    [Range(1,10)]
    public float speed=5;

    #endregion
    #region Events
    private void Start() => this.Component(out player);
    private void Update()
    {
        Controls();
        Movement();

    }
    #endregion
    #region Methods

    /// <summary>
    /// Check every input pressed
    /// </summary>
    private void Controls()
    {
        //set the desired values of movement
        movement.Set(
            Utils.Axis(EControl.RIGHT, EControl.LEFT),
            0f,
            Utils.Axis(EControl.FORWARD, EControl.BACK)
        );
    }

    /// <summary>
    /// Move the player in X and Z based on the orientation of the transform in Z axis (forward)
    /// </summary>
    private void Movement()
    {
        // orientates the vector of movement
        movement = transform.rotation * movement;

        //normalize the values to max 1.
        movement = movement.normalized;

        player.Move(movement * speed * Time.deltaTime);
    }
    #endregion
}
