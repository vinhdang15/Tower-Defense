using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnLoadMainMenu : BtnBase
{
    protected override void Start() 
    {
        sceneName = "MainMenuScene";
        base.Start();
    }
}
