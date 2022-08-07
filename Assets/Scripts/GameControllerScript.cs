using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript Instance;
    public delegate void SetGameoverDelegate();
    public static SetGameoverDelegate setGameoverDelegate;

    public delegate void OnGameSet();
    public static OnGameSet onGameSet;

    public delegate void OnLevelCompleteSet();
    public static OnLevelCompleteSet onLevelCompleteSet;

    public delegate void SetLevelCompleteDelegate();
    public static SetLevelCompleteDelegate setLevelCompleteDelegate;

    public delegate void OnMenuSet();
    public static OnMenuSet onMenuSet;

    public delegate void OnGameoverSet();
    public static OnGameoverSet onGameoverSet;

    public PlayerEndSceneMovementScript player;
    public GameObject[] Levels;
    int currentlevel;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentlevel = PlayerPrefs.GetInt("Level");
        if(currentlevel >= Levels.Length)
        {
            currentlevel = Random.Range(0, Levels.Length);
        }
        //Debug.Log("Level   : " + currentlevel);
        GameObject level = Instantiate(Levels[currentlevel]);
        level.SetActive(true);
    }

}
