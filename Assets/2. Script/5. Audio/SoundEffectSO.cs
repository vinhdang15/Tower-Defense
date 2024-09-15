using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Sound Effect", menuName = "Audio/Sound Effect")]
public class SoundEffectSO : ScriptableObject
{
    [Header("Next Wave Alarm")]
    public AudioClip comingWaveSound;
    [Header("Theme")]
    public List<AudioClip> Theme = new();

    [Header("Click Event")]
    public AudioClip clickSound;
    public AudioClip BuildSound;
    public AudioClip AddGoldSound;

    [Header("Weapons")]
    public AudioClip arrowSound;
    public AudioClip bomExplosionSound;
    public AudioClip bomWhistleSound;
    public AudioClip MagicBallHitSound;
     public AudioClip MagicBallWhistleSound;
    public List<AudioClip> Sword = new();
    

    public AudioClip GetRandomSwordSound()
    {
        int index = Random.Range(0, Sword.Count);
        return Sword[index];
    }

    public AudioClip GetThemeSound(int index)
    {
        return Theme[index];
    }
}
