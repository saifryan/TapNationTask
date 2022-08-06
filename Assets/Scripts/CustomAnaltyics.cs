using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;


public class CustomAnaltyics : MonoBehaviour
{
    public static CustomAnaltyics instance = null;

    private void Awake()
    {
        if (instance != null)
        {

            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void GameStartAnaltyics(int level)
    {
        string message = "level_";
        message = message + level + "_start_";
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, message);
    }
    public void LevelCompleteAnaltyics(int level)
    {
        string message = "level_";
        message = message + level + "_complete_";
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, message);
        //Debug.LogWarning("Message  :::  " + message);
    }
    public void GameOverAnaltyics(int level)
    {
        string message = "level_";
        message = message + level + "_failed_";
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, message);
    }
    
}