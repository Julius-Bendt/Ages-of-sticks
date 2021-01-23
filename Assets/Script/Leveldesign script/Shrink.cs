using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{

    public float time;
    public Vector3 toSize;

    private void Start()
    {
        StartCoroutine(ShrinkFunc());
    }

    IEnumerator ShrinkFunc()
    {
        Vector3 startScale = transform.localScale;
        float timeElapsed = 0;

        while(timeElapsed < time)
        {
            timeElapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, toSize, (float)timeElapsed / (float)time);
            yield return null;
        }

        yield return null;
    }
}
