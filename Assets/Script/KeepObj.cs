using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObj : MonoBehaviour
{
    private static KeepObj _this;
    void Start()
    {
        if (_this == null)
        {
            _this = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
