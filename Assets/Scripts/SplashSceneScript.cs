using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneScript : MonoBehaviour
{
    public float LoadTimeDelay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadGamePlay", LoadTimeDelay);
    }

    void LoadGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
