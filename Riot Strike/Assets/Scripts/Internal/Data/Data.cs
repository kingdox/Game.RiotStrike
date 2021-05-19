#region Imports
using UnityEngine;
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
        public const string version = "v0.0.1";

        //Screen Trigger Keys
        public static readonly string[] SCREEN_TRIGGERS = { "Show", "Hide" };

    }
    /// <summary>
    /// Las escenas del juego, ordenadas como en "Build Settings"
    /// </summary>
    public enum Scenes
    {
        SplashScene=0,
        MenuScene=1,
        GameScene=2
    }

    /// <summary>
    /// linked with <seealso cref="Data.SCREEN_TRIGGERS"/>
    /// </summary>
    public enum ScreenTrigger { SHOW, HIDE };

    #endregion
}

