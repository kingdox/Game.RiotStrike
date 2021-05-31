#region Access
using UnityEngine.UI;
#endregion
#region Enums
#region IntroScene
namespace IntroRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in Intro item
    /// in <see cref="IntroScene.IntroManager"/>
    /// </summary>
    enum SkipText
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
    enum Text
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
#endregion
