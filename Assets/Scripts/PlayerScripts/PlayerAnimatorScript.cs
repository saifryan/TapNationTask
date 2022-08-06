using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Transform runnersParent;

    private void Awake()
    {
        GameControllerScript.onGameSet += StartRunning;
        GameControllerScript.onLevelCompleteSet += StopRunning;
        GameControllerScript.onMenuSet += StopRunning;
        GameControllerScript.onGameoverSet += StopRunning;
    }

    private void OnDestroy()
    {
        GameControllerScript.onGameSet -= StartRunning;
        GameControllerScript.onLevelCompleteSet -= StopRunning;
        GameControllerScript.onMenuSet -= StopRunning;
        GameControllerScript.onGameoverSet -= StopRunning;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartRunning()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            PlayerRunnerScript runner = runnersParent.GetChild(i).GetComponent<PlayerRunnerScript>();
            runner.StartRunning();
        }
    }

    private void StopRunning(int none = 0)
    {
        StopRunning();
    }

    private void StopRunning()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            PlayerRunnerScript runner = runnersParent.GetChild(i).GetComponent<PlayerRunnerScript>();
            runner.StopRunning();
        }
    }

    
}
