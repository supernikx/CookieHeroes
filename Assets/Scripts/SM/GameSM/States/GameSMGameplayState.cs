using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Stato di Gameplay della GameSM
/// </summary>
public class GameSMGameplayState : GameSMBaseState
{
    ScoreController scoreCtrl;
    UnityAdsManager adsMng;
    ShapeController shapeCtrl;
    BackgroundManager bgMng;
    ShapeSpawnController spawnCtrl;
    UI_Manager uiMng;
    UIMenu_Gameplay gameplayPanel;

    float changeShapeDelayTime = 0.1f;
    float changeShapeDelayTimer;
    bool readInput;
    bool videoAlreadyWhatched;

    public override void Enter()
    {
        adsMng = UnityAdsManager.instance;
        spawnCtrl = context.GetGameManager().GetSpawnController();
        uiMng = context.GetGameManager().GetUIManager();
        shapeCtrl = context.GetGameManager().GetShapeController();
        bgMng = context.GetGameManager().GetBackgroundManager();
        scoreCtrl = context.GetGameManager().GetScoreController();

        PrintController.OnShapeGuessed += HandleOnShapeGuessed;
        PrintController.OnShapeWrong += HandleOnShapeWrong;
        ShapeController.OnShapeChanged += HandleOnShapeChange;
        ShapeController.OnNewShapeAdd += HandleOnNewShapeAdd;
        context.GetGameManager().OnGameEnd += HandleOnGameEnd;

        gameplayPanel = uiMng.GetMenu<UIMenu_Gameplay>();
        uiMng.SetCurrentMenu<UIMenu_Gameplay>();

        DifficultyManager.StartGame();
        scoreCtrl.Init();
        bgMng.StartBackground();
        spawnCtrl.StartSpawn();

        gameplayPanel.UpdateScore(scoreCtrl.GetCurrentScore());
        HandleOnShapeChange(Direction.None, ShapeController.GetCurrentShape(), false);

        videoAlreadyWhatched = false;
        readInput = true;
    }

    public override void Tick()
    {
        if (changeShapeDelayTimer > 0)
        {
            changeShapeDelayTimer -= Time.deltaTime;
            return;
        }

        if (readInput && SwipeController.IsSwiping(Direction.Right))
        {
            SwipeController.RightSwipe();
            shapeCtrl.ChangeShape(Direction.Right, true);
            changeShapeDelayTimer = changeShapeDelayTime;
        }
        else if (readInput && SwipeController.IsSwiping(Direction.Left))
        {
            SwipeController.LeftSwipe();
            shapeCtrl.ChangeShape(Direction.Left, true);
            changeShapeDelayTimer = changeShapeDelayTime;
        }
    }

    #region Handlers
    private void HandleOnShapeChange(Direction _swipeDir, ShapeScriptable _newShape, bool _animation)
    {
        int currentShapeIndex = ShapeController.GetCurrentShapeIndex();
        gameplayPanel.UpdateShape(_swipeDir, _newShape, ShapeController.GetShapeByIndex(currentShapeIndex - 1), ShapeController.GetShapeByIndex(currentShapeIndex + 1), _animation, changeShapeDelayTime);
    }

    private void HandleOnNewShapeAdd(ShapeScriptable _newShape)
    {
        shapeCtrl.ChangeShape(_newShape);
    }

    private void HandleOnShapeGuessed()
    {
        scoreCtrl.AddScore();
        gameplayPanel.UpdateScore(scoreCtrl.GetCurrentScore());
    }


    private void HandleOnShapeWrong()
    {
        if (videoAlreadyWhatched || !adsMng.CheckCanShowAD())
            GameManager.GameOver();
        else
        {
            Time.timeScale = 0;
            readInput = false;
            gameplayPanel.EnableVideoRewardPanel(HandleOnRewardedVideoEnd);
        }
    }

    private void HandleOnRewardedVideoEnd(bool _result)
    {
        Time.timeScale = 1;

        if (!_result)
            GameManager.GameOver();
        else
        {
            readInput = true;
            videoAlreadyWhatched = true;
        }
    }

    private void HandleOnGameEnd()
    {
        Complete();
    }
    #endregion

    public override void Exit()
    {
        context.GetGameManager().OnGameEnd -= HandleOnGameEnd;
        ShapeController.OnShapeChanged -= HandleOnShapeChange;
        ShapeController.OnNewShapeAdd -= HandleOnNewShapeAdd;
        PrintController.OnShapeGuessed -= HandleOnShapeGuessed;
        PrintController.OnShapeWrong -= HandleOnShapeWrong;

        readInput = false;
        DifficultyManager.StopGame();
    }
}