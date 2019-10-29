using System;
using TMPro;
using UnityEngine;
using IC.UIBase;

public class UIMenu_EndGame : UIControllerBase
{
    public Action RetyButtonPressed;

    [SerializeField]
    private TextMeshProUGUI cookieCounterText;

    public override void ToggleMenu(bool _value)
    {
        base.ToggleMenu(_value);

        if (isActive)
        {
            cookieCounterText.text = "X" + manager.GetGameManager().GetScoreController().GetCurrentScore().ToString();
        }
    }

    public void RetryButton()
    {
        RetyButtonPressed?.Invoke();
    }
}
