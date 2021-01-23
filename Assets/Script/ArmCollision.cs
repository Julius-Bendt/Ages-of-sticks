using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D o)
    {
        PlayerController p = o.GetComponent<PlayerController>();

        if(p)
        {
            if (p != GetComponentInParent<PlayerController>())
                p.TakeDamage(5,Vector3.one);
        }
    }
}
