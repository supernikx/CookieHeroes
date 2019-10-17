using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Stato di Gameplay della GameSM
/// </summary>
public class GameSMGameplayState : GameSMBaseState
{
    PlayerController playeCtrl;
    ShapeSpawnController spawnCtrl;
    UI_Manager uiMng;
    UIMenu_Gameplay gameplayPanel;

    public override void Enter()
    {
        spawnCtrl = context.GetGameManager().GetSpawnController();
        uiMng = context.GetGameManager().GetUIManager();
        playeCtrl = context.GetGameManager().GetPlayerController();

        playeCtrl.OnShapeChanged += HandleOnShapeChange;
        context.GetGameManager().OnGameEnd += HandleOnGameEnd;

        playeCtrl.Enable();
        gameplayPanel = uiMng.GetMenu<UIMenu_Gameplay>();
        uiMng.SetCurrentMenu<UIMenu_Gameplay>();
        HandleOnShapeChange(playeCtrl.GetCurrentShape());

        spawnCtrl.StartSpawn();
    }

    public override void Tick()
    {
        if (SwipeController.IsSwiping(Direction.Right))
        {
            playeCtrl.ChangeShape(playeCtrl.GetCurrentShapeIndex() - 1);
        }
        else if (SwipeController.IsSwiping(Direction.Left))
        {
            playeCtrl.ChangeShape(playeCtrl.GetCurrentShapeIndex() + 1);
        }
    }

    private void HandleOnShapeChange(ShapeScriptable _newShape)
    {
        int currentShapeIndex = playeCtrl.GetCurrentShapeIndex();
        gameplayPanel.UpdateShape(_newShape, playeCtrl.GetShapeByIndex(currentShapeIndex - 1), playeCtrl.GetShapeByIndex(currentShapeIndex + 1));
    }

    private void HandleOnGameEnd()
    {
        Complete();
    }

    public override void Exit()
    {
        context.GetGameManager().OnGameEnd -= HandleOnGameEnd;
        playeCtrl.OnShapeChanged -= HandleOnShapeChange;

        context.GetGameManager().GetPlayerController().Disable();
        spawnCtrl.StopSpawn();
    }
}