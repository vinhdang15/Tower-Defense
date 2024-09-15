using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] UIController uiController;
    [SerializeField] GameObject gameOverPanel;
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
        totalGold = initialGold;
        lives = initialLive;
        uiController.UpdateGoldText(totalGold);
        uiController.UpdateLivesText(lives);
        gameOverPanel.SetActive(false);
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
        ShowGameOverPanel();
    }

    public void UpdateTotalWave(int total)
    {
        totalWave = total;
        uiController.UpdateTotalWaveText(totalWave);
    }

    public void UpdateCurrentWave(int current)
    {
        currentWave = current;
        uiController.UpdateCurrentWaveText(currentWave);
    }

    void ShowGameOverPanel()
    {
        if(lives == 0) gameOverPanel.SetActive(true);
    }
}
