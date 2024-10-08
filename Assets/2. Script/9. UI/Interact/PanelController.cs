using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelController : PracticalUtilities
{
    [Header("Tower && Detection Prefabs")]
    public List<GameObject> towerList = new List<GameObject>();
    [SerializeField] Transform instantiatePanel;
    [SerializeField] Transform upgradePanel;
    [SerializeField] UnitInstantiateStatus instantiateStatusPanel;
    [SerializeField] UnitCurrentStatus currentStatusPanel;
    [SerializeField] Transform nextLevelDetection;
    [SerializeField] Transform currentLevelDetection;
    [SerializeField] Transform towerHolder;

    public Transform currentBarrack;
    public Vector2 worldPos;

    [SerializeField] GoldRefund goldRefundBox;

    [Header("Panel Animation")]
    [SerializeField] float duration = 0.5f;
    Vector3 startScale = new Vector3(0, 0, 0);
    Vector3 endScale = new Vector3(1, 1, 0);

    // ================== Internal state variables
    // using spriteBoundInX this to get the size of the sprite of the CurrentLevelDetection and NextLevelDetection
    float spriteBoundInX;
    Transform currenBuildingSpot;
    Transform currentPanelTrans;
    Transform currentBodyTowerTrans;
    public TowerBase currentTowerBaseScript;
    Button currentButton;
    Vector2 position;
    Coroutine scaleUpCoroutine;
    public bool isShowPanel;

    public delegate void TowerAction();
    public delegate void TryTowerAction();

    // ======= AUDIO =======
    [SerializeField] SoundEffectSO soundEffectSO;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetUpIndicatorAndPanel();
    }

    void Update()
    {
        //Debug.Log(currentPanelTrans.name);
    }

    #region RAYCAST HIT INFORMATION PROCESSING
    public void GetHitInFor(RaycastHit2D hit)
    {
        if(hit.collider == null)
        {           
           HandleRaycastHitNull();
           return;
        }
        
        if(hit.collider.gameObject.layer == LayerMask.NameToLayer("BuildingSpot"))
        {
            HandleBuildingSpotHit(hit);          
        }
        else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("TowerBody"))
        {
            HandleTowerBodyHit(hit);
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("BarrackBody"))
        {
            HandleBarrackBodyHit(hit);
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("BarrackRange"))
        {
            HandleBarrackRangeHit(hit);
        }

        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.clickSound);
    }

    public void HandleRaycastHitNull()
    {
        ResetCurrentSelected();
        HideCurrentLevleDetection();
        HideNextlevelDetection();
        HideCurrentPanel();
        HideInstantiateStatusPanel();
        HideCurrentStatusPanel();
    }

    void HandleBuildingSpotHit(RaycastHit2D hit)
    {
        ResetCurrentSelected();
        HideCurrentLevleDetection();
        HideNextlevelDetection();
        HideInstantiateStatusPanel();
        HideCurrentStatusPanel();
        currenBuildingSpot = hit.collider.transform;
        position = hit.collider.transform.position;

        if (currentPanelTrans != null) HideCurrentPanel();

        currentPanelTrans = instantiatePanel;
        currentPanelTrans.position = hit.collider.transform.position;
    }

    void HandleTowerBodyHit(RaycastHit2D hit)
    {
        position = hit.collider.transform.parent.parent.position;
        currentTowerBaseScript = hit.collider.transform.parent.GetComponent<TowerBase>();
        currentBodyTowerTrans = hit.collider.transform;         
        currentLevelDetection.position = currentBodyTowerTrans.position; 
        
        currentPanelTrans = upgradePanel;
        currentPanelTrans.position = position;
        currentStatusPanel.SetCurrentLevelStatusText(currentTowerBaseScript);
        ShowCurrentStatusPanel();
    }

    void HandleBarrackBodyHit(RaycastHit2D hit)
    {
        HandleTowerBodyHit(hit);
        currentBarrack = hit.collider.transform.parent;
        currentTowerBaseScript = hit.collider.transform.parent.GetComponent<TowerBase>();
    }

    void HandleBarrackRangeHit(RaycastHit2D hit)
    {
        Barrack barrackScript = currentBarrack.GetComponent<Barrack>();
        HideCurrentLevleDetection();
        HideInstantiateStatusPanel();
        barrackScript.barrackRange.gameObject.SetActive(false);
        barrackScript.UpdateGuardPointPos(worldPos);
        barrackScript.SetSoldierGuardPoint();
    }

    #endregion
    
    #region PANEL AND INDICATOR VISIBILITY SETUP
    void SetUpIndicatorAndPanel()
    {
        spriteBoundInX =  nextLevelDetection.GetComponent<SpriteRenderer>().bounds.size.x/2;
        HideCurrentLevleDetection();
        HideNextlevelDetection();
        HideInstantiateStatusPanel();
        HideCurrentStatusPanel();
        HideObjectCoroutine(scaleUpCoroutine, instantiatePanel.transform, startScale);
        HideObjectCoroutine(scaleUpCoroutine, upgradePanel.transform, startScale);
    }
    #endregion

    #region ON BUTTON CLICK
    // =============== ON BUTTON CLICK
    public void OnButtonClick(Button clickedButton, TowerAction towerAction, TryTowerAction tryTowerAction = null)
    {
        if (currentButton == clickedButton)
        {
            towerAction();
            HideNextlevelDetection();
            HideCurrentLevleDetection();
            HideInstantiateStatusPanel();
            HideCurrentStatusPanel();
        }
        else
        {
            if (currentButton != null) HideButtonTick(currentButton);
            ShowButtonTick(clickedButton);
            HideGoldRefundBox();
            tryTowerAction();          
        }
        currentButton = clickedButton;
    }
    #endregion

    #region INSTANTIATE TOWER
    public void InstantiateArrowTower()
    {
        InstantiateTower(0);
    }

    public void InstantiateMagicTower()
    { 
        InstantiateTower(1);      
    }
    
    public void InstantiateCannonTower()
    {
        InstantiateTower(2);
    }

    public void InstantiateBarrack()
    {
        InstantiateTower(3);
    }

    void InstantiateTower(int index)
    {
        TowerBase towerBaseScript = towerList[index].GetComponentInChildren<TowerBase>();
        if(towerBaseScript.CanInstantiate())
        {
            GameObject tower = Instantiate(towerList[index], position, Quaternion.identity, towerHolder);
            tower.transform.GetComponentInChildren<TowerBase>().buildingSpotList.Add(currenBuildingSpot.gameObject);  
            AudioManager.Instance.PlaySound(audioSource, soundEffectSO.BuildSound);
            
            HideCurrentPanel();
            HideButtonTick(currentButton);
            HideBuildingSpot();
        }
    }
    #endregion

    #region BUTTON EVENT - INSTANTIATE TOWER
    public void StartInstantiateArrowTower(Button clickedButton)
    {
        TowerBase towerBaseScript = towerList[0].GetComponentInChildren<TowerBase>();
        instantiateStatusPanel.SetCurrentLevelStatusText(towerBaseScript);
        ShowInstantiateStatusPanel();

        OnButtonClick(clickedButton, InstantiateArrowTower, TryUpgradeTower);
    }

    public void StartInstantiateMageTower(Button clickedButton)
    {
        TowerBase towerBaseScript = towerList[1].GetComponentInChildren<TowerBase>();
        instantiateStatusPanel.SetCurrentLevelStatusText(towerBaseScript);
        ShowInstantiateStatusPanel();

        OnButtonClick(clickedButton, InstantiateMagicTower, TryUpgradeTower);
    }

    public void StartInstantiateCannonTower(Button clickedButton)
    {
        TowerBase towerBaseScript = towerList[2].GetComponentInChildren<TowerBase>();
        instantiateStatusPanel.SetCurrentLevelStatusText(towerBaseScript);
        ShowInstantiateStatusPanel();

        OnButtonClick(clickedButton, InstantiateCannonTower, TryUpgradeTower);
    }

    public void StartInstantiateBarrack(Button clickedButton)
    {
        TowerBase towerBaseScript = towerList[3].GetComponentInChildren<TowerBase>();
        instantiateStatusPanel.SetCurrentLevelStatusText(towerBaseScript);
        ShowInstantiateStatusPanel();

        OnButtonClick(clickedButton, InstantiateBarrack, TryUpgradeTower);
    }
    #endregion

    #region BUTTON EVENT - SET GUARD POINT
    public void SetGuardPoint()
    {
        HideCurrentPanel();
        HideInstantiateStatusPanel();
        HideCurrentStatusPanel();
        ShowCurrentLevelDetection();
        // delay in 0.2f to avoid click
        Invoke(nameof(ActiveBarrackRange), 0.2f);
    }

    void ActiveBarrackRange()
    {
        currentBarrack.GetComponent<Barrack>().barrackRange.gameObject.SetActive(true);
    }
    #endregion

    #region BUTTON EVENT - UPGRADE TOWER
    public void StartUpgradeTower(Button clickedButton)
    {
        instantiateStatusPanel.SetNextLevelStatusText(currentTowerBaseScript);
        ShowInstantiateStatusPanel();
        
        OnButtonClick(clickedButton, UpgradeTower, TryUpgradeTower);
    }

    void UpgradeTower()
    {
        if(currentBodyTowerTrans == null) return;
        currentTowerBaseScript.TryUpgrade();
        HideCurrentPanel();
    }

    public void TryUpgradeTower()
    {
        ShowNextlevelDetection();
    }
    #endregion

    #region BUTTON EVENT - SELL TOWER
    public void StartSellTower(Button clickedButton)
    {
        OnButtonClick(clickedButton, SellTower, TrySellTower);
    }

    void SellTower()
    {
        HideCurrentLevleDetection();
        HideCurrentPanel();
        ShowBuildingSpot();
        
        GameController.Instance.AddGold(currentTowerBaseScript.GetGoldRefund());
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.AddGoldSound);
        Destroy(currentTowerBaseScript.gameObject);
        
    }

    void TrySellTower()
    {
        ShowAndUpdateGoldRefundBox();   
    }
    #endregion

    #region HELPER FUNCTION
    // ================== VISIBILITY CURRENT BUILDING SPOT
    void ShowBuildingSpot()
    {
        currentTowerBaseScript.buildingSpotList[0].transform.localScale = endScale;
    }
    void HideBuildingSpot()
    {
        currenBuildingSpot.localScale = startScale;
    }

    // ================== VISIBILITY CURRENT PANEL
    public void ShowCurrentPanel()
    {
        if(currentPanelTrans != null)
        {
            ShowObjectCoroutine(scaleUpCoroutine, currentPanelTrans, startScale, endScale, duration);
            if(currentButton != null) HideButtonTick(currentButton);
            ResetCurrentButton();
        }
        isShowPanel = true;
    }

    public void HideCurrentPanel()
    {
        if(currentPanelTrans == null) return;
        HideGoldRefundBox();
        HideObjectCoroutine(scaleUpCoroutine, currentPanelTrans, startScale);
        isShowPanel = false;
    }

    // ================== VISIBILITY INSTANTIATE STATUS PANEL
    void ShowInstantiateStatusPanel()
    {
        SetInstantiateStatusPanelPosition();
        instantiateStatusPanel.gameObject.SetActive(true);
    }

    void SetInstantiateStatusPanelPosition()
    {
        instantiateStatusPanel.MovePosition(currentPanelTrans);
    }

    void HideInstantiateStatusPanel()
    {
        instantiateStatusPanel.gameObject.SetActive(false);
    }

    void ShowCurrentStatusPanel()
    {
        currentStatusPanel.gameObject.SetActive(true);
    }
    void HideCurrentStatusPanel()
    {
        currentStatusPanel.gameObject.SetActive(false);
    }

    // ================== VISIBILITY BUTTON TICK
    public void ShowButtonTick(Button button)
    {
        if(button.transform.childCount == 0) return;
        button.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void HideButtonTick(Button button)
    {
        if(button.transform.childCount == 0) return;
        button.transform.GetChild(0).gameObject.SetActive(false);
    }
    
    // ================== VISIBILITY DETECTION
    public void ShowCurrentLevelDetection()
    {
        currentLevelDetection.gameObject.SetActive(true);
        SetSprtieIndicator(currentLevelDetection, currentTowerBaseScript.GetTowerCurentLevelRange(), spriteBoundInX);
    }

    public void HideCurrentLevleDetection()
    {
        currentLevelDetection.gameObject.SetActive(false);
    }

    void ShowNextlevelDetection()
    {
        if(currentTowerBaseScript == null) return;
        nextLevelDetection.gameObject.SetActive(true);
        nextLevelDetection.position = currentBodyTowerTrans != null ? currentBodyTowerTrans.position : position;
        SetSprtieIndicator(nextLevelDetection, currentTowerBaseScript.GetTowerNextLevelRange(), spriteBoundInX);
    }

    void HideNextlevelDetection()
    {
        nextLevelDetection.gameObject.SetActive(false);
    }

    // ================== VISIBILITY GOLD REFUND BOX
    void ShowAndUpdateGoldRefundBox()
    {
        string goldText = currentTowerBaseScript.GetGoldRefund().ToString();
        goldRefundBox.UpdateGoldRefundText(goldText);
        goldRefundBox.gameObject.SetActive(true);
    }

    void HideGoldRefundBox()
    {
        goldRefundBox.gameObject.SetActive(false);
    }

    // ================== RESET CURRENT BUTTON AND TOWER
    void ResetCurrentButton()
    {
        currentButton = null;
    }
    void ResetCurrentSelected()
    {
        currentTowerBaseScript = null;
        if(currentBarrack != null) currentBarrack.GetComponent<Barrack>().barrackRange.gameObject.SetActive(false);
        currentBarrack = null;
    }
    #endregion
}
