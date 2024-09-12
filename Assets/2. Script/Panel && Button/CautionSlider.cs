using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CautionSlider : MonoBehaviour
{
    public float timeLimit;
    public bool isStartFirstWave;
    Image cautionFill;
    float timeRemain;
    float timeStart;
    
    void Awake()
    {
        timeLimit = 6f;
        cautionFill = transform.GetChild(1).GetComponent<Image>();
    }

    // void Start()
    // {
    //     timeLimit = 6f;
    // }

    IEnumerator UpdateCautionFill()
    {
        timeRemain = timeLimit;
        timeStart = Time.deltaTime;
        while(timeRemain >=0)
        {
            timeStart += Time.deltaTime;
            timeRemain = timeLimit - timeStart;

            cautionFill.fillAmount = (timeLimit - timeRemain)/timeLimit;
            yield return new WaitForFixedUpdate();
        }
            StopCaution();
    }

    public void StopCaution()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void StartUpCaution()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        StartCoroutine(UpdateCautionFill());
    }
}
