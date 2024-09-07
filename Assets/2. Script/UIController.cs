using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI healthText;

    // [SerializeField] static TextMeshProUGUI goldText;
    // [SerializeField] static TextMeshProUGUI healthText;

    void Awake()
    {
        // goldText = goldTextInstance;
        // healthText = healthTextInstance;
    }
    public void UpdateGoldText(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void UpdateLivesText(int health)
    {
        healthText.text = health.ToString();
    }
}
