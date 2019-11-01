using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Stato di Gameplay della GameSM
/// </summary>
public class GameSMGameplayState : GameSMBaseState
{
    ScoreController scoreCtrl;
    ShapeController shapeCtrl;
    BackgroundManager bgMng;
    ShapeSpawnController spawnCtrl;
    UI_Manager uiMng;
    UIMenu_Gameplay gameplayPanel;

    bool readInput;
    bool videoAlreadyWhatched;

    public override void Enter()
    {
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

        scoreCtrl.Init();
        bgMng.StartBackground();
        spawnCtrl.StartSpawn();

        gameplayPanel.UpdateScore(scoreCtrl.GetCurrentScore());
        HandleOnShapeChange(ShapeController.GetCurrentShape());

        videoAlreadyWhatched = false;
        readInput = true;
    }

    public override void Tick()
    {
        if (readInput && SwipeController.IsSwiping(Direction.Right))
        {
            shapeCtrl.ChangeShape(ShapeController.GetCurrentShapeIndex() - 1);
        }
        else if (readInput && SwipeController.IsSwiping(Direction.Left))
        {
            shapeCtrl.ChangeShape(ShapeController.GetCurrentShapeIndex() + 1);
        }
    }

    #region Handlers
    private void HandleOnShapeChange(ShapeScriptable _newShape)
    {
        int currentShapeIndex = ShapeController.GetCurrentShapeIndex();
        gameplayPanel.UpdateShape(_newShape, ShapeController.GetShapeByIndex(currentShapeIndex - 1), ShapeController.GetShapeByIndex(currentShapeIndex + 1));
    }

    private void HandleOnNewShapeAdd(ShapeScriptable _newShape)
    {
        shapeCtrl.ChangeShape(ShapeController.GetIndexByShape(_newShape));
    }

    private void HandleOnShapeGuessed()
    {
        scoreCtrl.AddScore();
        gameplayPanel.UpdateScore(scoreCtrl.GetCurrentScore());
    }


    private void HandleOnShapeWrong()
    {
        if (videoAlreadyWhatched)
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

        bgMng.ResetBackground();
        spawnCtrl.StopSpawn();
    }
}