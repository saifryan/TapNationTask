using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataScript : MonoBehaviour
{
    public static LevelDataScript Instance;
    public Transform EndPoint;

    private void Awake()
    {
        Instance = this;
    }
}
