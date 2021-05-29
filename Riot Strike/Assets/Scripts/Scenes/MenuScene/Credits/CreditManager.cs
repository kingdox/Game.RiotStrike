#region
using UnityEngine;
using System;
using XavHelpTo.Change;
using CreditRefresh;
#endregion
namespace MenuScene
{
    /// <summary>
    /// Manages the credits of the game, in <see cref="Environment.Scenes.MENU_SCENE"/>
    /// </summary>
    public class CreditManager : MonoBehaviour
    {
        #region Variable
        [Header("Achievement Manager")]
        public Transform tr_parent_credits;
        public GameObject pref_creditItem;

        #endregion
        #region Event
        private void Start()
        {
            foreach (var c in Environment.Data.CREDITS) CreateCreditList(in c);
        }
        #endregion
        #region Method

        /// <summary>
        /// Generates the list of a creditList, with title and their names
        /// </summary>
        /// <param name="c"></param>
        private void CreateCreditList(in CreditData c)
        {
            //1. creamos el titulo
            CreateCredit(c.TEXT, c.BACKGROUND.ToColor(), true);
            //2. creamos un for de los nombres
            foreach (string n in c.NAMES) CreateCredit(n, Color.black);
        }

        /// <summary>
        /// Create a credit
        /// </summary>
        private void CreateCredit(string name, Color color, bool isTitle = false)
        {
            RefreshController _refresh = RefreshController.CreateRefresh(in pref_creditItem, in tr_parent_credits);

            if (isTitle) _refresh.Translate(RefreshText.NAME, name);
            else _refresh.RefreshText(RefreshText.NAME, name);

            _refresh.RefreshImgColor(RefreshImage.BACKGROUND, color);
        }




        #endregion
    }
}



