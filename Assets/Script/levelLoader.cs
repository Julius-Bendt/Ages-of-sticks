using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.Sceneloader;

public class levelLoader : MonoBehaviour
{
    public string load;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name, gameObject);
        SceneLoader.LoadScene(load);
    }

   
}
