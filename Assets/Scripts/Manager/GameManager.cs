using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    #region Action
    public Action OnGameEnd;
    #endregion

    private static GameManager i;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private BackgroundManager bgMng;
    [SerializeField]
    private PrintController printController;
    [SerializeField]
    private UI_Manager uiMng;
    [SerializeField]
    private ShapeController shapeCtrl;
    [SerializeField]
    private ShapeSpawnController spawnCtrl;
    [SerializeField]
    private ScoreController scoreCtrl;
    [SerializeField]
    private SoundManager soundMng;
    [SerializeField]
    private MusicSoundController musicCtrl;

    private GameSMController smCtrl;

    private void Awake()
    {
        i = this;
    }

    public void Setup(GameSMController _smCtrl)
    {
        smCtrl = _smCtrl;
        uiMng.Setup(this);
        soundMng.Setup(this);
        musicCtrl.Init(soundMng);
        Vibration.Setup(this);
        shapeCtrl.Setup(this);
        spawnCtrl.Setup(this);
        printController.Setup();
        bgMng.Setup();
    }

    public BackgroundManager GetBackgroundManager()
    {
        return bgMng;
    }

    public ShapeController GetShapeController()
    {
        return shapeCtrl;
    }

    public ShapeSpawnController GetSpawnController()
    {
        return spawnCtrl;
    }

    public UI_Manager GetUIManager()
    {
        return uiMng;
    }

    public ScoreController GetScoreController()
    {
        return scoreCtrl;
    }

    public Camera GetCamera()
    {
        return cam;
    }

    public PrintController GetPrintController()
    {
        return printController;
    }

    public SoundManager GetSoundManager()
    {
        return soundMng;
    }

    public MusicSoundController GetMusicController()
    {
        return musicCtrl;
    }

    public static void GameOver()
    {
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
        { "Speed", DifficultyManager.GetMovementSpeed() },
        { "SpawnRate", DifficultyManager.GetCurrentSpawnRate() },
        { "ShapeAmmount", ShapeController.GetShapeAmmount() }
        });
        i.OnGameEnd?.Invoke();
    }
}
