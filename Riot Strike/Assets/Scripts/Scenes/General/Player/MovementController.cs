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

    #endregion
    #region Events
    private void Start() => this.Component(out player);
    #endregion
    #region Methods

    /// <summary>
    /// Move the player in X and Z based on the orientation of the transform in Z axis (forward)
    /// </summary>
    public void Move(float speed, float x, float y=0, float z=0)
    {
        movement.Set(x,y,z);

        // orientates the vector of movement
        movement = transform.rotation * movement;

        //normalize the values to max 1.
        movement = movement.normalized;

        player.Move(movement * speed * Time.deltaTime);
    }
    #endregion
}
