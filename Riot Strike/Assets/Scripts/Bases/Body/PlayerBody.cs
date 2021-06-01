#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion

/// <summary>
/// Management of the body who player can `play`
/// </summary>
public class PlayerBody : BodyBase
{
    #region Variable

//[CreateAssetMenu(menuName = "Character/Player")]

    #endregion
    #region Event
    private void Update()
    {
        Control();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Controls the actions of the player
    /// TODO hacer que movement controller y rotation controller se manejen aquí (el input) tambien y se ejecuten allá
    /// </summary>
    private void Control()
    {

    }
    #endregion

}
