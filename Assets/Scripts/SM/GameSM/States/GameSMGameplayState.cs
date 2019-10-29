using UnityEngine.SceneManagement;
using System;

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

    public override void Enter()
    {
        spawnCtrl = context.GetGameManager().GetSpawnController();
        uiMng = context.GetGameManager().GetUIManager();
        shapeCtrl = context.GetGameManager().GetShapeController();
        bgMng = context.GetGameManager().GetBackgroundManager();
        scoreCtrl = context.GetGameManager().GetScoreController();

        PrintController.OnShapeGuessed += HandleOnShapeGuessed;
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
    }

    public override void Tick()
    {
        if (SwipeController.IsSwiping(Direction.Right))
        {
            shapeCtrl.ChangeShape(ShapeController.GetCurrentShapeIndex() - 1);
        }
        else if (SwipeController.IsSwiping(Direction.Left))
        {
            shapeCtrl.ChangeShape(ShapeController.GetCurrentShapeIndex() + 1);
        }
    }

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

    private void HandleOnGameEnd()
    {
        Complete();
    }

    public override void Exit()
    {
        context.GetGameManager().OnGameEnd -= HandleOnGameEnd;
        ShapeController.OnShapeChanged -= HandleOnShapeChange;
        ShapeController.OnNewShapeAdd -= HandleOnNewShapeAdd;
        PrintController.OnShapeGuessed -= HandleOnShapeGuessed;

        bgMng.ResetBackground();
        spawnCtrl.StopSpawn();
    }
}