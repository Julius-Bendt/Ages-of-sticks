using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    //Skills
    public float health = 100; 

    public float StartHealth()
    {
        return 100 - Upgrades() * 10;
    }



    //Gravity done, speed done,
    public int gravityUpgrades, powerBoostUpgrades, weaponMagnetUpgrades, rapidFireUpgrades, speedBoostUpgrades;

    public int Upgrades()
    {
        return gravityUpgrades + powerBoostUpgrades + weaponMagnetUpgrades + rapidFireUpgrades + speedBoostUpgrades;
    }

    public float Gravity()
    {
        return gravityUpgrades * 0.05f;
    }

    public float PowerBoost()
    {
        return powerBoostUpgrades * 0.05f;
    }

    public float WeaponMagnet()
    {
        return weaponMagnetUpgrades * 2f;
    }

    public float RapidFire()
    {
        return rapidFireUpgrades * 0.001f;
    }

    public float SpeedBoost()
    {
        return speedBoostUpgrades * 0.75f;
    }

    //Controller
    public int controllerId;


    //Game
    public float won, death, shotsFired, damageRecived;

    public Color c;
}
