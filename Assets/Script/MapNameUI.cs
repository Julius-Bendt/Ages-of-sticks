using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapNameUI : MonoBehaviour
{
    public TextMeshProUGUI index;
    public Image background,logo;

    public MapNames[] maps;

    [System.Serializable]
    public struct MapNames
    {
        public string name;
        public Sprite timeLogo;
    }

    public void ShowName(string _name, int _index)
    {
        MapNames mn = maps[0];

        Debug.Log(_name);

        foreach (MapNames map in maps)
        {
            if (map.name.ToLower() == _name.ToLower())
                mn = map;
        }

        index.color = Color.clear;
        index.text = Roman.To(_index);

        background.color = logo.color = Color.black;
        logo.sprite = mn.timeLogo;

        StartCoroutine(_Show());
    }

    IEnumerator _Show()
    {
        App.Instance.playable = false;
        StartCoroutine(fadeLogo(true));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(fadeIndex(true));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(fadeLogo(false));
        StartCoroutine(fadeIndex(false));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(fadeBackground());
        App.Instance.playable = true;
    }

    IEnumerator fadeLogo(bool fadeIn)
    {
        float timeElapsed = 0;
        float time = 0.3f;

        Color start = logo.color;

        Color end = (fadeIn) ? Color.white : Color.clear;

        while(time > timeElapsed)
        {
            timeElapsed += Time.deltaTime;
            logo.color = Color.Lerp(start, end, timeElapsed / time);
            yield return null;

        }

        yield return null;
    }

    IEnumerator fadeIndex(bool fadeIn)
    {
        float timeElapsed = 0;
        float time = 0.3f;

        Color start = index.color;

        Color end = (fadeIn) ? Color.white : Color.clear;

        while (time > timeElapsed)
        {
            timeElapsed += Time.deltaTime;
            index.color = Color.Lerp(start, end, timeElapsed / time);
            yield return null;

        }

        yield return null;
    }

    IEnumerator fadeBackground()
    {
        float timeElapsed = 0;
        float time = 0.5f;

        Color start = background.color;
        while (time > timeElapsed)
        {
            timeElapsed += Time.deltaTime;
            background.color = Color.Lerp(start, Color.clear, timeElapsed / time);
            yield return null;

        }
        yield return null;
    }
}
