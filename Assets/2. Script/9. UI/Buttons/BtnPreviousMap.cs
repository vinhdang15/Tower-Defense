using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnPreviousMap : BtnBase
{
    [SerializeField] MainMenu mainMenu;
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnButtonClick()
    {
        PlayClickSound();
        mainMenu.PerviousMap();
    }
}
