using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSMEndGame : GameSMBaseState
{
    UI_Manager uiMng;
    UIMenu_EndGame endGamePanel;

    public override void Enter()
    {
        uiMng = context.GetGameManager().GetUIManager();
        uiMng.SetCurrentMenu<UIMenu_EndGame>();

        endGamePanel = uiMng.GetMenu<UIMenu_EndGame>();
        endGamePanel.RetyButtonPressed = RetryButtonPressed;
    }

    private void RetryButtonPressed()
    {
        Complete(0);
    }
}
