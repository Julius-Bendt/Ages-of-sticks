using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.UI;
using TMPro;

public class LobbyUI : MonoBehaviour
{

    public UIAnimation anim;
    public GameObject window;
    public TextMeshProUGUI text;
    bool stop = false;
    string format = "WARMUP\n<size=70%>{0} seconds";
    SpawnManager manager;

    void Start()
    {
        manager = FindObjectOfType<SpawnManager>();
        anim.Open("lobby", 1);
    }

    public void LobbyOpenDone(UIAnimation.UIAnimationEvent _event)
    {
        if(_event.identifier == 1)
        anim.Close("controller_text", 1);

        if (_event.identifier == 0)
        {
            text.text = string.Format(format, manager.warmup);
            stop = true;

        }
    }

    private void Update()
    {
        if(stop)
        {
            int int_warmup = (int)manager.warmup;
            text.text = string.Format(format, int_warmup);
        }
    }

    public void AnimDone(UIAnimation.UIAnimationEvent _event)
    {
        if(_event.identifier == 1 && !stop)
        anim.Toggle("controller_text", 1);
    }
}
