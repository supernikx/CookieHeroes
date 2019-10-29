using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Action
    public Action OnGameEnd;
    #endregion

    private static GameManager i;

    Camera cam;
    BackgroundManager bgMng;
    GameSMController smCtrl;
    UI_Manager uiMng;
    ShapeController shapeCtrl;
    ShapeSpawnController spawnCtrl;
    ScoreController scoreCtrl;

    private void Awake()
    {
        i = this;
    }

    public void Setup(GameSMController _smCtrl)
    {
        smCtrl = _smCtrl;
        uiMng = FindObjectOfType<UI_Manager>();
        shapeCtrl = FindObjectOfType<ShapeController>();
        spawnCtrl = FindObjectOfType<ShapeSpawnController>();
        cam = FindObjectOfType<Camera>();
        bgMng = FindObjectOfType<BackgroundManager>();
        scoreCtrl = GetComponent<ScoreController>();

        shapeCtrl.Setup(this);
        spawnCtrl.Setup(this);
        uiMng.Setup(this);
        bgMng.Setup(cam);
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

    public static void GameOver()
    {
        i.OnGameEnd?.Invoke();
    }
}
