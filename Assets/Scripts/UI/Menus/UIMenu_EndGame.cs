using System;
using System.Collections;
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
            StartCoroutine(CounterCoroutine(manager.GetGameManager().GetScoreController().GetCurrentScore()));
        }
    }

    public void RetryButton()
    {
        RetyButtonPressed?.Invoke();
    }

    private IEnumerator CounterCoroutine(int _cookieCount)
    {
        int cookieCount = 0;
        cookieCounterText.gameObject.transform.localScale = cookieCounterText.gameObject.transform.localScale * 1.2f;
        while (cookieCount < _cookieCount)
        {
            cookieCounterText.text = "X" + cookieCount;
            yield return null;
            cookieCount+=5;
        }
        cookieCounterText.gameObject.transform.localScale = cookieCounterText.gameObject.transform.localScale / 1.2f;
        cookieCounterText.text = "X" + cookieCount;
    }
}
