using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI[] counts;
    public void UpdateScore()
    {
        foreach (TextMeshProUGUI text in counts)
        {
            text.gameObject.SetActive(false);
        }
        for (int i = 0; i < App.Instance.playerStats.Count; i++)
        {
            counts[i].gameObject.SetActive(true);
            counts[i].text = App.Instance.playerStats[i].won.ToString();
        }
    }

    public void HideStats()
    {
        foreach (TextMeshProUGUI text in counts)
        {
            text.gameObject.SetActive(false);
        }
    }
}
