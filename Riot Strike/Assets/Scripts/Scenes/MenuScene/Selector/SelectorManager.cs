#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using Dat = Environment.Data;
# endregion

namespace MenuScene
{
    /// <summary>
    /// Manages the selection of the user player
    /// <para>TODO If you're on multiplayer, then it shows the players selected of the others inGame</para>
    /// in <seealso cref="Environment.Scenes.MENU_SCENE"/>
    /// </summary>
    public class SelectorManager : MonoBehaviour
    {
        #region Variable
        private StatViewController[] ctrl_statViews;
        [Header("SelectorManager")]
        public RefreshController refresh_title;
        public Transform tr_parent_statViews;

        #endregion
        #region Event
        private void Start()
        {
            tr_parent_statViews.Components(out ctrl_statViews);
        }
        #endregion
        #region Method


        /// <summary>
        /// Selects the character and refresh the information
        /// </summary>
        public void SelectCharacter(int i) { }

        /// <summary>
        /// Refresh the information visual of the character, includes
        /// <para>Stats, Spell weapon and Lore</para>
        /// <para>And the title of the selected character</para>
        /// </summary>
        private void RefreshCharacterInfo(ECharacter character) {

            CharacterData characterData = Dat.Character(character);
            StatData statData = Dat.GetStatData(characterData.ID);
            SpellData spellData = Dat.GetSpellData(characterData.ID);
            WeaponData weaponData = Dat.GetWeaponData(characterData.ID);



        }

        #endregion

    }
}

namespace QuirkRefresh
{

}
