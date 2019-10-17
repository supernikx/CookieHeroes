using System;

namespace IC.BaseSM
{
    /// <summary>
    /// Interfaccia che definisce il comportamento di uno stato
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Funzione che ritorna il riferimento all'ID dello stato
        /// </summary>
        /// <returns></returns>
        string GetID();
        /// <summary>
        /// Funzione che ritorna il riferimento al Context
        /// </summary>
        /// <returns></returns>
        IContext GetContext();
        /// <summary>
        /// Funzione chiamata all'ingresso dello stato
        /// </summary>
        void Enter();
        /// <summary>
        /// Funzione chiamata all'update dello stato
        /// </summary>
        void Tick();
        /// <summary>
        /// Funzione chiamata all'uscita dello stato
        /// </summary>
        void Exit();
        /// <summary>
        /// Funzione di setup dello sato
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_onStateStartCallback"></param>
        /// <param name="_onStateEndCallback"></param>
        void Setup(IContext _context, Action<IState> _onStateStartCallback, Action<IState> _onStateEndCallback);

        /// <summary>
        /// Evento che notifica il completamento dello stato
        /// </summary>
        event IStateEventsClass.IStateEvents OnStateComplete;
    }

    public class IStateEventsClass
    {
        public delegate void IStateEvents(IState _state, int _exitCondition = 0);
    }
}
