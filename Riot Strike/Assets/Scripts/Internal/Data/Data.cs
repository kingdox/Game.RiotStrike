#region Imports
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
#endregion

namespace Environment
{
    #region Environment
    /// <summary>
    /// Representa los datos basicos del enviroment
    /// </summary>
    public class Data 
    {
        [HideInInspector]
        public static Data data = new Data();

        public const string savedPath = "saved.txt";
        public const string version = "v0.5.0";

        private const string DATA_KEY = "Data";

        //Screen Trigger Keys
        public static readonly string[] SCREEN_TRIGGERS = { "Show", "Hide" };
        //languages availables in game
        public static readonly string[] LANGUAGES =
        {
            TranslateSystem.DEFAULT_LANG,
            "English"
        };

        

        [Header("Json Data Loads")]
        public readonly static Achievement[] ACHIEVEMENTS = LoadData<Achievement>();
        public readonly static Credit[] CREDITS = LoadData<Credit>();
        public readonly static Control[] CONTROLS = LoadData<Control>();


        //TODO los nuevos
        public readonly static Buff[] BUFFS = LoadData<Buff>();
        public readonly static Spell[] SPELLS = LoadData<Spell>();
        public readonly static Weapon[] WEAPONS = LoadData<Weapon>();
        public readonly static Character[] CHARACTERS = LoadData<Character>();
        public readonly static Stat[] STATS = LoadData<Stat>();



        /// <summary>
        /// Loads the json data based on <typeparamref name="T"/> + <see cref="DATA_KEY"/>
        /// </summary>
        private static T[] LoadData<T>() => (typeof(T) + DATA_KEY).LoadJson<DataList<T>>().DATA;
        /// <summary>
        /// Returns a Control by a enum argument
        /// </summary>
        public static Control Control(EControl control) => CONTROLS[control.ToInt()];
        
    }
    /// <summary>
    /// Las escenas del juego, ordenadas como en "Build Settings"
    /// </summary>
    public enum Scenes
    {
        [InspectorName("Splash")]SPLASH_SCENE=0,
        [InspectorName("Tutorial")] TUTORIAL_SCENE =1,
        [InspectorName("Intro Video")] INTRO_SCENE =2,
        [InspectorName("Menú")] MENU_SCENE =3,
        [InspectorName("Juego")] GAME_SCENE =4
    }

    /// <summary>
    /// linked with <seealso cref="Data.SCREEN_TRIGGERS"/>
    /// </summary>
    public enum ScreenTrigger { SHOW, HIDE };

    /// <summary>
    /// Helper who generalize to a list of inforamtion, used in JSON files
    /// </summary>
    public struct DataList<T> { public T[] DATA; }
    #endregion
}



#region TODO MOver leugo

public struct Buff
{
    public string NAME;
    public string ID;
    public string DESCRIPTION;
    public float COOLDOWN;
    public string OWNER;
}
public struct Character
{
    public string NAME;
    public string NICKNAME;
    public string ROLE;
    public string DESCRITION;
    public string ID;
}
public struct Spell
{
    public string NAME;
    public string ID;
    public string DESCRIPTION;
    public float COOLDOWN;
    public string OWNER;
}
public struct Weapon
{
    public string NAME;
    public string ID;
    public string APPEARENCE;
    public int AMMO;
    public float RELOAD_TIME;
    public float CADENCE;
    public string OWNER;
}
public struct Stat
{
    public string ID;
    public int STRENGHT;
    public int DEFENSE;
    public int SPEED;
}

#endregion