using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IC.UIBase;

public class UI_Manager : UIManagerBase
{
    private Animator anim;
    private UIControllerBase currentAnimPanel;
    private Action currentAnimPanelCallback;

    protected override void CustomSetup()
    {
        base.CustomSetup();
        anim = GetComponent<Animator>();
        currentAnimPanel = null;
        currentAnimPanelCallback = null;
    }

    /// <summary>
    /// Funzione che setta il menù corrente a quello del tipo passato.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>Ritorna la conferma dell'azione eseguita</returns>
    public bool SetCurrentMenuAnimation<T>(string _animationID, Action _animationCallback = null) where T : UIControllerBase
    {
        UIControllerBase menuToSet = GetMenu<T>();
        if (menuToSet == null)
        {
            // non ho trovato il tipo del menù
            return false;
        }
        else
        {
            // cambio effettivamente menù
            for (int i = 0; i < menus.Count; i++)
                menus[i].ToggleMenu(false);

            currentMenu = menuToSet;
            currentAnimPanel = currentMenu;
            currentAnimPanelCallback = _animationCallback;
            anim.SetTrigger(_animationID);
            currentMenu.ToggleMenu(true);
            currentAnimPanel.Enable(false);
            OnCurrentMenuChange(currentMenu);
            return true;
        }
    }

    public void EndAnimationCallback()
    {
        anim.SetTrigger("GoToEmpty");
        currentAnimPanel.Enable(true);
        currentAnimPanel = null;

        currentAnimPanelCallback?.Invoke();
        currentAnimPanelCallback = null;
    }
}
