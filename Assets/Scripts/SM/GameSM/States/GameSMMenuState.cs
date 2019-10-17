using System;
using IC.BaseSM;

/// <summary>
/// Stato di Menu della GameSM
/// </summary>
public class GameSMMenuState : GameSMBaseState
{
    UI_Manager uiMng;
    UIMenu_MainMenu mainMenuPanel;

    public override void Enter()
    {
        uiMng = context.GetGameManager().GetUIManager();
        uiMng.SetCurrentMenu<UIMenu_MainMenu>();

        mainMenuPanel = uiMng.GetMenu<UIMenu_MainMenu>();
        mainMenuPanel.StartGameButtonPressed = StartGameButtonPressed;
    }

    private void StartGameButtonPressed()
    {
        Complete();
    }
}