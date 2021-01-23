using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public float damage;
    public float damageTime;
    private List<PlayerController> players = new List<PlayerController>();

    private float _time;

    private void OnCollisionEnter2D(Collision2D o)
    {
        PlayerController p = o.gameObject.GetComponent<PlayerController>();

        if(p)
        {
            players.Add(p);
        }
    }

    private void OnCollisionExit2D(Collision2D o)
    {
        PlayerController p = o.gameObject.GetComponent<PlayerController>();
        if (p)
        {
            players.Remove(p);
        }
    }

    private void OnTriggerEnter2D(Collider2D o)
    {
        PlayerController p = o.gameObject.GetComponent<PlayerController>();

        if (p)
        {
            players.Add(p);
        }
    }

    private void OnTriggerExit2D(Collider2D o)
    {
        PlayerController p = o.gameObject.GetComponent<PlayerController>();
        if (p)
        {
            players.Remove(p);
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if(_time >= damageTime)
        {
            _time = 0;
            foreach (PlayerController player in players)
            {
                player.TakeDamage(damage,Vector2.right);
            }
        }
    }
}
