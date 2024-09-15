using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    
    SpawnEnemyManager spawnEnemyManager;
    [SerializeField] CautionSlider cautionSlider;
    [SerializeField] List<EnemyEntry> enemyEntries = new List<EnemyEntry>();
    [SerializeField] float timeBetweenEnemy;
    public int currentWave;
    public bool cautionButtonClicked = false;
    public float elapsed;

    [SerializeField] SoundEffectSO soundEffectSO;

    void Awake()
    {
        currentWave = 0;
    }

    public void Initialize(SpawnEnemyManager manager)
    {
        spawnEnemyManager = manager;
        spawnEnemyManager.cautionSliders.Add(cautionSlider);

        GameObject canvas = GameObject.Find("Canvas - WorldSpace");
        cautionSlider.transform.SetParent(canvas.transform, true);

        CheckToShowCautionAtFirstWave();
    }

    public void StartSpawnCoroutine()
    {
        StartCoroutine(InstantiateEnemyWave());
    }
    
    IEnumerator InstantiateEnemyWave()
    {
        currentWave = 1;
        GameController.Instance.UpdateCurrentWave(currentWave);
        for(int y = 0; y < enemyEntries.Count; y++)
        {
            for(int i = 0; i < enemyEntries[y].numberEnemyInWave; i++)
            {
                InstantiateEnemy(enemyEntries[y].enemy, i);
                yield return new WaitForSeconds(SetTimeBetweenEnemy());
            }
            currentWave++;
            // cause the number enemy been spawn in each wave of each path is different
            // so the time to finish of each wave is not the same
            // then need wait until they are get in the same wave to begin the next wave  
            // Check Current wave Index of each Land
            spawnEnemyManager.CheckCurrentwaveIndex();
            // wait until all land are in the same wait
            yield return new WaitUntil(() => spawnEnemyManager.beginNextWave);
            float timeToShowCaution = spawnEnemyManager.timeForNextWave - cautionSlider.timeLimit;
            yield return new WaitForSeconds(timeToShowCaution);

            if (timeToShowCaution > 0 &&
                y < enemyEntries.Count - 1 &&
                enemyEntries[y + 1].numberEnemyInWave > 0)              
            {        
                //yield return new WaitForSeconds(timeToShowCaution);
                cautionSlider.StartUpCaution();
            }
            // using yield here to sync time of all Spawn enemy instance,
            // because atleast there is one path have show up caution button

            

            // check if GetNextWave() is click or not
            elapsed = 0f;
            while(elapsed < cautionSlider.timeLimit)
            {
                if(cautionButtonClicked)
                {
                    cautionButtonClicked = false;
                    break;
                }
                yield return new WaitForSeconds(0.1f);
                elapsed += 0.1f;
            }
            
            if(currentWave <= enemyEntries.Count)
            {
                GameController.Instance.UpdateCurrentWave(currentWave);
                spawnEnemyManager.PlaySound();
            }
        }
    }

    void CheckToShowCautionAtFirstWave()
    {
        if(enemyEntries[0].numberEnemyInWave == 0)
        {
            cautionSlider.StopCaution();
        }
    }
    
    float SetTimeBetweenEnemy()
    {
        return Random.Range(timeBetweenEnemy * 0.5f, timeBetweenEnemy * 2f);
    }
    void InstantiateEnemy(GameObject enemy, int index)
    {
        GameObject enemyInstantiate = Instantiate(enemy, transform);
        enemyInstantiate.GetComponent<PathFinder>().SetPosInPathWave(index % 3);
    }

    public int GetTotalWave()
    {
        return enemyEntries.Count;
    }
}

[System.Serializable]
public class EnemyEntry
{
    public GameObject enemy;
    public int numberEnemyInWave;
}

