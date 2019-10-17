using IC.BaseSM;
using System;

/// <summary>
/// Classe che definisce lo stato base della GameSM
/// </summary>
public abstract class GameSMBaseState : StateBase
{
    /// <summary>
    /// Riferimento sovrascritto del context
    /// </summary>
    protected new GameSMController.Context context;

    /// <summary>
    /// Setup dello stato
    /// </summary>
    /// <param name="_context"></param>
    /// <param name="_onStateStartCallback"></param>
    /// <param name="_onStateEndCallback"></param>
    public override void Setup(IContext _context, Action<IState> _onStateStartCallback, Action<IState> _onStateEndCallback)
    {
        base.Setup(_context, _onStateStartCallback, _onStateEndCallback);
        context = _context as GameSMController.Context;
    }
}
