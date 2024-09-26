using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnNextMap : BtnBase
{
    [SerializeField] MainMenu mainMenu;
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnButtonClick()
    {
        PlayClickSound();
        mainMenu.NextMap();
    }
}
