using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI rate;
    // Start is called before the first frame update

    public void SetCurrentLevelStatusText(TowerBase tower)
    {
        int currentLevel = tower.currentLevel;
        description.text = tower.description[currentLevel].ToString();
        rate.text = tower.spawnRate[currentLevel].ToString() + "s";
        damage.text = tower.GetCurrentDamage(currentLevel).ToString();
    }

    public void SetNextLevelStatusText(TowerBase tower)
    {
        if(tower.currentLevel == 2) return;
        int currentLevel = tower.currentLevel;
        description.text = tower.description[currentLevel + 1].ToString();
        rate.text = tower.spawnRate[currentLevel + 1].ToString() + "s";
        damage.text = tower.GetCurrentDamage(currentLevel + 1).ToString();
    }
}
