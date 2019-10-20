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
        ShapeController.OnNewShapeAdd += HandleOnNewShapeAdd;

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
        gameplayPanel.UpdateShape(_newShape, ShapeController.GetShapeByIndex(currentShapeIndex - 1), ShapeController.GetShapeByIndex(currentShapeIndex + 1));
    }

    private void HandleOnNewShapeAdd(ShapeMatchScriptable _newShape)
    {
        playeCtrl.ChangeShape(ShapeController.GetIndexByShape(_newShape));
    }

    private void HandleOnGameEnd()
    {
        Complete();
    }

    public override void Exit()
    {
        context.GetGameManager().OnGameEnd -= HandleOnGameEnd;
        playeCtrl.OnShapeChanged -= HandleOnShapeChange;
        ShapeController.OnNewShapeAdd -= HandleOnNewShapeAdd;

        context.GetGameManager().GetPlayerController().Disable();
        spawnCtrl.StopSpawn();
    }
}