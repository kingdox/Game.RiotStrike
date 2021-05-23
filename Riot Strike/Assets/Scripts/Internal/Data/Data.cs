#region Imports
using UnityEngine;
using XavHelpTo;
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
        public const string version = "v0.0.5";

        //Screen Trigger Keys
        public static readonly string[] SCREEN_TRIGGERS = { "Show", "Hide" };


        [Header("Json Data Loads")]        
        public readonly static Achievement[] ACHIEVEMENTS = "AchievementData".LoadJson<AchievementList>().ACHIEVEMENTS;
        public readonly static Credit[] CREDITS = "CreditData".LoadJson<CreditList>().CREDITS;
        public readonly static TutorialList TUTORIAL = "TutorialData".LoadJson<TutorialList>();
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

    #endregion
}

