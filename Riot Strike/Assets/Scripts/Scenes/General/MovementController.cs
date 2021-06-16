#region Access
using UnityEngine;
using UnityEngine.AI;
using XavHelpTo;
#endregion
/// <summary>
/// Controller of the movement
/// </summary>
public class MovementController : MonoBehaviour
{
    #region Variables
    //private CharacterController player;
    private Vector3 movement = new Vector3();
    #endregion
    #region Events
    #endregion
    #region Methods
    /// <summary>
    /// check if it can move
    /// </summary>
    private bool CanMove => enabled && !Time.timeScale.Equals(0);

    /// <summary>
    /// Move the player in X and Z based on the orientation of the transform in Z axis (forward)
    /// </summary>
    public void Move(CharacterController player, float speed, float x, float y = 0, float z = 0)
    {
        if (!CanMove) return; //🛡

        movement.Set(x, y, z);

        // orientates the vector of movement
        movement = player.transform.rotation * movement;

        //normalize the values to max 1.
        movement = movement.normalized;

        player.Move(movement * speed * Time.deltaTime);
    }
    /// <summary>
    /// Do the movement for a agent
    /// </summary>
    public void Move(CharacterController controller, NavMeshAgent agent, float speed, Vector3 destination)
    {
        if (!CanMove) return; //🛡
        agent.destination = destination;
        controller.Move(
            agent.desiredVelocity.normalized
            * speed
            * Time.deltaTime
        );

        //equales the value to keep in the same area
        agent.velocity = controller.velocity;

        //TODO hacer que el movimiento de este permita

    }
    #endregion
}
