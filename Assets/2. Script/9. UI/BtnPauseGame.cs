using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPauseGame : BtnBase
{
    protected override void Start() 
    {
        PlayClickSound();
        base.Start();
    }

    protected override void OnButtonClick()
    {
        PauseGame();
    }
}
