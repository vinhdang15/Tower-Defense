using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] GameStatus uiController;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] int totalWave;
    [SerializeField] int currentWave;
    [SerializeField] int initialGold = 100;
    [SerializeField] int initialLive = 10;
    [HideInInspector] public int totalGold;
    [HideInInspector] public int lives;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResumingGame();
        totalGold = initialGold;
        lives = initialLive;
        uiController.UpdateGoldText(totalGold);
        uiController.UpdateLivesText(lives);
        HideAllPanel();
    }

    public void AddGold(int gold)
    {
        totalGold += gold;
        uiController.UpdateGoldText(totalGold);
    }

    public void SpendGold(int gold)
    {
        totalGold -= gold;
        uiController.UpdateGoldText(totalGold);
    }

    public void UpdateLives(int i)
    {
        lives += i;
        uiController.UpdateLivesText(lives);
        if(lives == 0)
        {
            PauseGame();
            ShowGameOverMenu();
        }
    }

    public void UpdateTotalWaveText(int total)
    {
        totalWave = total;
        uiController.UpdateTotalWaveText(totalWave);
    }

    public void UpdateCurrentWaveText(int current)
    {
        currentWave = current;
        uiController.UpdateCurrentWaveText(currentWave);
    }

    void ShowGameOverMenu()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowVictoryMenu()
    {
        victoryPanel.SetActive(true);
    }
 
    public void ShowPauseMenu()
    {
        pausePanel.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pausePanel.SetActive(false);
    }

    void HideAllPanel()
    {
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumingGame()
    {
        Time.timeScale = 1;
    }
}
