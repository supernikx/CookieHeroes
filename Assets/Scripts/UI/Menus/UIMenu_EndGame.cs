using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IC.UIBase;

public class UIMenu_EndGame : UIControllerBase
{
    public Action RetyButtonPressed;
    public Action MainMenuButtonPressed;

    public void RetryButton()
    {
        RetyButtonPressed?.Invoke();
    }

    public void MainMenuButton()
    {
        MainMenuButtonPressed?.Invoke();
    }
}
