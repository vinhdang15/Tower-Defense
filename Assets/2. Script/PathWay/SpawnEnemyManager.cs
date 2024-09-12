using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    [Header("Wave Status")]
    public float timeForNextWave = 8f;
    public int currentWave;
    public bool beginNextWave;
    public bool beginSpawnCoroutine;
    public List<CautionSlider> cautionSliders = new();
    [SerializeField] List<SpawnEnemy> spawnEnemies = new();

    [Header("Audio")]
    [SerializeField] SoundEffectSO soundEffectSO;
    AudioSource audioSource;

    void Start()
    {
        AddPath();
        audioSource = GetComponent<AudioSource>();
    }

    void AddPath()
    {
        foreach(Transform pathTransform in transform)
        {
            if(pathTransform.gameObject.activeSelf)
            {
                var spawnEnemy = pathTransform.GetComponent<SpawnEnemy>();
                if (spawnEnemy != null)
                {
                    spawnEnemies.Add(spawnEnemy);
                    spawnEnemy.Initialize(this); // Initialize with manager reference
                }
            }
        }
    }
    public void CheckCurrentwaveIndex()
    {   
        currentWave = spawnEnemies[0].currentWave;
        foreach(SpawnEnemy spawn in spawnEnemies)
        {
            if(spawn.currentWave != currentWave)
            {
                beginNextWave = false;
                return;
            }
        }
        beginNextWave = true;
    }

    // button event
    public void GetNextWave()
    {
        PlaySound();
        // Check if caution button first hit, then begin SpawnCoroutine at all path (each path is a spawnEnemy instance)
        foreach(SpawnEnemy spawnEnemy in spawnEnemies)
        {
            spawnEnemy.cautionButtonClicked = true;
            if(beginSpawnCoroutine == false)
            {
                spawnEnemy.StartSpawnCoroutine();
                // reset spawnEnemy.cautionButtonClicked if this if the caution button was first hit (mean begin Spawn Coroutine)
                spawnEnemy.cautionButtonClicked = false;
            }
            //Debug.Log(spawnEnemy.cautionButtonClicked);
        }
        beginSpawnCoroutine = true;

        foreach(CautionSlider caution in cautionSliders)
        {
            caution.StopCaution();
            caution.isStartFirstWave = true;
        }
    }

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.comingWaveSound);
    }

}
