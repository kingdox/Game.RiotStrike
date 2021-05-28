#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
# endregion

public class IntroGun : MonoBehaviour
{
    #region Variables

    public ParticleSystem part_shot;

    #endregion
    private void Start()
    {
    }
    #region
    public void GunAnimShow() { }
    public void GunAnimShot()
    {
        part_shot.Play();
    }
    #endregion

}
