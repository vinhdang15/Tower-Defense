using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public string beginScene = "BeginScene";
    public string globalMap = "GlobalMap";
    public List<string> sceneList;
    [HideInInspector] public string sceneName;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadScenesFromBuildSettings();
    }

    void LoadScenesFromBuildSettings()
    {
        sceneList = new List<string>();
        int i = 0;
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if(scene.enabled)
            {
                string scenePath = scene.path;
                string sceneNameString = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                sceneList.Add(sceneNameString);
                Debug.Log(i++);
            }
        }
    }

    #region cũ
    public void LoadGlobalScene()
    {
        DisableCurrentSceneSetting();
        SceneManager.LoadScene(globalMap, LoadSceneMode.Additive);
    }

    public void LoadMap1Scene()
    {
        DisableCurrentSceneSetting();
        SceneManager.LoadScene(sceneList[0]);
    }

    public void LoadMap2Scene()
    {
        DisableCurrentSceneSetting();
        SceneManager.LoadScene(sceneList[1]);
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
    #endregion
    

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
