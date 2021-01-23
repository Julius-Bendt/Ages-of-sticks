using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportTo;
    public Transform[] placement;
    public float min = 5, max = 10;

    private void Start()
    {
        StartCoroutine(WaitForNewPlace());
    }
    private void OnTriggerEnter2D(Collider2D o)
    {
        o.transform.position = teleportTo.position - Vector3.right * 2;
    }

    IEnumerator WaitForNewPlace()
    {
        newPlace();

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(min,max));
            newPlace();
            yield return null;
        }
        yield return null;
    }

    void newPlace()
    {
        Vector2 pos = placement[Random.Range(0, placement.Length - 1)].position + Vector3.up*2f + Vector3.right;
        transform.position = pos;
    }
}
