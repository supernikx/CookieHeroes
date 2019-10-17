using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IC.UIBase;

public class UIMenu_MainMenu : UIControllerBase
{
    public Action StartGameButtonPressed;


    public void StartGame()
    {
        StartGameButtonPressed?.Invoke();
    }
}
