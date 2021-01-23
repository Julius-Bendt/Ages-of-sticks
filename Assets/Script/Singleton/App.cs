using UnityEngine;
using System.IO;
using Juto;
using Juto.Audio;
using UnityEngine.SceneManagement;
using Juto.Sceneloader;
using System.Collections.Generic;

public class App : Singleton<App>
{
    // (Optional) Prevent non-singleton constructor use.
    protected App() { }



    public bool isPlaying = false;
    public bool isNewGame = false;

    public int death, killed;

    public Settings settings;
    public ShakeBehavior Shake;
    public string CurrentLevel;

    public AudioDB audioDB;

    private static bool doneDontDestroy = false;

    [Header("Spawn settings")]
    public int players = 0;
    public GameObject player;
    public Color[] colors;
    private LevelManager level;
    public List<Stats> playerStats = new List<Stats>();

    [Header("Level settings")]
    public LevelName[] levels;
    private int levelIndex = -2, mapIndex = 0;
    private LevelName currentLevel;
    private int playerUpgradeIndex = 0;
    public int spawnPlayers = 0;

    private GameUI gameUI;

    [System.Serializable]
    public struct LevelName
    {
        public string mapPrefix;
        public int maps;
    }

    public bool DebugMode = true;

    public bool playable = true;
    public MapNameUI mapShower;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Loaded scene: " + scene.name);
        if (!doneDontDestroy)
        {
            //DontDestroyOnLoad(gameObject);
            doneDontDestroy = true;
        }

        if (Shake == null)
            Shake = FindObjectOfType<ShakeBehavior>();

        level = FindObjectOfType<LevelManager>();

        if(level)
        {
            if(scene.buildIndex > 2)
                level.SpawnPlayers();
        }

        gameUI = FindObjectOfType<GameUI>();


        gameUI.HideStats();
        if (scene.buildIndex > 3 && scene.name != "leaderboard")
        {
            gameUI.UpdateScore();
            string[] sceneNameString = scene.name.Split('_');

            string name = "";
            int index = int.Parse(sceneNameString[sceneNameString.Length - 1]) + 1;

            for (int i = 0; i < sceneNameString.Length; i++)
            {
                if (i < sceneNameString.Length - 1)
                    name += sceneNameString[i];
            }


            mapShower.ShowName(name, index);
        }

        if (scene.name == "leaderboard")
            gameUI.HideStats();


    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void NextLevel()
    {
        if (levelIndex == -2) //currently in lobby
        {
            levelIndex = -1;
            SceneManager.LoadScene("powerup");
            return;
            
        } 
        else if (levelIndex == -1) //currently in upgrades
        {
            levelIndex = 0;
            currentLevel = levels[0];
            return;
        }

        PlayerController winner = FindObjectOfType<PlayerController>();

        foreach (Stats s in playerStats)
        {
            if(s.controllerId == winner.controllerId)
            {
                s.won++;
            }
        }

        if(mapIndex >= currentLevel.maps)
        {
            levelIndex++;
            if(levelIndex < levels.Length)
            {
                currentLevel = levels[levelIndex];
                mapIndex = 0;
            }
            else
            {
                SceneManager.LoadScene("leaderboard");
                return;
            }

        }

        string mapToLoad = currentLevel.mapPrefix + "_" + mapIndex;

        mapIndex++;

        SceneManager.LoadScene(mapToLoad);
    }


}
