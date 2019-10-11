using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    ShapeSpawnController spawnCtrl;

    void Start()
    {
        spawnCtrl = FindObjectOfType<ShapeSpawnController>();
        player = FindObjectOfType<PlayerController>();

        player.Setup();
        spawnCtrl.Setup();
    }
}
