#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HUDRefresh;
using XavHelpTo;
using XavHelpTo.Change;
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
    public RefreshController refresh;
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
    #endregion
}
