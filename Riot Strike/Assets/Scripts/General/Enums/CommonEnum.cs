#region Access
using UnityEngine;
#endregion
#region Enums
/// <summary>
/// Identification of the player controller
/// </summary>
public enum EControl
{
    ATTACK=0,
    FOCUS=1,
    RELOAD=2,
    SPELL=3,
    FORWARD=4,
    BACK=5,
    LEFT=6,
    RIGHT=7,
    CHAT=8, // multiplayer only
    PAUSE=9,
}
/// <summary>
/// Identifyier of the player characters availables
/// </summary>
public enum ECharacter{
    NO = -1,
    /// <summary>
    /// Healer spell, Wear a Sniper
    /// </summary>
    [InspectorName("Enlai Ming")] ENLAI_MING = 0,
    /// <summary>
    /// Jump and speed, Wear bombs grenades
    /// </summary>
    [InspectorName("Tabare Flare")]TABARE_FLARE = 1,
    /// <summary>
    /// Tank, wear a gun
    /// </summary>
    [InspectorName("Cole Megalos")]COLE_MEGALOS = 2,
    /// <summary>
    /// Samurai guy, katana weapon
    /// </summary>
    [InspectorName("Akiyama Arata")]AKIYAMA_ARATA = 3,
    /// <summary>
    /// Robot
    /// </summary>
    [InspectorName("X-F20")]X_F20 = 4,
}
/// <summary>
/// Enumeration of the character stats
/// </summary>
public enum EStat
{
    ATTACK=0,
    DEFENSE=1,
    SPEED=2,
    HEALTH=3,
}
#endregion