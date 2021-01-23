using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float min, max;
    public float speed;
    public int startDir;
    private int dir;
    public int stat;

    private PowerUpManager manager;

    public void Start()
    {
        dir = startDir;

        float y = (dir == 1) ? min : max;
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(y, min, max), 0);

        manager = FindObjectOfType<PowerUpManager>();
    }

    public void Update()
    {
        float y = speed * dir * Time.deltaTime;

        if (transform.localPosition.y >= max)
            dir = -1;
        else if (transform.localPosition.y <= min)
            dir = 1;

        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y + y, min, max), 0);

    }

    private void OnTriggerEnter2D(Collider2D o)
    {
        if(stat > -1)
        {
            if (o.CompareTag("bullet"))
            {
                int sender = o.GetComponent<Bullet>().statSender;
                Debug.Log("Sender: " + sender + " Stat id: " + stat);

                manager.BoughtUpgrade(sender, stat);
                Destroy(o.gameObject);

            }
        }
    }
}
