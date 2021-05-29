#region Access
using System;

#endregion
#region Structs
/// <summary>
/// Json Structure of the achievements
/// </summary>
[Serializable]
public struct AchievementData
{
    public string NAME;
    public string DESCRIPTION;
    public int REQUIREMENT;
    public string DEBUG_REQUIREMENT;
}
/// <summary>
/// Json Structure of the Credits
/// </summary>
[Serializable]
public struct CreditData
{
    public float[] BACKGROUND;//[float,float,float]
    public string TEXT;
    public string[] NAMES;
}
/// <summary>
/// Json Structure of the control info
/// </summary>
[Serializable]
public struct ControlData
{
    public string NAME;
    public string KEY;
    public string DEBUG_DESCRIPTION;
}
/// <summary>
/// Json Structure of the Buffs
/// </summary>
public struct BuffData
{
    public string NAME;
    public string ID;
    public string DESCRIPTION;
    public float COOLDOWN;
    public string OWNER;
}
/// <summary>
/// Json Structure of the Character
/// </summary>
public struct CharacterData
{
    public string NAME;
    public string NICKNAME;
    public string ROLE;
    public string DESCRITION;
    public string ID;
}
/// <summary>
/// Json Structure of the Spell
/// </summary>
public struct SpellData
{
    public string NAME;
    public string ID;
    public string DESCRIPTION;
    public float COOLDOWN;
    public string OWNER;
}
/// <summary>
/// Json Structure of the Weapon
/// </summary>
public struct WeaponData
{
    public string NAME;
    public string ID;
    public string APPEARENCE;
    public int AMMO;
    public float RELOAD_TIME;
    public float CADENCE;
    public string OWNER;
}
/// <summary>
/// Json Structure of the Stat
/// </summary>
public struct StatData
{
    public string ID;
    public int STRENGHT;
    public int DEFENSE;
    public int SPEED;
}
#endregion