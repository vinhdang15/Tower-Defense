using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] PanelController panelController;
    GameObject currentTower;
    Vector3 mousePos;
    public Vector2 worldPos =new Vector2();
    RaycastHit2D hit;

    void Start()
    {
        panelController = GetComponent<PanelController>();
    }

    void Update()
    {
        GetUserInput();
    }

    void GetUserInput()
    {
        if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            mousePos = Input.GetMouseButtonUp(0)? Input.mousePosition : Input.GetTouch(0).position;
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            panelController.worldPos = worldPos;
                     
            // if(panelController.currentBarrack == null)
            // {
            //     hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("BuildingSpot", "TowerBody", "BarrackBody", "Button"));
            // }
            // else if(panelController.currentBarrack != null)
            // {
            //     hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("BarrackRange"));
            //     Debug.Log("check");
            // }
            if(panelController.isShowPanel == false)
            {
                hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("BuildingSpot", "TowerBody", "BarrackBody", "BarrackRange", "Button"));
            }
            else
            {
                hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Button"));
            }
            

            CheckShowPanel();
            CheckShowTowerColliderRange();
            
            if(hit.collider == null)
            {
                //Debug.Log("No hit");
            }
            else
            {
                //Debug.Log(hit.collider.name);
            }
        }
    }   

    void CheckShowPanel()
    {
        panelController.GetHitInFor(hit);
        if(hit.collider == null) return;
        if(hit.collider.gameObject.layer != LayerMask.NameToLayer("Button") 
            && hit.collider.gameObject.layer != LayerMask.NameToLayer("BarrackRange"))
        {
            if(hit.collider != null) panelController.ShowCurrentPanel();
            else panelController.HideCurrentPanel();
        }
    }

    void CheckShowTowerColliderRange()
    {
        if (hit.collider == null)
        {  
            if(currentTower != null)
            {
                panelController.HideCurrentLevleDetection();
            }
            return;
        }

        int layer = hit.collider.gameObject.layer;
        if(layer == LayerMask.NameToLayer("TowerBody"))
        {
            // turn off the indicator of the previous tower and show the new one
            if(currentTower != null)
            {
                panelController.HideCurrentLevleDetection();
            }
            currentTower = hit.collider.transform.parent.gameObject;
            panelController.ShowCurrentLevelDetection();
        }
        else if (layer == LayerMask.NameToLayer("Button") 
                || layer == LayerMask.NameToLayer("BarrackBody"))
        {
            // Do nothing
        }
    }
}
