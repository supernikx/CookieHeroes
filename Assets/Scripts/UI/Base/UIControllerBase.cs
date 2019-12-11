using UnityEngine;

namespace IC.UIBase
{
    /// <summary>
    /// Classe che fa da base per tutti i panneli principali di menù
    /// </summary>
    public abstract class UIControllerBase : MonoBehaviour
    {
        /// <summary>
        /// Riferimento al prorpio manager
        /// </summary>
        protected UIManagerBase manager;

        /// <summary>
        /// Stato di attivo o disattivo del menù
        /// </summary>
        protected bool isActive;   
        /// <summary>
        /// Stato di enable o disable del menù
        /// </summary>
        protected bool isEnable;

        /// <summary>
        /// Setup del menu
        /// </summary>
        public void Setup(UIManagerBase _manager)
        {
            manager = _manager;
            CustomSetup();
        }

        /// <summary>
        /// Funzione chimata al setup della classe base
        /// </summary>
        public virtual void CustomSetup() { }

        /// <summary>
        /// Funzione che ritorna true se il menù è attivo, false altrimenti
        /// </summary>
        /// <returns></returns>
        public bool IsActive()
        {
            return isActive;
        }

        /// <summary>
        /// Funzione che ritorna true se il menù è abilitato
        /// </summary>
        /// <returns></returns>
        public bool IsEnable()
        {
            return isEnable;
        }

        /// <summary>
        /// Funzion che attiva o disattiva il GameObject del menù
        /// </summary>
        /// <param name="_value"></param>
        public virtual void ToggleMenu(bool _value)
        {
            isActive = _value;
            gameObject.SetActive(isActive);
        }

        /// <summary>
        /// Funzione che disabilita gli input del pannello
        /// </summary>
        /// <param name="_enable"></param>
        public virtual void Enable(bool _enable)
        {
            isEnable = _enable;
        }
    }
}