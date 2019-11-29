using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using IC.UIBase;
using TMPro;
using UnityEngine.Advertisements;
using DG.Tweening;

public class UIMenu_Gameplay : UIControllerBase
{
    [Header("Tutorial Refrence")]
    [SerializeField]
    private GameObject tutorialPanel;

    [Header("Score Reference")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Feedback References")]
    [SerializeField]
    private GameObject newShapeText;

    [Header("Shape Reference")]
    [SerializeField]
    private Image rightShape;
    [SerializeField]
    private Image centerShape;
    [SerializeField]
    private Image leftShape;
    [SerializeField]
    private Image newShape;

    [Header("Shape Positions Reference")]
    [SerializeField]
    private Transform extremeRightPos;
    [SerializeField]
    private Transform extremeLeftPos;

    [Header("Video Reward")]
    [SerializeField]
    private GameObject videoRewardPanel;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private float panelDuration;

    private Vector3 rightStartPos;
    private Vector3 leftStartPos;
    private Vector3 centerStartPos;
    private Vector3 newStartPos;
    private Quaternion rightStartRot;
    private Quaternion leftStartRot;
    private Quaternion centerStartRot;
    private Quaternion newStartRot;
    private IEnumerator newShapeFeedbackRoutine;
    private IEnumerator changeShapeRoutine;

    public override void CustomSetup()
    {
        videoRewardPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        newShapeText.SetActive(false);

        rightStartPos = rightShape.transform.position;
        leftStartPos = leftShape.transform.position;
        centerStartPos = centerShape.transform.position;
        newStartPos = newShape.transform.position;

        rightStartRot = rightShape.transform.rotation;
        leftStartRot = leftShape.transform.rotation;
        centerStartRot = centerShape.transform.rotation;
        newStartRot = newShape.transform.rotation;
    }

    public override void ToggleMenu(bool _value)
    {
        base.ToggleMenu(_value);

        if (_value)
        {
            ResetUIPos();
            ShapeController.OnNewShapeAdd += HandleOnNewShapeAdd;
        }
        else
        {
            if (newShapeFeedbackRoutine != null)
                StopCoroutine(newShapeFeedbackRoutine);

            if (changeShapeRoutine != null)
                StopCoroutine(changeShapeRoutine);

            ShapeController.OnNewShapeAdd -= HandleOnNewShapeAdd;
        }
    }

    private void ResetUIPos()
    {
        rightShape.transform.position = rightStartPos;
        leftShape.transform.position = leftStartPos;
        centerShape.transform.position = centerStartPos;
        newShape.transform.position = newStartPos;
 
        rightShape.transform.rotation = rightStartRot;
        leftShape.transform.rotation = leftStartRot;
        centerShape.transform.rotation = centerStartRot;
        newShape.transform.rotation = newStartRot;
    }

    public void UpdateShape(Direction _swipeDir, ShapeScriptable _currentShape, ShapeScriptable _previousShape, ShapeScriptable _nextShape, bool _animation, float _animTime = 0f)
    {
        if (_animation)
        {
            changeShapeRoutine = ChangeShapeCoroutine(_swipeDir, _currentShape, _previousShape, _nextShape, _animTime);
            StartCoroutine(changeShapeRoutine);
        }
        else
        {
            if (changeShapeRoutine != null)
                StopCoroutine(changeShapeRoutine);

            ResetUIPos();

            rightShape.sprite = _nextShape.uiShapeSprite;
            centerShape.sprite = _currentShape.uiShapeSprite;
            leftShape.sprite = _previousShape.uiShapeSprite;
        }
    }

    public void UpdateScore(int _newScore)
    {
        scoreText.text = "X" + _newScore.ToString();
    }

    private void HandleOnNewShapeAdd(ShapeScriptable _newShape)
    {
        newShapeFeedbackRoutine = NewShapeFeedbackCoroutine();
        StartCoroutine(newShapeFeedbackRoutine);
    }

    private IEnumerator NewShapeFeedbackCoroutine()
    {
        newShapeText.SetActive(true);
        yield return new WaitForSeconds(1f);
        newShapeText.SetActive(false);
    }

    private IEnumerator ChangeShapeCoroutine(Direction _swipeDir, ShapeScriptable _currentShape, ShapeScriptable _previousShape, ShapeScriptable _nextShape, float _animTime)
    {
        switch (_swipeDir)
        {
            case Direction.Right:
                //Imposto la shape jolly iniziale
                newShape.transform.position = extremeLeftPos.position;
                newShape.transform.rotation = extremeLeftPos.rotation;
                newShape.sprite = _nextShape.uiShapeSprite;

                //Creo tutti i Tween per ogni forma posizionarsi rispetto allo swipe
                Tween newShapeToCenterPosR = newShape.transform.DOMove(leftStartPos, _animTime).SetEase(Ease.Linear);
                Tween newShapeToCenterRotR = newShape.transform.DORotateQuaternion(leftStartRot, _animTime).SetEase(Ease.Linear);
                Tween leftToCenterPosR = leftShape.transform.DOMove(centerStartPos, _animTime).SetEase(Ease.Linear);
                Tween leftToCenterRotR = leftShape.transform.DORotateQuaternion(centerStartRot, _animTime).SetEase(Ease.Linear);
                Tween centerToRightTweenPosR = centerShape.transform.DOMove(rightStartPos, _animTime).SetEase(Ease.Linear);
                Tween centerToRightTweenRotR = centerShape.transform.DORotateQuaternion(rightStartRot, _animTime).SetEase(Ease.Linear);
                Tween rightToExtremeRightTweenPosR = rightShape.transform.DOMove(extremeRightPos.position, _animTime).SetEase(Ease.Linear);
                Tween rightToExtremeRightTweenRotR = rightShape.transform.DORotateQuaternion(extremeRightPos.rotation, _animTime).SetEase(Ease.Linear);

                //Assegno i Tween alla sequenza
                Sequence rightSequence = DOTween.Sequence();

                rightSequence.Append(newShapeToCenterPosR);
                rightSequence.Insert(0, newShapeToCenterRotR);
                rightSequence.Insert(0, leftToCenterPosR);
                rightSequence.Insert(0, leftToCenterRotR);
                rightSequence.Insert(0, centerToRightTweenPosR);
                rightSequence.Insert(0, centerToRightTweenRotR);
                rightSequence.Insert(0, rightToExtremeRightTweenPosR);
                rightSequence.Insert(0, rightToExtremeRightTweenRotR);

                //Eseguo la sequenza e  aspetto il completamento
                rightSequence.Play();
                yield return rightSequence.WaitForCompletion();

                //Rimetto ogni shape nella sua giusta posizione e gli assegno la nuova sprites
                rightShape.transform.position = rightStartPos;
                rightShape.transform.rotation = rightStartRot;
                rightShape.sprite = _nextShape.uiShapeSprite;

                centerShape.transform.position = centerStartPos;
                centerShape.transform.rotation = centerStartRot;
                centerShape.sprite = _currentShape.uiShapeSprite;

                leftShape.transform.position = leftStartPos;
                leftShape.transform.rotation = leftStartRot;
                leftShape.sprite = _previousShape.uiShapeSprite;

                newShape.transform.position = newStartPos;
                newShape.transform.rotation = newStartRot;
                break;
            case Direction.Left:
                //Imposto la shape jolly iniziale
                newShape.transform.position = extremeRightPos.position;
                newShape.transform.rotation = extremeRightPos.rotation;
                newShape.sprite = _previousShape.uiShapeSprite;

                //Creo tutti i Tween per ogni forma posizionarsi rispetto allo swipe
                Tween newShapeToCenterPosL = newShape.transform.DOMove(rightStartPos, _animTime).SetEase(Ease.Linear);
                Tween newShapeToCenterRotL = newShape.transform.DORotateQuaternion(rightStartRot, _animTime).SetEase(Ease.Linear);
                Tween rightToCenterPosL = rightShape.transform.DOMove(centerStartPos, _animTime).SetEase(Ease.Linear);
                Tween rightToCenterRotL = rightShape.transform.DORotateQuaternion(centerStartRot, _animTime).SetEase(Ease.Linear);
                Tween leftToCenterPosL = centerShape.transform.DOMove(leftStartPos, _animTime).SetEase(Ease.Linear);
                Tween leftToCenterRotL = centerShape.transform.DORotateQuaternion(leftStartRot, _animTime).SetEase(Ease.Linear);
                Tween leftToExtremeLeftTweenPos = leftShape.transform.DOMove(extremeLeftPos.position, _animTime).SetEase(Ease.Linear);
                Tween leftToExtremeLeftTweenRot = leftShape.transform.DORotateQuaternion(extremeLeftPos.rotation, _animTime).SetEase(Ease.Linear);

                //Assegno i Tween alla sequenza
                Sequence lSequence = DOTween.Sequence();

                lSequence.Append(newShapeToCenterPosL);
                lSequence.Insert(0, newShapeToCenterRotL);
                lSequence.Insert(0, rightToCenterPosL);
                lSequence.Insert(0, rightToCenterRotL);
                lSequence.Insert(0, leftToCenterPosL);
                lSequence.Insert(0, leftToCenterRotL);
                lSequence.Insert(0, leftToExtremeLeftTweenPos);
                lSequence.Insert(0, leftToExtremeLeftTweenRot);

                //Eseguo la sequenza e  aspetto il completamento
                lSequence.Play();
                yield return lSequence.WaitForCompletion();

                //Rimetto ogni shape nella sua giusta posizione e gli assegno la nuova sprites
                leftShape.transform.position = leftStartPos;
                leftShape.transform.rotation = leftStartRot;
                leftShape.sprite = _previousShape.uiShapeSprite;

                centerShape.transform.position = centerStartPos;
                centerShape.transform.rotation = centerStartRot;
                centerShape.sprite = _currentShape.uiShapeSprite;

                rightShape.transform.position = rightStartPos;
                rightShape.transform.rotation = rightStartRot;
                rightShape.sprite = _nextShape.uiShapeSprite;

                newShape.transform.position = newStartPos;
                newShape.transform.rotation = newStartRot;
                break;
        }
    }

    #region Video Reward
    private IEnumerator videoRewardPanelRoutine;
    private Action<bool> OnPanelEndCallback;

    public void EnableVideoRewardPanel(Action<bool> _OnPanelEnd)
    {
        if (newShapeFeedbackRoutine != null)
            StopCoroutine(newShapeFeedbackRoutine);

        OnPanelEndCallback = _OnPanelEnd;
        videoRewardPanelRoutine = VideoRewardPanelCoroutine();
        StartCoroutine(videoRewardPanelRoutine);
    }

    public void PlayVideoButton()
    {
        if (videoRewardPanelRoutine != null)
            StopCoroutine(videoRewardPanelRoutine);

        videoRewardPanel.SetActive(false);
        UnityAdsManager.instance.ShowRewardedAD(ADCallback);
    }

    private void ADCallback(ShowResult _results)
    {
        bool result = false;
        switch (_results)
        {
            case ShowResult.Failed:
                result = false;
                break;
            case ShowResult.Skipped:
                result = false;
                break;
            case ShowResult.Finished:
                result = true;
                break;
        }

        OnPanelEndCallback?.Invoke(result);
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

    #region Tutorial Panel
    public void EnableTutorialPanel(bool _enable)
    {
        tutorialPanel.SetActive(_enable);
    }
    #endregion
}
