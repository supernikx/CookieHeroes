using System;
using IC.BaseSM;

/// <summary>
/// Stato di Menu della GameSM
/// </summary>
public class GameSMMenuState : GameSMBaseState
{
    GameManager gm;
    UI_Manager uiMng;
    UIMenu_MainMenu mainMenuPanel;
    MusicSoundController musicCtrl;

    public override void Enter()
    {
        gm = context.GetGameManager();
        musicCtrl = gm.GetMusicController();
        uiMng = gm.GetUIManager();
        mainMenuPanel = uiMng.GetMenu<UIMenu_MainMenu>();

        uiMng.SetCurrentMenuAnimation<UIMenu_MainMenu>("MainMenuIn");
        CoroutineController.StartRoutine(() => mainMenuPanel.EnablePanelContainer(true), 0.01f);
        musicCtrl.PlayMainMenuClip();
        mainMenuPanel.StartGameButtonPressed = StartGameButtonPressed;
    }

    private void StartGameButtonPressed()
    {
        uiMng.SetCurrentMenuAnimation<UIMenu_MainMenu>("MainMenuOut", OnMainMenuAnimationEndCallback);
    }

    private void OnMainMenuAnimationEndCallback()
    {
        mainMenuPanel.EnablePanelContainer(false);
        Complete();
    }

}