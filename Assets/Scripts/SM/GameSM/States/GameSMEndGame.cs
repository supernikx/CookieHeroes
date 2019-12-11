﻿using System;
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
    MusicSoundController musicCtrl;
    int currentRetries = 0;

    public override void Enter()
    {
        uiMng = context.GetGameManager().GetUIManager();
        endGamePanel = uiMng.GetMenu<UIMenu_EndGame>();
        spawnCtrl = context.GetGameManager().GetSpawnController();
        printCtrl = context.GetGameManager().GetPrintController();
        bgMng = context.GetGameManager().GetBackgroundManager();
        musicCtrl = context.GetGameManager().GetMusicController();

        endGamePanel.RetyButtonPressed = RetryButtonPressed;
        endGamePanel.MainMenuButtonPressed = MainMenuButtonPressed;

        printCtrl.EndGameAnimation(OnPrinterAnimationEnd);
    }
    #region Callbacks
    private void OnPrinterAnimationEnd()
    {
        if (bgMng != null)
            bgMng.ResetBackground();

        if (spawnCtrl != null)
            spawnCtrl.StopSpawn();

        musicCtrl.PlayEndGameClip();
        uiMng.SetCurrentMenu<UIMenu_EndGame>(0.5f, 0.5f, EndGameAnim);
    }

    private void EndGameAnim()
    {
        uiMng.SetCurrentMenuAnimation<UIMenu_EndGame>("EndGame");
        endGamePanel.PlayFeedback();
    }
    #endregion

    private void RetryButtonPressed()
    {
        currentRetries++;
        if (currentRetries == retryForAds)
        {
            currentRetries = 0;
            UnityAdsManager.instance.ShowRegularAD(AdShowed);
        }
        else
            Complete(2);
    }

    private void MainMenuButtonPressed()
    {
        currentRetries++;
        if (currentRetries == retryForAds)
        {
            currentRetries = 0;
            UnityAdsManager.instance.ShowRegularAD(AdShowed);
        }
        else
            Complete(1);
    }

    private void AdShowed(ShowResult _results)
    {
        Complete();
    }

    public override void Exit()
    {
        if (endGamePanel != null)
        {
            endGamePanel.RetyButtonPressed = null;
            endGamePanel.MainMenuButtonPressed = null;
        }
    }
}
