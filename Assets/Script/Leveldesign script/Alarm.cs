using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.Audio;

public class Alarm : MonoBehaviour
{
    public float min, max;

    public SpriteRenderer light_left, light_right, overlay;
    public Sprite red;
    public ShakeBehavior shake;

    public GameObject sounds;
    public AudioClip[] clip;

    public Color overlayOn, overlayOff;

    private void Start()
    {
        StartCoroutine(_Alarm());
    }

    public AudioClip GetGasClip()
    {
        return clip[Random.Range(0, clip.Length - 1)];
    }

    IEnumerator _Alarm()
    {
        yield return new WaitForSeconds(Random.Range(min, max));
        light_left.sprite = light_right.sprite = red;
        shake.TriggerShake(1000);
        StartCoroutine(ToggleLight());
        StartCoroutine(gasSounds());
        sounds.SetActive(true);
        yield return false;

    }

    IEnumerator gasSounds()
    {
        while(true)
        {
            AudioController.PlaySound(GetGasClip());
            yield return new WaitForSeconds(Random.Range(1, 5));
            yield return null;
        }
    }


    IEnumerator ToggleLight()
    {
        float time = 1.5f, timeElapsed = 0;
        overlay.color = overlayOff;

        while (true)
        {
            timeElapsed = 0;
            while (timeElapsed < time)
            {
                timeElapsed += Time.deltaTime;
                overlay.color = Color.Lerp(overlayOff, overlayOn, timeElapsed / time);
                yield return null;
            }

            timeElapsed = 0;
            yield return new WaitForSeconds(1f);

            while (timeElapsed < time)
            {
                timeElapsed += Time.deltaTime;
                overlay.color = Color.Lerp(overlayOn, overlayOff, timeElapsed / time);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
    }
}
