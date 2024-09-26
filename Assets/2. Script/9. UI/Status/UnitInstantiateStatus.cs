using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitInstantiateStatus : MonoBehaviour
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

    public void MovePosition(Transform targetPosition)
    {
        float offsetInXAxis = 6f;
        if(targetPosition.position.x >= 0)
        {
            offsetInXAxis = Mathf.Abs(offsetInXAxis) * -1;
        }
        else
        {
            offsetInXAxis = Mathf.Abs(offsetInXAxis);
        }
        float x = targetPosition.position.x + offsetInXAxis;
        float y = targetPosition.position.y;
        transform.position = new Vector2(x, y);
    }
}
