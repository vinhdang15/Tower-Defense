using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    public int currentWave;
    public bool beginNextWave;
    public List<CautionSlider> cautionSliders = new();
    [SerializeField] List<SpawnEnemy> spawnEnemies = new();

    void Start()
    {
        AddPath();
    }

    void AddPath()
    {
        foreach(Transform pathTransform in transform)
        {
            if(pathTransform.gameObject.activeSelf)
            {
                spawnEnemies.Add(pathTransform.GetComponent<SpawnEnemy>());
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
        foreach(SpawnEnemy spawnEnemy in spawnEnemies)
        {
            spawnEnemy.cautionButtonClicked = true;
        }

        foreach(CautionSlider caution in cautionSliders)
        {
            caution.StopCaution();
            caution.isStartFirstWave = true;
        }   
    }

}
