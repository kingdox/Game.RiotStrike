#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HUDRefresh;
using XavHelpTo;
using XavHelpTo.Change;
using XavHelpTo.Set;
using XavHelpTo.Get;
# endregion
/// <summary>
/// Refresh the HUD elements in the screen
/// </summary>
[RequireComponent(typeof(RefreshController))]
public class HUDController : MonoBehaviour
{
    #region Variables
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



    public void RefreshWeapon(int currentAmmo, int maxAmmo)
    {
        //text current
        refresh.GetText(Text.AMMO_CURRENT).text = currentAmmo.ToString();
        //text max
        refresh.GetText(Text.AMMO_MAX).text = maxAmmo.ToString();

        //size TODO to check
        refresh.GetImg(Image.BG_AMMO_CURRENT).fillAmount = currentAmmo.PercentOf(maxAmmo);
    }
    #endregion
}
