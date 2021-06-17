#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Set;
using XavHelpTo.Change;
using Dat = Environment.Data;
using Lore = SelectorRefresh.Lore;
using Quirk = SelectorRefresh.Quirk;
using Title = SelectorRefresh.Title;
using SelectorRefresh.Character;
#endregion

namespace MenuScene
{
    /// <summary>
    /// Manages the selection of the user player
    /// in <seealso cref="Environment.Scenes.MENU_SCENE"/>
    /// </summary>
    public class SelectorManager : MonoBehaviour
    {
        #region Variable
        private const string PATH_FOLDER_WEAPON = "Weapons";
        private const string PATH_FOLDER_SPELLS = "Spells";
        private const string KEY_TITLE_CHOOSE = "selector_title";

        private StatViewController[] ctrl_statViews;
        private RefreshController[] refresh_quirks;
        [Header("SelectorManager")]
        public ECharacter currentCharacter = ECharacter.NO;
        [Space]
        public RefreshController refresh_title;
        public Transform tr_parent_statViews;
        public Transform tr_parent_quirks;
        public RefreshController refresh_lore;
        [Space]
        public Transform tr_parent_character;
        public GameObject[] pref_characters;
        [Space]
        public Button btn_play;
        #endregion
        #region Event
        private void Start()
        {
            tr_parent_statViews.Components(out ctrl_statViews);
            tr_parent_quirks.Components(out refresh_quirks);

            ClearPanel();
        }
        #endregion
        #region Method
        /// <summary>
        /// Clear the information into the panel
        /// </summary>
        private void ClearPanel()
        {
            //title
            refresh_title.Translate(Title.Text.TITLE, KEY_TITLE_CHOOSE);

            //lore
            refresh_lore.RefreshText(Lore.Text.LORE, "");

            //quirks
            foreach (RefreshController r in refresh_quirks)
            {
                r.RefreshText(Quirk.Text.TITLE, "");
                r.RefreshText(Quirk.Text.DESCRIPTION, "");
            }

            tr_parent_character.ClearChilds();


        }
        /// <summary>
        /// Refresh the information visual of the character, includes
        /// <para>Stats, Spell weapon and Lore</para>
        /// <para>And the title of the selected character</para>
        /// </summary>
        private void RefreshCharacterInfo(ECharacter character)
        {

            CharacterData characterData = Dat.Character(character);
            StatData statData = Dat.GetStatData(characterData.ID);
            SpellData spellData = Dat.GetSpellData(characterData.ID);
            WeaponData weaponData = Dat.GetWeaponData(characterData.ID);

            //Refresh the title
            refresh_title.Translate(Title.Text.TITLE, characterData.NICKNAME);

            //Refresh the stats
            int[] stats = statData.ToArray();
            for (int i = 0; i < ctrl_statViews.Length; i++) ctrl_statViews[i].SetQty(stats[i]);

            //Refresh the quirks
            RefreshQuirk(EQuirk.SPELL, spellData.NAME, spellData.DESCRIPTION, $"{PATH_FOLDER_SPELLS}/{spellData.ID}");
            RefreshQuirk(EQuirk.WEAPON, weaponData.NAME, weaponData.APPEARENCE, $"{PATH_FOLDER_WEAPON}/{weaponData.ID}");

            //Refresh the lore
            refresh_lore.Translate(Lore.Text.LORE, characterData.DESCRITION);

            //Refresh the visual Character
            tr_parent_character.ClearChilds();
            Instantiate(pref_characters[character.ToInt()], tr_parent_character)
                .transform
                .Component(out RefreshController refresh_character);


            //Refresh the Random animation
            refresh_character
                .GetAnimator(Anim.CHARACTER)
                .SetTrigger(
                    Set.ToArray(
                        "Idle",
                        "Act1",
                        "Jump",
                        "Reload"
                    ).Any()
                );

        }
        /// <summary>
        /// Refresh the quirk, the name, description, and icon (loading)
        /// </summary>
        private void RefreshQuirk(EQuirk quirk,
            string name,
            string description,
            string pathFile
        )
        {
            RefreshController refresh = refresh_quirks[quirk.ToInt()];
            refresh.Translate(Quirk.Text.TITLE, name);
            refresh.Translate(Quirk.Text.DESCRIPTION, description);
            //refresh.RefreshImgColor(Quirk.Image.BACKGROUND, Color.white);

            //refresh the image
            string path = $"{Dat.PATH_ICON}/{pathFile}";
            string filePath = $"{Application.dataPath}/Resources/{path}.png";
            if (File.Exists(filePath))
            {
                Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                refresh.RefreshImgSprite(Quirk.Image.ICON, sprite);
            }
            else
            {
                $"ID no encontrado ó Problemas buscando el archivo con el path {filePath}".Print("red");
            }

        }
        /// <summary>
        /// Selects the character and refresh the information
        /// </summary>
        public void SelectCharacter(int i) {

            //TODO EJECUTAR UN AUDIO DEPENDIENDO DEL PJ
            //SI ES EL MISMO QUE EL ULTIMO PJ NO HACEMOS UIDO
            if (!currentCharacter.ToInt().Equals(i)){
                //hacemos el ruido

                $"escogido a {i}".Print();

            }



            //Tech 
            currentCharacter = (ECharacter)i;
            btn_play.interactable = true;

            //Visual
            RefreshCharacterInfo(currentCharacter);





            // Data
            SavedData saved = DataSystem.Get;
            saved.characterSelected = i;
            DataSystem.Set(saved);
        }


        #endregion

    }
}



