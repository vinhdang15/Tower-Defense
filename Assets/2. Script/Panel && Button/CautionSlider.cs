using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CautionSlider : MonoBehaviour
{
    Image cautionFill;
    public float timeLimit;
    float timeRemain;
    float timeStart;
    public bool isStartFirstWave;

    void Awake()
    {
        timeLimit = 6f;
        cautionFill = transform.GetChild(1).GetComponent<Image>();
    }

    void Start()
    {
        timeLimit = 6f;
    }

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
