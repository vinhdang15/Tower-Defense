using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemyManager : MonoBehaviour
{
    [Header("Wave Status")]
    public float timeForNextWave = 8f;
    public int currentWave;
    public bool beginNextWave;
    public bool beginSpawnCoroutine;
    public List<BtnCautionSlider> cautionSliders = new();
    [SerializeField] List<SpawnEnemy> spawnEnemies = new();

    [Header("Audio")]
    [SerializeField] SoundEffectSO soundEffectSO;
    AudioSource audioSource;

    void Start()
    {
        AddPath();
        UpdateTotalWave();
        audioSource = GetComponent<AudioSource>();
        GameController.Instance.UpdateCurrentWaveText(0);
        StartCoroutine(CheckAllEnemiesSpawnedAndDead());
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

    void UpdateTotalWave()
    {
        int totalWave = spawnEnemies[0].GetTotalWave();
        GameController.Instance.UpdateTotalWaveText(totalWave);
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
        foreach(SpawnEnemy spawner in spawnEnemies)
        {
            spawner.cautionButtonClicked = true;
            // Check if caution button first hit, then begin SpawnCoroutine at all path (each path is a spawnEnemy instance)
            if(beginSpawnCoroutine == false)
            {
                spawner.StartSpawnCoroutine();
                // reset spawnEnemy.cautionButtonClicked if this if the caution button was first hit (mean begin Spawn Coroutine)
                spawner.cautionButtonClicked = false;
            }
        }
        beginSpawnCoroutine = true;

        foreach(BtnCautionSlider caution in cautionSliders)
        {
            caution.isStartFirstWave = true;
            caution.AddGold();
            caution.StopCaution();   
        }
    }

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound(audioSource, soundEffectSO.comingWaveSound);
    }

    private bool AllEnemiesSpawnedAndDead()
    {
        foreach(SpawnEnemy spawner in spawnEnemies)
        {
            if(!spawner.EnemiesSpawnedAndDead()) return false;
        }
        return true;
    }
    private IEnumerator CheckAllEnemiesSpawnedAndDead()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            if(AllEnemiesSpawnedAndDead())
            {
                GameController.Instance.ShowVictoryMenu();
                yield break;
            }
            
        }
    }
}
