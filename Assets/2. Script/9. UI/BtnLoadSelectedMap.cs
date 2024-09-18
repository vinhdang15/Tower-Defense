using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BtnLoadSelectedMap : BtnBase
{
    [SerializeField] MainMenu mainMenu;
    protected override void Start()
    {
        base.Start();
    }

    void GetCurrentMapSceneName()
    {
        sceneName = mainMenu.currentMapSceneName;
        SceneManager.LoadScene(sceneName);
    }

    protected override void LoadScene()
    {
        sceneName = mainMenu.currentMapSceneName;
        SceneManager.LoadScene(sceneName);
    }
}
