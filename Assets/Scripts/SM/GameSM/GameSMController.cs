using System;
using IC.BaseSM;

/// <summary>
/// Classe che fa da controller della GameSM
/// </summary>
public class GameSMController : StateMachineBase
{
    /// <summary>
    /// Classe che definisce il contesto della GameSM
    /// </summary>
    public class Context : IContext
    {
        /// <summary>
        /// Riferimento al controller della SM
        /// </summary>
        GameSMController smController;
        /// <summary>
        /// Riferimento al GameManager
        /// </summary>
        GameManager gameMng;

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="_smController"></param>
        public Context(GameSMController _smController, GameManager _gameMng)
        {
            smController = _smController;
            gameMng = _gameMng;
        }

        /// <summary>
        /// Funzione che ritorna il riferimento al GameSMController
        /// </summary>
        /// <returns></returns>
        public GameSMController GetSMController()
        {
            return smController;
        }

        /// <summary>
        /// Funzione che ritorna il riferimento al GameManager
        /// </summary>
        /// <returns></returns>
        public GameManager GetGameManager()
        {
            return gameMng;
        }
    }

    private Context currentContext;

    public override void Setup(IContext _context)
    {
        base.Setup(_context);
        currentContext = _context as Context;
    }

    /// <summary>
    /// Funzione che gestisce l'evento OnStateComplete
    /// </summary>
    /// <param name="_state"></param>
    /// <param name="_exitCondition"></param>
    protected override void HandleOnStateComplete(IState _state, int _exitCondition)
    {
        switch (_state.GetID())
        {
            case "Setup":
                GoToState("MainMenu");
                break;
            case "MainMenu":
                GoToState("Gameplay");
                break;
            case "Gameplay":
                GoToState("EndGame");
                break;
            case "EndGame":
                if (_exitCondition == 1)
                    GoToState("MainMenu");
                else if (_exitCondition == 2)
                    GoToState("Gameplay");
                break;
        }
    }
}