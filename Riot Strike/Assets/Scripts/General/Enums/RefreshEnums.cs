#region Access
using UnityEngine.UI;
#endregion
#region Enums
namespace IntroRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in Intro item
    /// in <see cref="IntroManager"/>
    /// </summary>
    enum SkipText
    {
        SKIP=0
    }
}
namespace CreditRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Image"/> in credit item
    ///  in <see cref="CreditManager"/>
    /// </summary>
    public enum RefreshImage
    {
        BACKGROUND = 0
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in credit item
    /// in <see cref="CreditManager"/>
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
        /// in <see cref="OptionManager"/>
        /// </summary>
        public enum RefreshButton
        {
            SWITCH = 0
        }
        /// <summary>
        /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in option item
        /// in <see cref="OptionManager"/>
        /// </summary>
        public enum RefreshText
        {
            INFO = 0,
            VALUE = 1
        }
    }
}
namespace LangRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Button"/> in splash item
    /// in <see cref="SplashManager"/>
    /// </summary>
    public enum RefreshButton
    {
        BUTTON = 0
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in splash item
    /// in <see cref="SplashManager"/>
    /// </summary>
    public enum RefreshText
    {
        LANG = 0,
    }
}
namespace ControlRefresh
{
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in key controls item
    /// in <see cref="ControlManager"/>
    /// </summary>
    public enum RefreshText
    {
        TITLE = 0,
        VALUE = 1,
        DEFAULT = 2,
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Button"/> in key controls items
    /// in <see cref="ControlManager"/>
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
    /// in <see cref="TutorialManager"/>
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
    /// in <see cref="AchievementManager"/>
    /// </summary>
    public enum RefreshImage
    {
        ICON = 0
    }
    /// <summary>
    /// Refresher enum in <see cref="RefreshController"/> of <see cref="Text"/> in achievement item
    /// in <see cref="AchievementManager"/>
    /// </summary>
    public enum RefreshText
    {
        NAME = 0,
        DESCRIPTION = 1
    }
}
#endregion