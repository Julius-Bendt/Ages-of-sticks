using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.UI;

public class PowerUp : MonoBehaviour
{
    public string animation;
    private UIAnimation anim;
    bool inside;

    private void Start()
    {
        anim = FindObjectOfType<UIAnimation>();
    }

    public void OnTriggerEnter2D(Collider2D o)
    {
        if(o.gameObject.CompareTag("Player"))
        {
            inside = true;
            anim.Open(animation, 0);
        }

    }

    public void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.CompareTag("Player"))
        {
            inside = false;
            anim.Close(animation, 0);
        }
    }
}
