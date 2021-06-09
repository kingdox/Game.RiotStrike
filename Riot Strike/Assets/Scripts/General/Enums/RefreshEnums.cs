#region Access
using UnityEngine.UI;
using Dat = Environment.Data;
using Environment;
using UnityEngine;
#endregion
#region Enums
#region IntroScene
namespace IntroRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in Intro item
    /// in <see cref="IntroScene.IntroManager"/>
    /// </summary>
    public enum SkipText
    {
        SKIP=0
    }
}
#endregion
#region TutorialScene
namespace TutorialGameRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in tutorial game skip item
    /// in <see cref="TutorialScene.TutorialGameManager"/>
    /// </summary>
    public enum Text
    {
        SKIP=0
    }
}
#endregion
#region SplashScene
namespace LangRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Button"/> in splash item
    /// in <see cref="SplashScene.SplashManager"/>
    /// </summary>
    public enum RefreshButton
    {
        BUTTON = 0
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in splash item
    /// in <see cref="SplashScene.SplashManager"/>
    /// </summary>
    public enum RefreshText
    {
        LANG = 0,
    }
}
#endregion
#region MenuScene
namespace OWORefresh
{
    /// <summary>
    /// Refresher of <seealso cref="RefreshController"/> of <seealso cref="InputField"/>
    /// in <seealso cref="MenuScene.OWOManager"/>
    /// </summary>
    public enum Input
    {
        IP=0
    }
    /// <summary>
    /// Refresher of <seealso cref="RefreshController"/> of <seealso cref="Button"/>
    /// in <seealso cref="MenuScene.OWOManager"/>
    /// </summary>
    public enum Button
    {
        CONNECT = 0
    }
    /// <summary>
    /// Refresher of <seealso cref="RefreshController"/> of <seealso cref="Text"/>
    /// in <seealso cref="MenuScene.OWOManager"/>
    /// </summary>
    public enum Text
    {
        [InspectorName("Resultado de conexión")] RESULT_CONECTION = 0
    }
}
namespace CreditRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Image"/> in credit item
    ///  in <see cref="MenuScene.CreditManager"/>
    /// </summary>
    public enum RefreshImage
    {
        BACKGROUND = 0
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in credit item
    /// in <see cref="MenuScene.CreditManager"/>
    /// </summary>
    public enum RefreshText
    {
        NAME = 0,
    }
}
namespace OptionRefresh
{

    namespace Switch
    {
        /// <summary>
        /// Refresher enum in <see cref="RefreshController"/> of <see cref="Button"/> in option item
        /// in <see cref="MenuScene.OptionManager"/>
        /// </summary>
        public enum RefreshButton
        {
            SWITCH = 0
        }
        /// <summary>
        /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in option item
        /// in <see cref="MenuScene.OptionManager"/>
        /// </summary>
        public enum RefreshText
        {
            INFO = 0,
            VALUE = 1
        }
    }
}
namespace ControlRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in key controls item
    /// in <see cref="MenuScene.ControlManager"/>
    /// </summary>
    public enum RefreshText
    {
        TITLE = 0,
        VALUE = 1,
        DEFAULT = 2,
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Button"/> in key controls items
    /// in <see cref="MenuScene.ControlManager"/>
    /// </summary>
    public enum RefreshButton
    {
        KEY = 0,
        RESET = 1
    }
}
namespace TutorialRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in tutorial item
    /// in <see cref="MenuScene.TutorialManager"/>
    /// </summary>
    public enum RefreshText
    {
        INFO = 0
    }
}
namespace AchievementRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Image"/> in achievement item
    /// in <see cref="MenuScene.AchievementManager"/>
    /// </summary>
    public enum RefreshImage
    {
        ICON = 0
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in achievement item
    /// in <see cref="MenuScene.AchievementManager"/>
    /// </summary>
    public enum RefreshText
    {
        NAME = 0,
        DESCRIPTION = 1
    }
}
namespace SelectorRefresh
{
    namespace Title
    {
        /// <summary>
        /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in title
        /// in <seealso cref="MenuScene.SelectorManager"/>
        /// </summary>
        public enum Text
        {
            TITLE=0
        }
    }
    namespace Quirk
    {
        /// <summary>
        /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in quirks item
        /// in <seealso cref="MenuScene.SelectorManager"/>
        /// </summary>
        public enum Text
        {
            TITLE=0,
            DESCRIPTION=1,
        }
        /// <summary>
        /// Refresher enum in <see cref="RefreshController"/> of <see cref="Image"/> in quirks item
        /// in <seealso cref="MenuScene.SelectorManager"/>
        /// </summary>
        public enum Image
        {
            BACKGROUND=0,
            ICON=1
        }
    }
    namespace Lore
    {
        /// <summary>
        /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in lore area of the panel info selector 
        /// in <seealso cref="MenuScene.SelectorManager"/>
        /// </summary>
        public enum Text
        {
            LORE = 0,
        }
    }
}
#endregion
#region inGame
namespace HUDRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in player HUD
    /// <para> in <seealso cref="Scenes.TUTORIAL_SCENE"/> and <seealso cref="Scenes.GAME_SCENE"/></para>
    /// </summary>
    public enum Text
    {
        LIFE=0,
        SPELL=1,
        AMMO_MAX=2,
        AMMO_CURRENT=3,
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Image"/> in player HUD
    /// <para> in <seealso cref="Scenes.TUTORIAL_SCENE"/> and <seealso cref="Scenes.GAME_SCENE"/></para>
    /// </summary>
    public enum Image
    {
        LIFE=0,
        SPELL=1,
        SHOT_CURSOR=2,
        AMMO_CURRENT=3,
        RELOAD=4,
    }

}

namespace SpellsRefresh
{
    namespace HealSpell
    {
        /// <summary>
        /// Refresher of the particle for <seealso cref="HealSpell"/>
        /// </summary>
        public enum Particle
        {
            HEAL = 0,
            HEAL_AREA = 1
        }
    }
    namespace AntiGravityJumpSpell
    {
        /// <summary>
        /// Refresher of the particle for <seealso cref="AntiGravityJumpSpell"/>
        /// </summary>
        public enum Particle
        {
            JUMP=0,
            FALL=1
        }
    }
    namespace ElectroSpell
    {
        /// <summary>
        /// Refresher of the particle for <seealso cref="ElectroSpell"/>
        /// </summary>
        public enum Particle
        {
            ELECTRO = 0,
        }
    }
}
namespace WeaponRefresh
{
    namespace Ranged
    {
        /// <summary>
        /// Refresher of the particle for <seealso cref="SniperRifleRangedWeapon"/>
        /// </summary>
        public enum Particle
        {
            LINE=0,
            IMPACT=1,
            //SPLAT=2
        }
    }
    namespace Near{
        //.... TODO (to new version)
    }
}
namespace BuffRefresh
{
    namespace StatBuff
    {
        /// <summary>
        /// Refresher of the effects
        /// </summary>
        public enum Particle
        {
            CREATION = 0,
            DESTRUCTION = 1,
            CONSTANT = 2
        }
    }
}
#endregion
#endregion
