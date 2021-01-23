using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidRotate : MonoBehaviour
{
    Rigidbody2D rig;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.AddTorque(speed);
    }
}
