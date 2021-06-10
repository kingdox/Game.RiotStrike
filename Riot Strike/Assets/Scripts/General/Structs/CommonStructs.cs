#region Access
using System;
using XavHelpTo;
using XavHelpTo.Know;
using Dat = Environment.Data;
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
///// <summary>
///// Json Structure of the Buffs
///// </summary>
//[Serializable]
//public struct BuffData
//{
//    public string NAME;
//    public string ID;
//    public string DESCRIPTION;
//    public float COOLDOWN;
//    public string OWNER;
//}
/// <summary>
/// Json Structure of the Character
/// </summary>
[Serializable]
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
[Serializable]
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
[Serializable]
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
[Serializable]
public struct StatData
{
    public string ID;
    public int STRENGHT;
    public int DEFENSE;
    public int SPEED;

    public StatData(string I, int STR, int DEF, int SPD)
    {
        ID=I;
        STRENGHT=STR;
        DEFENSE=DEF;
        SPEED=SPD;
    }

    /// <summary>
    /// Creates an array wih every stat value
    /// </summary>
    public int[] ToArray() => new int[3] {STRENGHT,DEFENSE,SPEED};
    public StatData RealStats => new StatData(ID,RealStrength, RealHealth, RealSpeed);

    /// <summary>
    /// Knows the real Strength
    /// </summary>
    public int RealStrength => PercentOf(STRENGHT).QtyOf(CallBaseStat.STRENGHT);
    /// <summary>
    /// Shows the real health
    /// </summary>
    public int RealHealth =>  PercentOf(DEFENSE).QtyOf(CallBaseStat.DEFENSE);
    /// <summary>
    /// Shows the real speed
    /// </summary>
    public int RealSpeed => PercentOf(SPEED).QtyOf(CallBaseStat.SPEED);



    /// <summary>
    /// Check the percent based on the <seealso cref="Dat.STAT_MAX"/>
    /// which determines the limit of stats
    /// </summary>
    private int PercentOf(int value) => value.PercentOf(Dat.STAT_MAX);

    /// <summary>
    /// Calls the base of Stats, where it contains the real values,
    /// not based on 0-<seealso cref="Dat.STAT_MAX"/>
    /// </summary>
    private StatData CallBaseStat => Dat.GetStatData(Dat.ID_BASE_STAT);


}
/// <summary>
/// Shows the information of the buff selected
/// </summary>
[Serializable]
public struct BuffData
{
    public string TYPE;
    public string TITLE;
    public string MESSAGE;

}
/// <summary>
/// Shows the information in key,
/// where the qty represent the lenght of the tutorial parts
/// </summary>
[Serializable]
public struct Tutorial
{
    public string titleKey;
    public string infoKey;
    public int qty;

    public Tutorial(string titleKey, string infoKey, int qty)
    {
        this.titleKey = titleKey;
        this.infoKey = infoKey;
        this.qty = qty;
    }
}
#endregion