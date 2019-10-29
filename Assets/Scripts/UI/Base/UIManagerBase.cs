using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace IC.UIBase
{
    /// <summary>
    /// Manager che gestisce le funzionalità base della UI
    /// </summary>
    public abstract class UIManagerBase : MonoBehaviour
    {
        /// <summary>
        /// Immagine che esegue il fade
        /// </summary>
        [Tooltip("Immagine che esegue il fade. Può essere lasciata nulla")]
        [SerializeField] Image fadeImage;

        /// <summary>
        /// Lista di menù/pannelli
        /// </summary>
        protected List<UIControllerBase> menus;
        /// <summary>
        /// Menù attualmente selezionato
        /// </summary>
        protected UIControllerBase currentMenu;
        /// <summary>
        /// Riferimento al GameManager
        /// </summary>
        protected GameManager gm;

        /// <summary>
        /// Setup della classe
        /// </summary>
        public void Setup(GameManager _gm)
        {
            gm = _gm;
            menus = GetComponentsInChildren<UIControllerBase>(true).ToList();

            for (int i = 0; i < menus.Count; i++)
                menus[i].Setup(this);

            CustomSetup();
        }

        /// <summary>
        /// Funzione che spegne il menù corrente
        /// </summary>
        public void ClearCurrentMenu()
        {
            currentMenu.ToggleMenu(false);
            currentMenu = null;
            OnCurrentMenuChange(currentMenu);
        }

        /// <summary>
        /// Funzione che setta il menù corrente a quello del tipo passato.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Ritorna la conferma dell'azione eseguita</returns>
        public bool SetCurrentMenu<T>() where T : UIControllerBase
        {
            UIControllerBase menuToSet = GetMenu<T>();
            if (menuToSet == null)
            {
                // non ho trovato il tipo del menù
                return false;
            }
            else if (currentMenu != null && currentMenu.GetType() == typeof(T))
            {
                // sto riaccendendo il menù corrente
                return true;
            }
            else
            {
                // cambio effettivamente menù
                for (int i = 0; i < menus.Count; i++)
                    menus[i].ToggleMenu(false);

                currentMenu = menuToSet;
                currentMenu.ToggleMenu(true);
                OnCurrentMenuChange(currentMenu);
                return true;
            }
        }

        /// <summary>
        /// Funzione che setta il menù corrente a quello del tipo passato eseguendo un fade.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_fadeInTime"></param>
        /// <param name="_fadeOutTime"></param>
        /// <param name="_fadeInCallBack"></param>
        /// <param name="_fadeOutCallBack"></param>
        /// <returns></returns>
        public bool SetCurrentMenu<T>(float _fadeInTime = 0, float _fadeOutTime = 0, Action _fadeInCallBack = null, Action _fadeOutCallBack = null) where T : UIControllerBase
        {
            if (fadeImage == null)
                return false;

            // se entrambi i tempi sono minori di zero eseguo la funzione base senza fade
            if (_fadeInTime <= 0 && _fadeOutTime <= 0)
                return SetCurrentMenu<T>();
            else
            {
                UIControllerBase menuToSet = GetMenu<T>();
                if (menuToSet == null)
                {
                    // non ho trovato il tipo del menù
                    return false;
                }
                else if (currentMenu != null && currentMenu.GetType() == typeof(T))
                {
                    // sto riaccendendo il menù corrente
                    return true;
                }
                else
                {
                    // cambio effettivamente menù
                    for (int i = 0; i < menus.Count; i++)
                        menus[i].ToggleMenu(false);

                    // lancio il fade in del pannello
                    fadeImage.DOFade(1, _fadeInTime).OnComplete(() =>
                    {
                        // eseguo il cambio del menù attivo
                        currentMenu = menuToSet;
                        currentMenu.ToggleMenu(true);
                        OnCurrentMenuChange(currentMenu);

                        // al completamento del fade in, se non è nulla, esegue la callback sul fade in
                        _fadeInCallBack?.Invoke();

                        //lancio il fade out del pannello
                        fadeImage.DOFade(0, _fadeOutTime).OnComplete(() =>
                        {
                            // al completamento del fade in, se non è nulla, esegue la callback sul fade out
                            _fadeOutCallBack?.Invoke();
                        });
                    });
                    return true;
                }
            }
        }

        /// <summary>
        /// Funzione che ritorna il menu attivo
        /// </summary>
        /// <returns></returns>
        public UIControllerBase GetCurrentMenu()
        {
            return currentMenu;
        }

        /// <summary>
        /// Funzione che ritorna il Game Manager
        /// </summary>
        /// <returns></returns>
        public GameManager GetGameManager()
        {
            return gm;
        }

        /// <summary>
        /// Funzione che ritorna il menu che corrisponde al tipo passato
        /// </summary>
        /// <returns></returns>
        public T GetMenu<T>() where T : UIControllerBase
        {
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].GetType() == typeof(T))
                    return (menus[i] as T);
            }

            return default(T);
        }

        #region Virtual Methods
        /// <summary>
        /// Funzione chiamata al setup
        /// </summary>
        protected virtual void CustomSetup() { }

        /// <summary>
        /// Funzione chiamata al camcbio di menù
        /// </summary>
        protected virtual void OnCurrentMenuChange(UIControllerBase _uIController) { }
        #endregion
    }
}