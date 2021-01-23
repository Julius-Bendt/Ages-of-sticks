using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.Sceneloader;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPos;
    public LobbyUI lobbyUI;
    public string DebugLoad = "stoneage_1";

    private bool lobbyUIClosed = false;
    public string connected = "";

    public float warmup = 20;

    // Update is called once per frame
    void Update()
    {
        if (App.Instance.players < 5)
        {
            for (int j = 0; j < 5; j++)
            {
                if (connected.Contains(j.ToString()))
                    continue;

                bool instansiate = false;
                for (int i = 0; i < 20; i++) //for hver knap på controlleren
                {
                    if (Input.GetKeyDown("joystick " + (j+1) + " button " + i))
                    {
                        Debug.Log("joystick " + (j + 1) + " button " + i);
                        instansiate = true;
                        connected += j;
                    }
                }

                if (instansiate)
                {
                    App.Instance.players++;
                    Stats s = new Stats();
                    s.controllerId = j+1;
                    s.c = App.Instance.colors[App.Instance.players - 1];
                    App.Instance.playerStats.Add(s);

                    GameObject g = Instantiate(App.Instance.player, spawnPos.position, Quaternion.identity);
                    g.GetComponent<PlayerController>().Init(App.Instance.players-1, App.Instance.colors[App.Instance.players-1]);

                    if(!lobbyUIClosed)
                    {
                        lobbyUIClosed = true;
                        lobbyUI.anim.Close("lobby",0);
                    }
                    continue;
                }
            }
        }

        if (App.Instance.playerStats.Count > 1) //&& FindObjectsOfType<PlayerController>().Length <= 1
        {
            warmup -= Time.deltaTime;
        }
            
        if(warmup <= 0)
            App.Instance.NextLevel();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SceneLoader.LoadScene(DebugLoad);
            App.Instance.NextLevel();
        }
    }

    public void PlayerGotKilled(int player, int controller)
    {
        Debug.Log($"Player: {player}, controller: {controller}");
        GameObject g = Instantiate(App.Instance.player, spawnPos.position, Quaternion.identity);
        g.GetComponent<PlayerController>().Init(player, App.Instance.colors[player]);
    }
}
