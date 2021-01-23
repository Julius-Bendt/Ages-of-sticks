using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LeaderboardUI : MonoBehaviour
{
    public GameObject[] elem;
    public PlayerSprites[] playerSprites;
    public Transform[] placements;


    [System.Serializable]
    public struct PlayerSprites
    {
        public Color c;
        public GameObject obj;
    }

    void Start()
    {
        UpdateUI();
    }


    public void UpdateUI()
    {
        Stats[] s = App.Instance.playerStats.OrderByDescending(_s => _s.won).ToArray();

        for (int i = 0; i < s.Length; i++)
        {
            Stats p = s[i];
            string c = ColorUtility.ToHtmlStringRGB(s[i].c);


            elem[i].name = "i = " + i;
            elem[i].SetActive(true);

            TextMeshProUGUI id = elem[i].transform.Find("id").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI stats = elem[i].transform.Find("stats").GetComponent<TextMeshProUGUI>();

            PlayerSprites sprite = playerSprites[0];

            foreach (PlayerSprites item in playerSprites)
            {
                if (item.c == p.c)
                    sprite = item;
            }

            sprite.obj.transform.position = placements[i].position;

            id.text = string.Format(id.text, "<color=#" + c + ">", "Player " + s[i].controllerId);
            stats.text = string.Format(stats.text, s[i].won, s[i].death, s[i].shotsFired, s[i].damageRecived, s[i].Upgrades());

            /*
            if (App.Instance.playerStats.Count > i) //-1 because winner
            {
                elem[i].SetActive(true);

                TextMeshProUGUI id = elem[i].transform.Find("id").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI stats = elem[i].transform.Find("stats").GetComponent<TextMeshProUGUI>();


                id.text = string.Format(id.text, "<color=#" + c + ">", "Player " + s[i].controllerId);
                stats.text = string.Format(stats.text, s[i].won, s[i].death, s[i].shotsFired, s[i].damageRecived, s[i].Upgrades());
            }
            else
            {
                elem[i].SetActive(false);
            }
            */
        }

       

        /*
        id.text = string.Format(id.text,"<color=#" + c + ">","Player " + winner.controllerId);
        stats.text = string.Format(stats.text, winner.won, winner.death, winner.shotsFired, winner.damageRecived, winner.Upgrades());
        */

    }
}
