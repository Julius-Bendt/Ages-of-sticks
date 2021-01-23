using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public Weapon weapon;

    public void OnCollisionEnter2D(Collision2D o)
    {
        PlayerController p = o.gameObject.GetComponent<PlayerController>();

        if(p)
        {
            if(p.weapon == null)
            {
                p.PickupWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}
