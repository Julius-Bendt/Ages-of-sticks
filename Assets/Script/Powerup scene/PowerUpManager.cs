using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public StatUI[] uis;
    public void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            uis[i].gameObject.SetActive(App.Instance.playerStats.Count > i);
        }
    }

    public void BoughtUpgrade(int playerStatId, int statId)
    {
        if(App.Instance.playerStats[playerStatId].Upgrades() < 9)
        {
            switch (statId)
            {
                case 0:
                    App.Instance.playerStats[playerStatId].gravityUpgrades++;
                    break;
                case 1:
                    App.Instance.playerStats[playerStatId].powerBoostUpgrades++;
                    break;
                case 2:
                    App.Instance.playerStats[playerStatId].weaponMagnetUpgrades++;
                    break;
                case 3:
                    App.Instance.playerStats[playerStatId].rapidFireUpgrades++;
                    break;
                case 4:
                    App.Instance.playerStats[playerStatId].speedBoostUpgrades++;
                    break;
            }

            Debug.Log("hehe");
            uis[playerStatId].UpdateUI();
        }
    }

    private void Update()
    {
        if (FindObjectsOfType<PlayerController>().Length == 0)
            App.Instance.NextLevel();
    }
}
