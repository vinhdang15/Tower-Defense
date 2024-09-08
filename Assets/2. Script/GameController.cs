using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] GameObject gameOverPanel;
    public static int totalGold;
    public static int lives;
    
    void Start()
    {
        totalGold = 100;
        lives = 10;
        uiController.UpdateGoldText(totalGold);
        uiController.UpdateLivesText(lives);
        gameOverPanel.SetActive(false);
    }

    public static void AddGold(int gold)
    {
        totalGold += gold;
        Instance.uiController.UpdateGoldText(totalGold);
    }

    public static void SpendGold(int gold)
    {
        totalGold -= gold;
        Instance.uiController.UpdateGoldText(totalGold);
    }

    public static void UpdateLives(int i)
    {
        lives += i;
        Instance.uiController.UpdateLivesText(lives);
        Instance.ShowGameOverPanel();
    }

    void ShowGameOverPanel()
    {
        if(lives == 0) gameOverPanel.SetActive(true);
    }

    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }
            return _instance;
        }
    }
}
