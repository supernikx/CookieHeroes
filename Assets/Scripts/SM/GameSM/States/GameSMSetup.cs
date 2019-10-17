using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSMSetup : GameSMBaseState
{
    public override void Enter()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        GameSMController smCtrl = FindObjectOfType<GameSMController>();

        gm.Setup(smCtrl);
        smCtrl.Setup(new GameSMController.Context(smCtrl, gm));

        Complete();
    }
}
