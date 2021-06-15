 #region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI = UnityEngine.UI;
using HUDRefresh;
using XavHelpTo;
using XavHelpTo.Change;
using XavHelpTo.Know;
using XavHelpTo.Set;
using XavHelpTo.Get;
using Dat = Environment.Data;

# endregion
/// <summary>
/// Refresh the HUD elements in the screen
/// </summary>
[RequireComponent(typeof(RefreshController))]
public class HUDController : MonoBehaviour
{
    #region Variables
    private const string PATH_SHOT_CURSOR = "ShotCursor/shot_";
    private const string PATH_SPELL_ICON = "Spells/";

    [Header("HUDController")]
    public RefreshController refresh;
    [Space]
    public Transform tr_parent_damageTexts;
    public Transform tr_parent_buffText;
    public GameObject pref_damageText;
    public GameObject pref_buffText;
    public float lenghtRandomDamage = 10;
    public float durationDamage = 2f;
    public float durationFadeDamage = 1f;
    #endregion
    #region Events
    private void Start()
    {
        this.Component(out refresh);

    }
    #endregion
    #region Methods

    /// <summary>
    /// Refresh the cursor of the player
    /// </summary>
    public void RefreshShotCursor(string ID)
    {
        string path = $"{Dat.PATH_ICON}/{PATH_SHOT_CURSOR}{ID}";
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        //loads the sprite
        refresh.RefreshImgSprite(Image.SHOT_CURSOR,sprite);
    }

    /// <summary>
    /// Refresh he icon showd in the HUD of the character
    /// </summary>
    public void RefreshSpellIcon(string ID)
    {
        string path = $"{Dat.PATH_ICON}/{PATH_SPELL_ICON}{ID}";
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        refresh.RefreshImgSprite(Image.SPELL_ICON, sprite);
        refresh.RefreshImgSprite(Image.SPELL, sprite);
    }

    /// <summary>
    /// Refreshes the lifeBar
    /// </summary>
    public void RefreshLife(int life, int maxLife)
    {
        //size
        refresh.GetImg(Image.LIFE).fillAmount = (1f * life).PercentOf(maxLife, true);
        //text
        refresh.GetText(Text.LIFE).text = life.ToString();
    }

    /// <summary>
    /// Refreshes the visual spell
    /// </summary>
    public void RefreshSpell(float time, float max)
    {
        string txt = "";
        float fill = 0;
        if (!time.Equals(0))
        {
            txt= (max - time.ToInt()).ToString();
            fill = 1-time.PercentOf(max, true);
        }
        // text
        refresh.GetText(Text.SPELL).text = txt;
        // size
        refresh.GetImg(Image.SPELL).fillAmount = fill;
    }


    /// <summary>
    /// Refreshes the values of the player weapon
    /// </summary>
    public void RefreshWeapon(int currentAmmo, int maxAmmo)
    {
        //text current
        refresh.GetText(Text.AMMO_CURRENT).text = currentAmmo.ToString();
        //text max
        refresh.GetText(Text.AMMO_MAX).text = maxAmmo.ToString();

        //size
        refresh.GetImg(Image.AMMO_CURRENT).fillAmount = (1f*currentAmmo).PercentOf(maxAmmo,true);
    }


    /// <summary>
    /// Refresh que amount of fill in image reload weapon
    /// </summary>
    /// <param name="count"></param>
    public void RefreshReload(float count, float max)
    {
        float fill = 0;
        if (!count.Equals(1)) fill = count.PercentOf(max, true);
        // size
        refresh.GetImg(Image.RELOAD).fillAmount = fill;

    }


    /// <summary>
    /// in Inspector can create a Damage Text
    /// </summary>
    [ContextMenu("_CreateDamageText")]
    public void _Test_CreateDamageText() => CreateDamageText(99.ZeroMax(), 300, 300);

    /// <summary>
    /// Creates a damage in front of the overlay to give feedback to the enemy status
    /// the status is represented by the color
    /// </summary>
    public void CreateDamageText(int damage,int life,int max ){
        Color color = Color.white;//Get.RandomColor();
        color.ColorParam(ColorType.RGB.ToInt(), (1f*life).PercentOf(max, true));
        int valueToShow = life > damage
            ? damage
            :  life
        ;
        UI.Text txt = DoText(tr_parent_damageTexts);
        txt.color = color;
        txt.text = valueToShow.ToString();
        StartCoroutine(FadeDamageText(txt));

        //Text FUnnny
        //txt.text = Set.ToArray("POW","SMACK", "WOW").Any();// valueToShow.ToString();
    }

    public UI.Text DoText(Transform parent) {
        Vector3 variation = new Vector3(1,1,0) * (Random.insideUnitCircle * lenghtRandomDamage);
        Instantiate(
            pref_damageText,
            parent.position + variation,
            Quaternion.identity,
            parent
        ).transform
        .Component(out UI.Text txt);

        return txt;
    } 

    /// <summary>
    /// Creates the text of the buff with colors
    /// </summary>
    public void CreateBuffText(string message, Color color) {
        UI.Text txt = DoText(tr_parent_buffText);
        txt.color = color;
        txt.text = message;
        StartCoroutine(FadeDamageText(txt));
    }

    /// <summary>
    /// Do the fade of the text and then destroy it
    /// </summary>
    IEnumerator FadeDamageText(UI.Text txt)
    {
        yield return new WaitForSeconds(durationDamage);
        float count = 0;
        while (!durationFadeDamage.TimerIn(ref count)){
            txt.CrossFadeAlpha(0, durationFadeDamage, false);
            yield return new WaitForEndOfFrame();
        }
        Destroy(txt.gameObject);
    }
    #endregion
}
