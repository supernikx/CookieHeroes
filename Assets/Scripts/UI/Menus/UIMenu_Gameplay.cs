using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using IC.UIBase;
using TMPro;

public class UIMenu_Gameplay : UIControllerBase
{
    [Header("Score Reference")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Shape Reference")]
    [SerializeField]
    private Image rightShape;
    [SerializeField]
    private Image currentShape;
    [SerializeField]
    private Image leftShape;

    [Header("Video Reward")]
    [SerializeField]
    private GameObject videoRewardPanel;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private float panelDuration;

    public override void CustomSetup()
    {
        videoRewardPanel.SetActive(false);
    }

    public void UpdateShape(ShapeScriptable _currentShape, ShapeScriptable _previousShape, ShapeScriptable _nextShape)
    {
        rightShape.sprite = _nextShape.uiShapeSprite;
        currentShape.sprite = _currentShape.uiShapeSprite;
        leftShape.sprite = _previousShape.uiShapeSprite;
    }

    public void UpdateScore(int _newScore)
    {
        scoreText.text = "X" + _newScore.ToString();
    }

    #region Video Reward
    private IEnumerator videoRewardPanelRoutine;
    private Action<bool> OnPanelEndCallback;

    public void EnableVideoRewardPanel(Action<bool> _OnPanelEnd)
    {
        OnPanelEndCallback = _OnPanelEnd;
        videoRewardPanelRoutine = VideoRewardPanelCoroutine();
        StartCoroutine(videoRewardPanelRoutine);
    }

    public void PlayVideoButton()
    {
        if (videoRewardPanelRoutine != null)
            StopCoroutine(videoRewardPanelRoutine);

        videoRewardPanel.SetActive(false);
        OnPanelEndCallback?.Invoke(true);
    }

    private IEnumerator VideoRewardPanelCoroutine()
    {
        videoRewardPanel.SetActive(true);
        float timer = 0;
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();

        while (timer < panelDuration)
        {
            timer += Time.unscaledDeltaTime;
            fillImage.fillAmount = 1 - timer / panelDuration;
            yield return wfeof;
        }

        videoRewardPanel.SetActive(false);
        OnPanelEndCallback?.Invoke(false);
    }
    #endregion
}
