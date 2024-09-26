using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPauseGame : BtnBase
{
    protected override void Start() 
    {
        base.Start();
    }

    protected override void OnButtonClick()
    {
        PlayClickSound();
        GameController.Instance.ShowPauseMenu();
        GameController.Instance.PauseGame();
    }
}
