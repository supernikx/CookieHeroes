using System;
using System.Collections;
using TMPro;
using UnityEngine;
using IC.UIBase;

public class UIMenu_EndGame : UIControllerBase
{
    public Action MainMenuButtonPressed;
    public Action RetyButtonPressed;

    [SerializeField]
    private TextMeshProUGUI cookieCounterText;
    [SerializeField]
    private GameObject highScoreText;
    [SerializeField]
    private GenericSoundController traySoundCtrl;

    private Vector3 defaultCookieCounterScale;
    private ScoreController scoreCtrl;
    private IEnumerator counterRoutine;

    public override void CustomSetup()
    {
        scoreCtrl = manager.GetGameManager().GetScoreController();
        defaultCookieCounterScale = cookieCounterText.gameObject.transform.localScale;
        traySoundCtrl.Init(manager.GetGameManager().GetSoundManager());
    }

    public override void ToggleMenu(bool _value)
    {
        base.ToggleMenu(_value);

        if (isActive)
        {
            highScoreText.SetActive(false);
        }
        else
        {

            if (counterRoutine != null)
                StopCoroutine(counterRoutine);

            cookieCounterText.gameObject.transform.localScale = defaultCookieCounterScale;
            highScoreText.SetActive(false);
        }
    }

    public void RetryButton()
    {
        RetyButtonPressed?.Invoke();
    }

    public void MainMenuButton()
    {
        MainMenuButtonPressed?.Invoke();
    }

    public void PlayTrayFeedback()
    {
        traySoundCtrl.PlayClip();
    }

    public void PlayCounterRoutine()
    {
        counterRoutine = CounterCoroutine(scoreCtrl.GetCurrentScore());
        StartCoroutine(counterRoutine);
    }

    private IEnumerator CounterCoroutine(int _cookieCount)
    {
        int cookieCount = 0;
        bool highScore = scoreCtrl.CheckHighScore();
        cookieCounterText.text = cookieCount.ToString();

        cookieCounterText.gameObject.transform.localScale = cookieCounterText.gameObject.transform.localScale * 1.2f;
        while (cookieCount < _cookieCount)
        {
            cookieCounterText.text = cookieCount.ToString();
            yield return null;
            cookieCount += 25;
        }

        cookieCounterText.gameObject.transform.localScale = cookieCounterText.gameObject.transform.localScale / 1.2f;
        cookieCounterText.text = cookieCount.ToString();

        highScoreText.SetActive(highScore);
    }
}
