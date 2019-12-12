using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrintController : MonoBehaviour
{
    public static Action OnShapeGuessed;
    public static Action OnShapeWrong;

    [Header("Reference")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GenericSoundController soundCtrl;

    [Header("Print Settings")]
    [SerializeField]
    private GameObject graphic;
    [SerializeField]
    private Sprite redButtonSprite;
    [SerializeField]
    private Sprite greenButtonSprite;
    [SerializeField]
    private Image rightButtonImage;
    [SerializeField]
    private Image leftButtonImage;    
    
    private bool readCollision;
    private Sprite defaultLeftSprite;
    private Sprite defaultRightSprite;
    private IEnumerator rightShapeRoutine;

    public void Setup()
    {
        defaultLeftSprite = greenButtonSprite;
        defaultRightSprite = greenButtonSprite;
        readCollision = true;
    }

    public void EnableGraphic(bool _enable)
    {
        graphic.SetActive(_enable);
    }

    public void PlayFeedback()
    {
        soundCtrl.PlayClip();
        Vibration.CreateOneShot((long)2f, 5);
    }

    public void EndGameAnimation(Action _animationEndCallback)
    {
        StartCoroutine(EndGameAnimationCoroutine(_animationEndCallback));
    }

    private IEnumerator EndGameAnimationCoroutine(Action _animationEndCallback)
    {
        readCollision = false;
        rightButtonImage.gameObject.SetActive(true);
        leftButtonImage.gameObject.SetActive(true);
        rightButtonImage.sprite = redButtonSprite;
        leftButtonImage.sprite = redButtonSprite;

        for (int i = 0; i < 3; i++)
        {
            rightButtonImage.gameObject.SetActive(true);
            leftButtonImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            rightButtonImage.gameObject.SetActive(false);
            leftButtonImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

        rightButtonImage.sprite = greenButtonSprite;
        leftButtonImage.sprite = greenButtonSprite;
        rightButtonImage.gameObject.SetActive(true);
        leftButtonImage.gameObject.SetActive(false);

        _animationEndCallback?.Invoke();
        readCollision = true;
    }

    private IEnumerator RightShapeCoroutine()
    {
        leftButtonImage.gameObject.SetActive(true);
        rightButtonImage.gameObject.SetActive(true);
        leftButtonImage.sprite = greenButtonSprite;
        rightButtonImage.sprite = greenButtonSprite;

        yield return new WaitForSeconds(0.1f);
        leftButtonImage.gameObject.SetActive(false);
        rightButtonImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.15f);

        leftButtonImage.sprite = greenButtonSprite;
        leftButtonImage.gameObject.SetActive(false);
        rightButtonImage.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!readCollision)
            return;

        ShapeMatch shape = collision.GetComponent<ShapeMatch>();

        if (shape != null)
        {
            if (rightShapeRoutine != null)
                StopCoroutine(rightShapeRoutine);

            anim.SetTrigger("Print");

            if (shape.CheckShape(ShapeController.GetCurrentShape()))
            {
                rightShapeRoutine = RightShapeCoroutine();
                StartCoroutine(rightShapeRoutine);
                OnShapeGuessed?.Invoke();
                return;
            }

            OnShapeWrong?.Invoke();
        }
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}
