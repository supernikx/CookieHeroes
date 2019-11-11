using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameSMEndGame : GameSMBaseState
{
    [Header("Retry Settings")]
    [SerializeField]
    private int retryForAds;

    UI_Manager uiMng;
    UIMenu_EndGame endGamePanel;
    int currentRetries = 0;

    public override void Enter()
    {
        uiMng = context.GetGameManager().GetUIManager();

        uiMng.SetCurrentMenu<UIMenu_EndGame>();
        endGamePanel = uiMng.GetMenu<UIMenu_EndGame>();
        endGamePanel.RetyButtonPressed = RetryButtonPressed;
    }

    private void RetryButtonPressed()
    {
        currentRetries++;
        if (currentRetries == retryForAds)
        {
            currentRetries = 0;
            UnityAdsManager.instance.ShowRegularAD(AdShowed);
        }
        else
            Complete();
    }

    private void AdShowed(ShowResult _results)
    {
        Complete();
    }
}
