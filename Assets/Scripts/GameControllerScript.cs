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
    public int MaxLevel = 5;
    //public GameObject[] Levels;
    int currentlevel;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentlevel = PlayerPrefs.GetInt("Level");
        if(currentlevel >= MaxLevel)
        {
            currentlevel = Random.Range(0, MaxLevel);
        }
        //Debug.Log("Level   : " + currentlevel);
        GameObject level = Resources.Load<GameObject>("Levels/Level_" + (currentlevel + 1));
        level = Instantiate(level);
        level.SetActive(true);
    }

}
