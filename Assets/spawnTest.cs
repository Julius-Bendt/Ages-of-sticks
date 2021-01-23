using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTest : MonoBehaviour
{
    public Transform[] points;
    public Weapon[] weapons;

    public void Start()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            Weapon w = weapons[i];
            GameObject g = Instantiate(w.gun, points[i].position, points[i].rotation);
            g.transform.localScale = w.GetScale();
            g.AddComponent<PickUpWeapon>().weapon = w;
            g.AddComponent<Rigidbody2D>();
            g.AddComponent<BoxCollider2D>();
        }
    }
}
