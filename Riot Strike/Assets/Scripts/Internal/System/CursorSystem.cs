#region Access
using System;
using UnityEngine;
using XavHelpTo;
# endregion
/// <summary>
/// System to adjust the cursor in order to fit with the game status
/// </summary>
public class CursorSystem : MonoBehaviour
{
    #region Variables
    private static CursorSystem _;
    //public static Action<bool> OnCursor;
    #endregion
    #region Events
    private void Awake() => this.Singleton(ref _);
    #endregion
    #region Methods
    /// <summary>
    /// Displays or not the cursor, adjusting it on their position, emit an <seealso cref="Action"/> saying wether is displayed or not
    /// </summary>
    private static void Display(CursorLockMode c)
    {
        bool isLocked = c.Equals(CursorLockMode.Locked);
        Cursor.lockState = c;
        Cursor.visible = !isLocked;
        //OnCursor?.Invoke(!isLocked);
    }
    /// <summary>
    /// Hides the cursor
    /// </summary>
    public static void Hide() => Display(CursorLockMode.Locked);

    /// <summary>
    /// Shows the cursor
    /// </summary>
    public static void Show() => Display(CursorLockMode.None);

    [ContextMenu("Show")]
    /// <summary>
    /// Shows in inspector the option to show the cursor
    /// </summary>
    public void _Show() => Show();
    [ContextMenu("Hide")]
    /// <summary>
    /// Shows in inspector the option to HIDE the cursor
    /// </summary>
    public void _Hide() => Hide();
    #endregion
}
