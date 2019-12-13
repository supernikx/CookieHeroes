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
    private float cookieCounterDuration = 5f;
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
            cookieCounterText.text = "0";
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
        int start = cookieCount;
        bool highScore = scoreCtrl.CheckHighScore();        

        for (float timer = 0; timer < cookieCounterDuration; timer += Time.deltaTime)
        {
            float progress = timer / cookieCounterDuration;
            cookieCount = (int)Mathf.Lerp(start, _cookieCount, progress);
            cookieCounterText.text = cookieCount.ToString();
            yield return null;
        }
        cookieCount = _cookieCount;
        cookieCounterText.text = cookieCount.ToString();

        highScoreText.SetActive(highScore);
    }
}
