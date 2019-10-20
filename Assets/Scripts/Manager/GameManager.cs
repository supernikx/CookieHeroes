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

    GameSMController smCtrl;
    UI_Manager uiMng;
    PlayerController playerCtrl;
    ShapeController shapeCtrl;
    ShapeSpawnController spawnCtrl;

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
        playerCtrl = FindObjectOfType<PlayerController>();

        shapeCtrl.Setup(this);
        uiMng.Setup();
        playerCtrl.Setup();
    }

    public PlayerController GetPlayerController()
    {
        return playerCtrl;
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

    public static void GameOver()
    {
        i.OnGameEnd?.Invoke();
    }
}
