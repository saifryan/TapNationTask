using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript Instance;
    [Header("Main Menu Data")]
    public GameObject MainMenuPanel;
    public Text MainMenulevelText;
    [Header("Game Play Data")]
    public GameObject GamePlayPanel;
    public Image progressBar;
    public Text GamePlaylevelText;
    [Header("Level Complete Data")]
    public GameObject LevelCompletePanel;
    [Header("Level Failed Data")]
    public GameObject LevelFailedPanel;
    [Header("Fade Panel")]
    public GameObject FadePanelIn;
    public GameObject FadePanelOut;

    public delegate void UpdateProgressBarDelegate(float value);
    public static UpdateProgressBarDelegate updateProgressBarDelegate;
    public enum GameState { MENU, GAME, LEVELCOMPLETE, GAMEOVER }
    public static GameState gameState;

    private void Awake()
    {
        Instance = this;
        FadePanelIn.SetActive(true);
    }


    private void Start()
    {
        SetMainMenuData();
    }

    public static bool IsMainMenu()
    {
        return gameState == GameState.MENU;
    }
    public static bool IsGame()
    {
        return gameState == GameState.GAME;
    }

    public static bool IsLevelComplete()
    {
        return gameState == GameState.LEVELCOMPLETE;
    }

    public static bool IsGameover()
    {
        return gameState == GameState.GAMEOVER;
    }


    public void UpdateProgressBar(float value)
    {
        progressBar.fillAmount = value;
    }


    public void SetMainMenuData()
    {
        MainMenulevelText.text = "Level " + ((PlayerPrefs.GetInt("Level") + 1));
        gameState = GameState.MENU;
        
    }

    public void SetGamePlayData()
    {
        if (IsMainMenu())
        {
            if (CustomAnaltyics.instance != null)
                CustomAnaltyics.instance.GameStartAnaltyics((PlayerPrefs.GetInt("Level") + 1));
            gameState = GameState.GAME;
            MainMenuPanel.SetActive(false);
            GamePlayPanel.SetActive(true);
            GameControllerScript.onGameSet?.Invoke();
            progressBar.fillAmount = 0;
            GamePlaylevelText.text = "Level " + ((PlayerPrefs.GetInt("Level") + 1));
        }
    }

    public void SetLevelCompleteData()
    {
        if (IsGame())
        {
            if (CustomAnaltyics.instance != null)
                CustomAnaltyics.instance.LevelCompleteAnaltyics((PlayerPrefs.GetInt("Level") + 1));
            gameState = GameState.LEVELCOMPLETE;
            GamePlayPanel.SetActive(false);
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.Save();
            //GameControllerScript.onLevelCompleteSet();
            GameControllerScript.Instance.player.enabled = true;
            SoundManagerScript.Instance.LevelCompletPlaySound(SoundManagerScript.Instance.GetAudioSource());
            //StartCoroutine(LevelCompleteTimeDelay());
        }
    }

    public void LevelCompletePanelShow()
    {
        StartCoroutine(LevelCompleteTimeDelay());
    }

    IEnumerator LevelCompleteTimeDelay()
    {
        yield return new WaitForSeconds(2f);
        LevelCompletePanel.SetActive(true);
    }

    public void NextButton()
    {
        FadePanelOut.SetActive(true);
        StartCoroutine(NextButtonTimeDelay());
    }
    IEnumerator NextButtonTimeDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GamePlay");
    }

    public void SetLevelFailedData()
    {
        if (IsGame())
        {
            if (CustomAnaltyics.instance != null)
                CustomAnaltyics.instance.GameOverAnaltyics((PlayerPrefs.GetInt("Level") + 1));
            gameState = GameState.GAMEOVER;
            GamePlayPanel.SetActive(false);
            SoundManagerScript.Instance.LevelFailedPlaySound(SoundManagerScript.Instance.GetAudioSource());
            StartCoroutine(LevelFailedTimeDelay());
        }
    }

    IEnumerator LevelFailedTimeDelay()
    {
        yield return new WaitForSeconds(2f);
        LevelFailedPanel.SetActive(true);
    }


    private void OnEnable()
    {
        updateProgressBarDelegate += UpdateProgressBar;
        GameControllerScript.setLevelCompleteDelegate += SetLevelCompleteData;
        GameControllerScript.setGameoverDelegate += SetLevelFailedData;
    }

    private void OnDisable()
    {
        updateProgressBarDelegate -= UpdateProgressBar;
        GameControllerScript.setLevelCompleteDelegate -= SetLevelCompleteData;
        GameControllerScript.setGameoverDelegate -= SetLevelFailedData;
    }

    private void OnDestroy()
    {
        updateProgressBarDelegate -= UpdateProgressBar;
        GameControllerScript.setLevelCompleteDelegate -= SetLevelCompleteData;
        GameControllerScript.setGameoverDelegate -= SetLevelFailedData;
    }
}
