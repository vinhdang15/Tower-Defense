using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Sound Effect", menuName = "Audio/Sound Effect")]
public class SoundEffectSO : ScriptableObject
{
    
    public AudioClip arrowSound;
    public AudioClip bomExplosionSound;
    public AudioClip bomWhistleSound;
    public AudioClip comingWaveSound;
    public List<AudioClip> Sword = new();
    public List<AudioClip> Theme = new();

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
