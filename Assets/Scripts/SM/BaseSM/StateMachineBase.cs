using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace IC.BaseSM
{
    /// <summary>
    /// Classe che definisce le funzionalità base di una state mahcine
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public abstract class StateMachineBase : MonoBehaviour
    {
        #region Actions
        public Action<IState> OnStateEnter;
        public Action<IState> OnStateExit;
        #endregion

        /// <summary>
        /// Riferimento all'animator collegato a questa state machine
        /// </summary>
        protected Animator stateMachine;
        /// <summary>
        /// Riferimento al contesto della state machine
        /// </summary>
        protected IContext context;
        /// <summary>
        /// Lista degli stati
        /// </summary>
        protected List<IState> states;
        /// <summary>
        /// Riferimento allo stato attivo
        /// </summary>
        protected IState currentState;
        /// <summary>
        /// Riferimento allo stato precedente
        /// </summary>
        protected IState previousState;

        /// <summary>
        /// Funzione che esegue il setp della state machine
        /// </summary>
        public virtual void Setup(IContext _context)
        {
            stateMachine = GetComponent<Animator>();
            context = _context;

            //Prendo tutti gli stati dall'animator come IState
            states = stateMachine.GetBehaviours<StateBase>().ToList<IState>();

            //Setup di tutti gli stati
            for (int i = 0; i < states.Count; i++)
            {
                states[i].Setup(context, OnStateStart, OnStateEnd);
                states[i].OnStateComplete += HandleOnStateComplete;
            }
        }

        /// <summary>
        /// Funzione che esegue l'unsetup della state machine
        /// </summary>
        public virtual void Unsetup()
        {
            //Disiscrizione evento
            for (int i = 0; i < states.Count; i++)
            {
                states[i].OnStateComplete -= HandleOnStateComplete;
            }
        }

        /// <summary>
        /// Funzione che setta il trigger GoToNext
        /// </summary>
        public void GoToNext()
        {
            if (stateMachine != null)
               stateMachine.SetTrigger("GoToNext");
        }

        /// <summary>
        /// Funzione che setta il trigger relativo al nome dello stato passato.
        /// </summary>
        public void GoToState(string _stateID, int _layerIndex = 0)
        {
            if (stateMachine != null && !stateMachine.GetCurrentAnimatorStateInfo(_layerIndex).IsName(_stateID))
                stateMachine.SetTrigger("GoTo" + _stateID);
        }

        /// <summary>
        /// Funzione chiamata che notifica l'ingresso in un nuovo stato
        /// </summary>
        /// <param name="_startedState">Il nuovo stato</param>
        protected virtual void OnStateStart(IState _startedState)
        {
            currentState = _startedState;
            OnStateEnter?.Invoke(currentState);
        }

        /// <summary>
        /// Funzione che notifica l'usscita da uno stato
        /// </summary>
        /// <param name="_endedState">Lo stato che ha appena finito di eseguire le sue funzioni</param>
        protected virtual void OnStateEnd(IState _endedState)
        {
            previousState = _endedState;
            OnStateExit?.Invoke(previousState);
        }

        /// <summary>
        /// Funzione che gestisce l'evento OnStateComplete
        /// </summary>
        /// <param name="_state"></param>
        /// <param name="_exitCondition"></param>
        protected virtual void HandleOnStateComplete(IState _state, int _exitCondition)
        {

        }

        /// <summary>
        /// Funzione che ritorna il riferimento allo stato corrente
        /// </summary>
        /// <returns></returns>
        public IState GetCurrentState()
        {
            return currentState;
        }

        /// <summary>
        /// Funzione che ritorna il riferimento allo stato precedente
        /// </summary>
        /// <returns></returns>
        public IState GetPreviousState()
        {
            return previousState;
        }
    }
}