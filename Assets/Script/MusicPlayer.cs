using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public AudioClip stoneage, renaissance, modern, scifi, lobby;
    private AudioSource source;

    private void Start()
    {
        source.loop = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Contains("stoneage"))
        {
            source.clip = stoneage;
        }
        else if(scene.name.Contains("renaissance"))
        {
            source.clip = renaissance;
        }
        else if (scene.name.Contains("modern"))
        {
            source.clip = modern;
        }
        else if (scene.name.Contains("scifi"))
        {
            source.clip = scifi;
        }
        else if (scene.name.Contains("lobby"))
        {
            source.clip = lobby;
        }

        if(source == null)
            source = GetComponent<AudioSource>();

        source.Play();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
