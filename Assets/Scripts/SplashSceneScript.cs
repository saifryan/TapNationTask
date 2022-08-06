using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadGamePlay", 3f);
    }

    void LoadGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
