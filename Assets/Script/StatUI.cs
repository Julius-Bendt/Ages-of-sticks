using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image[] green, purple, blue, red, yellow;
    public Image healthBar;

    public int statId = 0;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthText.text = App.Instance.playerStats[statId].StartHealth().ToString();
        for (int i = 4; i >= 0; i--)
        {
            Color off = new Color(0.5f, 0.5f, 0.5f, 0.25f);
            green[i].color = (App.Instance.playerStats[statId].gravityUpgrades > i) ? Color.white : off;
            purple[i].color = (App.Instance.playerStats[statId].powerBoostUpgrades > i) ? Color.white : off;
            blue[i].color = (App.Instance.playerStats[statId].weaponMagnetUpgrades > i) ? Color.white : off;
            red[i].color = (App.Instance.playerStats[statId].rapidFireUpgrades > i) ? Color.white : off;
            yellow[i].color = (App.Instance.playerStats[statId].speedBoostUpgrades > i) ? Color.white : off;
        }
    }

}
