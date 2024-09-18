using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI totalWaveText;
    [SerializeField] TextMeshProUGUI currentWaveText;

    void Awake()
    {

    }
    public void UpdateTotalWaveText(int total)
    {
        totalWaveText.text = "OF " + total.ToString();
    }

    public void UpdateCurrentWaveText(int current)
    {
        currentWaveText.text = current.ToString();
    }

    public void UpdateGoldText(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void UpdateLivesText(int health)
    {
        livesText.text = health.ToString();
    }
}
