using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonVisible : PracticalUtilities
{
    [SerializeField] List<GameObject> towerList = new();
    [SerializeField] List<Button> buttons = new List<Button>();
    [SerializeField] List<TextMeshProUGUI> instantiateCostTexts = new List<TextMeshProUGUI>();
    [SerializeField] TextMeshProUGUI upgradeCostTexts;
    [SerializeField] TextMeshProUGUI goldRefundTexts;
    PanelController panelController;

    // Start is called before the first frame update
    void Start()
    {
        panelController = GetComponent<PanelController>(); 
        GetTowerList();
    }

    // Update is called once per frame
    void Update()
    {
        SetUpButtonVisible();
    }

    void GetTowerList()
    {
       towerList = panelController.towerList;
    }

    void SetUpButtonVisible()
    {
        for(int i = 0; i < towerList.Count; i++)
        {   
            UpdateButtonVisibility(i);
            UpdateInstantiateCostText(i);
        }
        
        if(panelController.currentTowerBaseScript != null)
        {
            UpdateUpgradeButtonVisibility();
            UpdateUpgradeCostText();

            UpdateGuardPointButtonVisibility();
        }
        
    }

    void UpdateButtonVisibility(int index)
    {
        // for GreyOutButtonImage   
        if(towerList[index].GetComponentInChildren<TowerBase>().CanInstantiate())
        {
            ResetItemColor(buttons[index].transform);
            ResetItemColor(buttons[index].transform.GetChild(0));
        }
        else
        {
            GreyOutImage(buttons[index].transform);
            GreyOutImage(buttons[index].transform.GetChild(0));
        }
    }

    void UpdateInstantiateCostText(int index)
    {
        instantiateCostTexts[index].text = towerList[index].GetComponentInChildren<TowerBase>().GetGoldInstantiate().ToString();
    }

    void UpdateUpgradeButtonVisibility()
    {
        if(panelController.currentTowerBaseScript.currentLevel == 2)
        {
            buttons[4].gameObject.SetActive(false);
            return;
        }
        else
        {
            buttons[4].gameObject.SetActive(true);
        }

        if(panelController.currentTowerBaseScript.CanUpgrade())
        {
            ResetItemColor(buttons[4].transform);
        }
        else
        {
            GreyOutImage(buttons[4].gameObject.transform);
            GreyOutImage(buttons[4].transform.GetChild(0));
        }
    }

    void UpdateGuardPointButtonVisibility()
    {
        if(panelController.currentBarrack != null)
        {
            buttons[5].gameObject.SetActive(true);
        }else
        {
            buttons[5].gameObject.SetActive(false);
        }
    }

    void UpdateUpgradeCostText()
    {
        upgradeCostTexts.text = panelController.currentTowerBaseScript.GetGoldUpgrade().ToString();
    }
}
