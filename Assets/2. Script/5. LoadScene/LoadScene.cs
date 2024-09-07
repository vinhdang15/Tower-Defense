using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string beginScene = "BeginScene";
    public string globalMap = "GlobalMap";
    public string overScene = "Over";
    public List<string> map = new();
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        map = new List<string>
        {
            "map1", "map2"
        };
    }

    public void LoadGlobalScene()
    {
        DisableCurrentSceneSetting();
        SceneManager.LoadScene(globalMap, LoadSceneMode.Additive);
        animator.SetTrigger("SlideUp");
    }

    public void LoadMap1Scene()
    {
        DisableCurrentSceneSetting();
        SceneManager.LoadScene(map[0]);
    }

    public void LoadBeginScene()
    {
        DisableCurrentSceneSetting();
        SceneManager.LoadScene(beginScene);
    }

    public void ReloadCurrentMap()
    {
        DisableCurrentSceneSetting();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DisableCurrentSceneSetting()
    {
        //Disable current EventSystem
        EventSystem currentEventSystem = EventSystem.current;
        if(currentEventSystem != null)
        {
            currentEventSystem.gameObject.SetActive(false);
        }
    }

    // animation event
    public void OnLoadGlobalSceneComplete()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
