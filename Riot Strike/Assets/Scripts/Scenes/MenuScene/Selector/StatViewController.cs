#region Access
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
#endregion
namespace MenuScene
{
    /// <summary>
    /// Controls the qty of images showed (that identify the power of a stat)
    /// in <seealso cref="Environment.Scenes.MENU_SCENE"/>
    /// </summary>
    public class StatViewController : MonoBehaviour
    {
        #region Variables
        private Image[] imgs;
        [Header("Stat View Controller")]
        public Transform tr_parent_imgs;
        #endregion
        #region Events
        private void Start()
        {
            tr_parent_imgs.Components(out imgs);
            SetQty(0);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Length of the images
        /// </summary>
        private int Length => imgs.Length;

        /// <summary>
        /// Set the qty of the character selected
        /// </summary>
        public void SetQty(int qty){
            for (int i = 0; i < Length; i++)imgs[i].enabled = i < qty;
        }

        
        #endregion
    }
}
