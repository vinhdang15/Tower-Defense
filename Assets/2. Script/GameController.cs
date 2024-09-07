using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] UIController uiController;
    public static int totalGold = 100;
    public static int lives = 10;
    
    void Start()
    {
        uiController.UpdateGoldText(totalGold);
        uiController.UpdateLivesText(lives);
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
