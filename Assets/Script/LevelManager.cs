using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions as a level specific singleton.
/// </summary>
public class LevelManager : MonoBehaviour
{
    private float timer;

    public float spawnTime, delayTime;
    private bool firstTimeSpawned = true;

    [SerializeField]
    private Transform[] spawnPos, gunSpawnPos;

    public Weapon[] weapons;
    public void SpawnPlayers()
    {
        int toBeCreated = App.Instance.players;
        while (toBeCreated > 0)
        {
            GameObject g = Instantiate(App.Instance.player, spawnPos[toBeCreated-1].position, Quaternion.identity);
            g.GetComponent<PlayerController>().Init(toBeCreated-1, App.Instance.colors[toBeCreated-1]);
            toBeCreated--;
            App.Instance.spawnPlayers++;
        }
    }

    public void Update()
    {
        if(FindObjectsOfType<PlayerController>().Length == 1 && App.Instance.spawnPlayers >= App.Instance.playerStats.Count)
        {
            App.Instance.NextLevel();
        }

        if (App.Instance.DebugMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                App.Instance.NextLevel();
            }
        }

        timer += Time.deltaTime;

        if(timer >= spawnTime ||(firstTimeSpawned && timer >= delayTime))
        {
            firstTimeSpawned = false;
            timer = 0;

            if(gunSpawnPos != null)
            {
                //Spawn et våben ved hver
                foreach (Transform t in gunSpawnPos)
                {
                    Weapon w = weapons[Random.Range(0, weapons.Length)];


                    if (w.gun == null)
                        Debug.Log(w.name + " doesnt have a gun.");

                    GameObject g = Instantiate(w.gun, t.position, t.rotation);
                    g.transform.localScale = w.GetScale();
                    g.AddComponent<PickUpWeapon>().weapon = w;
                    g.AddComponent<Rigidbody2D>();
                    g.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,1),Random.Range(-1,1)) * 100);
                    g.AddComponent<BoxCollider2D>();
                }
            }
        }
    }
}
