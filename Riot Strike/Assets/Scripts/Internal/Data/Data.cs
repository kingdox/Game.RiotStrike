#region Imports
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using System.Linq;
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

        public const string TAG_PLAYER = "Player";
        public const string TAG_ENEMY = "Enemy";
        public const int STAT_MAX = 5;
        //Screen Trigger Keys
        public static readonly string[] SCREEN_TRIGGERS = { "Show", "Hide" };
        //languages availables in game
        public static readonly string[] LANGUAGES =
        {
            TranslateSystem.DEFAULT_LANG,
            "English"
        };


        [Header("ID")]
        public const string ID_BASE_STAT = "Base";

        [Header("Paths")]
        public const string PATH_ICON = "Image/Sprites/Icons";

        

        [Header("Json Data Loads")]
        public readonly static StatData[] STATS = LoadData<StatData>();
        public readonly static AchievementData[] ACHIEVEMENTS = LoadData<AchievementData>();
        public readonly static CreditData[] CREDITS = LoadData<CreditData>();
        public readonly static ControlData[] CONTROLS = LoadData<ControlData>();


        //TODO los nuevos
        //TODO revisar BuffData public readonly static BuffData[] BUFFS = LoadData<BuffData>();
        public readonly static SpellData[] SPELLS = LoadData<SpellData>();
        public readonly static WeaponData[] WEAPONS = LoadData<WeaponData>();
        public readonly static CharacterData[] CHARACTERS = LoadData<CharacterData>();



        /// <summary>
        /// Loads the json data based on <typeparamref name="T"/>
        /// </summary>
        private static T[] LoadData<T>() => (typeof(T).ToString()).LoadJson<DataList<T>>().DATA;
        /// <summary>
        /// Returns a <see cref="ControlData"/> by a <see cref="EControl"/> argument
        /// </summary>
        public static ControlData Control(EControl control) => CONTROLS[control.ToInt()];
        /// <summary>
        /// returns the <see cref="CharacterData"/> in <see cref="ECharacter"/> enum cahracters
        /// </summary>
        public static CharacterData Character(ECharacter character) => CHARACTERS[character.ToInt()];

        /// <summary>
        /// Gets the <see cref="StatData"/> document with an ID
        /// </summary>
        public static StatData GetStatData(string ID) => STATS.Where(S => S.ID.Equals(ID)).First();
        /// <summary>
        /// Gets the <see cref="SpellData"/> document with an ID
        /// </summary>
        public static SpellData GetSpellData(string ID) => SPELLS.Where(S => S.ID.Equals(ID)).FirstOrDefault();
        /// <summary>
        /// Gets the <see cref="WeaponData"/> document with an ID
        /// </summary>
        public static WeaponData GetWeaponData(string ID) => WEAPONS.Where(W => W.ID.Equals(ID)).FirstOrDefault();
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

