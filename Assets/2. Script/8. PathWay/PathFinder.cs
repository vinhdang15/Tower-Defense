using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigList;
    [SerializeField] List<Transform> waypoints;
    int wayPointIndex = 0;
    
    public int pathWaveIndex;

    public void SetPosInPathWave(int _pathWaveIndex)
    {
        SetPathWaveIndex(_pathWaveIndex);
        GetWayPoints();
    }

    void SetPathWaveIndex(int _pathWaveIndex)
    {
        pathWaveIndex = _pathWaveIndex;
    }

    void GetWayPoints()
    {
        transform.position = waveConfigList[pathWaveIndex].GetStartingWaypoint().position;
        waypoints =  waveConfigList[pathWaveIndex].GetWayPoints();
    }

    public void FollowPath(float speed)
    {
        // foreach(Transform target in waypoints)
        // {
        //     while(transform.position != target.position)
        //     {
        //         transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //         Debug.Log("check");
        //     }
        // }

        if (wayPointIndex < waypoints.Count)
        {
            if(transform.position != waypoints[wayPointIndex].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[wayPointIndex].position,speed * Time.deltaTime);
            }
            else
            {
                wayPointIndex++;
            }
        }
    }
}
