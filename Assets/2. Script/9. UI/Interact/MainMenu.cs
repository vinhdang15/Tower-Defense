using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // The name of play map scene
    [SerializeField] public string currentMapSceneName;
    [SerializeField] Image mapImage;
    [SerializeField] TextMeshProUGUI mapName;
    [SerializeField] TextMeshProUGUI mapDescription;
    [SerializeField] List<MapStatus> mapList = new();
    List<Sprite> mapSpriteList;
    [SerializeField] int currentMapIndex;
    Dictionary<int, (string sceneName, string name, string description)> mapDetails = new Dictionary<int, (string sceneName, string name, string description)>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentMapIndex = 0;
        SetMapList();
        SetMap();
    }

    public void SetMapList()
    {
        AddMapSpriteList();
        SetMapDetails();
        
        MapStatus mapStatus;
        for(int i = 0; i < mapSpriteList.Count; i++)
        {
            mapStatus = new MapStatus
            {
                sprite = mapSpriteList[i],
                sceneName = mapDetails.Keys.Contains(i) ? mapDetails[i].sceneName : "null",
                UIname = mapDetails.Keys.Contains(i) ? mapDetails[i].name : "null",
                description = mapDetails.Keys.Contains(i) ? mapDetails[i].description : "null"
            };
            mapList.Add(mapStatus);
        }
    }

    public void AddMapSpriteList()
    {
        Sprite[] mapImagesArray = Resources.LoadAll<Sprite>("Map");
        mapSpriteList = new List<Sprite>(mapImagesArray);
        mapSpriteList.Sort((x,y) => string.Compare(x.name, y.name, System.StringComparison.Ordinal));
    }

    public void SetMapDetails()
    {
        mapDetails = new Dictionary<int, (string sceneName, string name, string description)>
        {
            { 0, ("Map1", "THE FIELD", "Lately there is thief coming from the east") },
            { 1, ("Map2", "THE ANGRY FOREST", "End of the Horizontal there is a Myth Forest, with angry beasts inside") }
        };
    }

    // Button click
    public void NextMap()
    {
        if(currentMapIndex < mapList.Count -1)
        {
            currentMapIndex++;
            SetMap();
        }
    }

    public void PerviousMap()
    {
        if(currentMapIndex > 0) 
        {
            currentMapIndex--;
            SetMap();
        }
    }

    void SetMap()
    {
        currentMapSceneName = mapList[currentMapIndex].sceneName;
        mapImage.sprite = mapList[currentMapIndex].sprite;
        mapName.text = mapList[currentMapIndex].UIname;
        mapDescription.text = mapList[currentMapIndex].description;
    }
}

[System.Serializable]
public class MapStatus
{
    public Sprite sprite;
    public string sceneName;
    public string UIname;
    public string description;
}
