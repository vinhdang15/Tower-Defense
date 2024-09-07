using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Tower Properties")]
    public float spawnRate = 1f;
    public Transform SpawnPoint; 
    public Transform spawnHolder;
    public List<Transform> enemiesInRange = new List<Transform>();

    [Header("Tower Spec")]
    public int currentLevel;
    public int[] upgradeCosts = new int[3];
    public float[] detectionRanges = new float[3];
    //[SerializeField] GameObject[] bulletPrefabs = new GameObject[3];
    public List<GameObject> buildingSpotList = new List<GameObject>();
    public int goldRefund = 0;

    public float TowerBodyRadius = 0.7f;
    public float currentRange = 4f;
    public GameObject currentSpawnPrefab;

    public CircleCollider2D towerCollider;
    public Animator animator;

    #region DETECH ENEMY
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AddEnemy(other.transform);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            RemoveEnemy(other.transform);
        }
    }
    public void AddEnemy(Transform enemy)
    {
        enemiesInRange.Add(enemy);
    }

    public void RemoveEnemy(Transform enemy)
    {
        enemiesInRange.Remove(enemy);
    }
    #endregion

    #region SPAWN BULLET
    public IEnumerator SpawnBulletCoroutine(Animator animator)
    {
        while (true)
        {
            if (enemiesInRange.Count > 0){
                animator.SetTrigger("isTrigger");
                yield return new WaitForSeconds(spawnRate);
            }
            yield return null;
        }
    }

    // Animation Event
    public virtual void SpawnObject()
    {
        if(enemiesInRange.Count == 0) return;
        GameObject bullet = Instantiate(currentSpawnPrefab, SpawnPoint.position, Quaternion.identity, spawnHolder);
        bullet.GetComponent<Bullet>().SetTarget(enemiesInRange[0]);
    }
    #endregion

    #region TOWER BODY && TOWER COLLIIDER RANGE
    public virtual float GetTowerCurrentRange()
    {
        return currentRange;
    }
    
    public void SetTowerBodyRadius()
    {   // Tower Body Radius is fixed number
        foreach(var i in GetComponentsInChildren<CircleCollider2D>())
        {
            if(i.gameObject.layer == LayerMask.NameToLayer("TowerBody")) i.radius = TowerBodyRadius;
        }
    }

    public virtual void SetTowerColliderRange()
    {
        towerCollider.radius = currentRange;
    }

    public virtual float GetTowerCurentLevelRange()
    {
        return towerCollider.radius;
    }

    public virtual float GetTowerNextLevelRange()
    {
        if(currentLevel < upgradeCosts.Length -1)  return detectionRanges[currentLevel + 1];
        else                                    return detectionRanges[currentLevel];
    }

    #endregion

    #region INSTANTIATE AND UPGRADE
    public virtual int GetGoldInstantiate()
    {
        return upgradeCosts[0];
    }

    public virtual int GetGoldUpgrade()
    {
        if(currentLevel == 2) return 0;
        else return upgradeCosts[currentLevel + 1];
    }

    public virtual void Upgrade(int level)
    {
        // currentLevel = level;
        // currentRange = detectionRanges[level];
        // currentBulletPrefab = bulletPrefabs[level];
        // currentSpawnPrefab = bulletPrefabs[level];  
    }

    public virtual void TryUpgrade()
    {
        if(CanUpgrade())
        {
            int goldUpgrade = GetGoldUpgrade();
            GameController.SpendGold(goldUpgrade);
            goldRefund += goldUpgrade;

            Upgrade(currentLevel + 1);
            SetTowerColliderRange();
        }
    }

    public virtual bool CanInstantiate()
    {
        if(GameController.totalGold >= upgradeCosts[0]) return true;
        else return false;
    }

    public virtual bool CanUpgrade()
    {
        if(GameController.totalGold >= GetGoldUpgrade()) return true;
        else return false;
    }
    #endregion

    public int GetGoldRefund()
    {
        return goldRefund;
    }
}
