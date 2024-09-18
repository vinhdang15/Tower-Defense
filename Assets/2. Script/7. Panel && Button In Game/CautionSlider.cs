using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CautionSlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldToAddText;
    Color color;
    public float timeLimit;
    public bool isStartFirstWave;
    public int goldToAdd;
    float timeRemain;
    float timeStart;
    Image cautionFill;

    // ======= SOUND =======
    [HideInInspector] public AudioSource audioSource;
    public SoundEffectSO soundEffectSO;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetGoldToAddTextColor();
        timeLimit = 6f;
        cautionFill = transform.GetChild(1).GetComponent<Image>();
    }

    public void StopCaution()
    {
        foreach(Transform child in transform)
        {
            timeRemain = 0;
            if(!child.name.Contains("Text (TMP) - GoldToAdd")) child.gameObject.SetActive(false);
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

    IEnumerator UpdateCautionFill()
    {
        timeRemain = timeLimit;
        timeStart = Time.deltaTime;
        while(timeRemain >=0)
        {
            timeStart += Time.deltaTime;
            timeRemain = timeLimit - timeStart;
            goldToAdd = (int)timeRemain;
            cautionFill.fillAmount = (timeLimit - timeRemain)/timeLimit;
            yield return new WaitForFixedUpdate();
        }
            StopCaution();
    }

    public void AddGold()
    {
        if(goldToAdd <= 0) return;
        GameController.Instance.AddGold(goldToAdd);
        goldToAddText.text = "+" + goldToAdd.ToString();
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.AddGoldSound);
        StartCoroutine(TextEffect(goldToAddText));
    }

    IEnumerator TextEffect(TextMeshProUGUI text)
    {
        color.a = 0;
        while(color.a <= 1)
        {
            color.a += 2*Time.deltaTime;
            goldToAddText.color = color;
            yield return null;
        }
        if(color.a >= 1)
        {
            yield return new WaitForSeconds(0.3f);
            color.a = 0;
            goldToAddText.color = color;
            yield break;
        }
        
    }

    void SetGoldToAddTextColor()
    {
        color = goldToAddText.color;
        color.a = 0;
        goldToAddText.color = color;
    }
}
