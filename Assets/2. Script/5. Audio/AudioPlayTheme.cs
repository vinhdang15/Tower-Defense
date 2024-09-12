using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayTheme : MonoBehaviour
{
    [SerializeField] SoundEffectSO soundEffectSO;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.GetThemeSound(0));
    }
}
