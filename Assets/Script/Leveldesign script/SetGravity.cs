using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGravity : MonoBehaviour
{
    [Tooltip("Default: (0, -9.81f)")]
    public Vector2 gravity = new Vector2(0, -9.81f);
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = gravity;
    }

}
