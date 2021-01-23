using Juto.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SpriteShatter; <- paid asset
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    [HideInInspector]
    public float damage;
    public bool piercing = false;
    Rigidbody2D rig;

    public string senderName;
    public int statSender;

    const float ALIVETIME = 2.5f;

    public LayerMask destroy;
   //private Shatter shatter; <- paid asset

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //shatter = GetComponent<Shatter>(); <- paid asset
        Destroy(gameObject, ALIVETIME);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.MovePosition(rig.position + new Vector2(transform.right.x, transform.right.y) * speed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D o)
    {
        if (((1 << o.gameObject.layer) & destroy) != 0)
        {
            //shatter.shatter(); <- paid asset
            Destroy(gameObject,3);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D o)
    {
        PlayerController pc = o.gameObject.GetComponent<PlayerController>();
        if (pc)
        {
            if(pc.gameObject.name != senderName)
            {
                Vector2 dir = transform.position - o.transform.position;
                pc.TakeDamage(damage, dir.normalized);
                Destroy(gameObject);
            }
        }
    }

}
