#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion
/// <summary>
/// Throw misiles who fetch the nearest target
/// </summary>
public class DroneRocketSpell : Spell
{
    #region Variable
    [Header("Drone Rocket Spell")]
    public GameObject pref_bullet;

    #endregion
    #region Event

    #endregion
    #region Method
    /// <summary>
    /// Send their rockets from the shoulder
    /// </summary>
    public override void Cast(Body body)
    {
        if (!CanCast()) return; // 🛡
        AudioSystem.PlaySound(clip);
        LaunchRocket(body);
    }
    /// <summary>
    /// Start the Casting of the rocket
    /// </summary>
    private void LaunchRocket(Body body)
    {
        //TODO, en teoria el misil sale por la espalada, si hay tiempo no usar RangedWeapon sino
        // crearla desde aquí.
        RangedWeapon weapon = body.character.weapon as RangedWeapon;
        Bullet bullet = weapon.Shot(body, pref_bullet);
        bullet.damage *= 2;
        //ASSIGN TARGET
        Body[] bodys = FindObjectsOfType(typeof(Body)) as Body[];

        float Distance( Body body2) =>Vector3.Distance(body.transform.position, body2.transform.position);


        Body newBody;

        int indexNearest = 0;
        float lenght = -1;
        for (int i = 0; i < bodys.Length; i++){
            float entryLength = Distance(bodys[i]);
            // if the distance of the last is worst than the current then refresh with the nearest positioned body
            if (lenght.Equals(-1) || (lenght > entryLength) && !bodys[i].CompareTag(body.tag)){
                //"OBJETIVO ENCONTRADO".Print("magenta");
                indexNearest = i;
                lenght = entryLength;
            }
        }

        if (bodys.Length>0)
        {
            newBody = bodys[indexNearest];
            bullet.tr_target = newBody.tr_head;
        }
        else
        {
            //"NO ENOCNTRAMOS OBJETIVOS".Print("red");
        }
    }
    #endregion
}
