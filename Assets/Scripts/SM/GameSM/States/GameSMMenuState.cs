using System;
using IC.BaseSM;

/// <summary>
/// Stato di Menu della GameSM
/// </summary>
public class GameSMMenuState : GameSMBaseState
{
    UI_Manager uiMng;
    UIMenu_MainMenu mainMenuPanel;
    MusicSoundController musicCtrl;

    public override void Enter()
    {
        uiMng = context.GetGameManager().GetUIManager();
        mainMenuPanel = uiMng.GetMenu<UIMenu_MainMenu>();
        musicCtrl = context.GetGameManager().GetMusicController();

        musicCtrl.PlayMainMenuClip();
        uiMng.SetCurrentMenuAnimation<UIMenu_MainMenu>("MainMenuIn");
        mainMenuPanel.StartGameButtonPressed = StartGameButtonPressed;
    }

    private void StartGameButtonPressed()
    {
        uiMng.SetCurrentMenuAnimation<UIMenu_MainMenu>("MainMenuOut", OnMainMenuAnimationEndCallback);
    }

    private void OnMainMenuAnimationEndCallback()
    {
        Complete();
    }

}