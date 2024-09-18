using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] Transform pathPrefab;
    
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWayPoints()
    {
        var wavePoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            wavePoints.Add(child);
        }
        return wavePoints;
    }
}
