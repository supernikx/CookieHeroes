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

    ShapeSpawnController spawnCtrl;
    BackgroundManager bgMng;
    PrintController printCtrl;
    UI_Manager uiMng;
    UIMenu_EndGame endGamePanel;
    int currentRetries = 0;

    public override void Enter()
    {
        uiMng = context.GetGameManager().GetUIManager();
        spawnCtrl = context.GetGameManager().GetSpawnController();
        printCtrl = context.GetGameManager().GetPrintController();
        bgMng = context.GetGameManager().GetBackgroundManager();
        endGamePanel = uiMng.GetMenu<UIMenu_EndGame>();

        endGamePanel.RetyButtonPressed = RetryButtonPressed;

        printCtrl.EndGameAnimation(OnPrinterAnimationEnd);
    }

    private void OnPrinterAnimationEnd()
    {
        if (bgMng != null)
            bgMng.ResetBackground();

        if (spawnCtrl != null)
            spawnCtrl.StopSpawn();

        uiMng.SetCurrentMenu<UIMenu_EndGame>();
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
