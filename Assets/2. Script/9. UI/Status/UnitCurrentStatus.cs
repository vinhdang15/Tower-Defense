using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitCurrentStatus : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI rate;
    // Start is called before the first frame update

    public void SetCurrentLevelStatusText(TowerBase tower)
    {
        int currentLevel = tower.currentLevel;
        rate.text = tower.spawnRate[currentLevel].ToString() + "s";
        damage.text = tower.GetCurrentDamage(currentLevel).ToString();
    }
}
