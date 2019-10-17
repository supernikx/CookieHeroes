using System;
using UnityEngine;

namespace IC.BaseSM
{
    /// <summary>
    /// Classe che definisce le funzionalità base di uno stato
    /// </summary>
    public abstract class StateBase : StateMachineBehaviour, IState
    {
        /// <summary>
        /// ID per la State Machine dello stato
        /// Racchiuso dalla property.
        /// </summary>
        [SerializeField]
        protected string id;

        /// <summary>
        /// Riferimento al contex
        /// </summary>
        protected IContext context;
        /// <summary>
        /// Callback chiamata a inizio stato
        /// </summary>
        protected Action<IState> onStateStartCallback;
        /// <summary>
        /// Callback chiamata a fine stato
        /// </summary>
        protected Action<IState> onStateEndCallback;

        /// <summary>
        /// Evento che notifica il completamento dello stato
        /// </summary>
        public event IStateEventsClass.IStateEvents OnStateComplete;

        /// <summary>
        /// Funzione che ritorna il riferimento all'ID dello stato
        /// </summary>
        /// <returns></returns>
        public string GetID()
        {
            return id;
        }
        /// <summary>
        /// Funzione che ritorna il riferimento al Context
        /// </summary>
        /// <returns></returns>
        public IContext GetContext()
        {
            return context;
        }

        #region Base Behaviour Methods
        /// <summary>
        /// Funzione di setup dello stato
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_onStateStartCallback"></param>
        /// <param name="_onStateEndCallback"></param>
        public virtual void Setup(IContext _context, Action<IState> _onStateStartCallback, Action<IState> _onStateEndCallback)
        {
            context = _context;
            onStateStartCallback = _onStateStartCallback;
            onStateEndCallback = _onStateEndCallback;
        }
        /// <summary>
        /// Funzione chiamata all'ingresso dello stato
        /// </summary>
        public virtual void Enter()
        {

        }

        /// <summary>
        /// Funzione chiamata all'update dello stato
        /// </summary>
        public virtual void Tick() { }
        /// <summary>
        /// Funzione chiamata all'uscita dello stato
        /// </summary>
        public virtual void Exit() { }

        /// <summary>
        /// Funzione che completa lo stato
        /// </summary>
        public void Complete(int _exitCondition = 0)
        {
            OnStateComplete?.Invoke(this, _exitCondition);
        }
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity Animator OnStateEnter
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            onStateStartCallback?.Invoke(this);
            Enter();
        }

        /// <summary>
        /// Unity Animator OnStateUpdate
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Tick();
        }

        /// <summary>
        /// Unity Animator OnStateExit
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Exit();
            onStateEndCallback?.Invoke(this);
        }

        /// <summary>
        /// Unity OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            Exit();
        }
        #endregion
    }
}