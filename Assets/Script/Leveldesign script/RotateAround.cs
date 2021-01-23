using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float speed = 30, delta;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target.position, delta *Time.deltaTime);
    }
}
