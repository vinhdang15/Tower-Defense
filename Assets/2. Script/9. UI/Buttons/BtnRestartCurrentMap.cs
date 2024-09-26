using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRestartCurrentMap : BtnBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnButtonClick()
    {
        PlayClickSound();
        GameController.Instance.HidePauseMenu();
        ReLoadCurrentScene();
    }
}
