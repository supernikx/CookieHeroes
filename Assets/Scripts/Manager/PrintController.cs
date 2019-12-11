using System;
using System.Collections;
using UnityEngine;

public class PrintController : MonoBehaviour
{
    public static Action OnShapeGuessed;
    public static Action OnShapeWrong;

    [Header("Print Settings")]
    [SerializeField]
    private GameObject graphic;

    private Animator anim;
    private GenericSoundController soundCtrl;
    private bool readCollision;

    public void Setup()
    {
        anim = GetComponent<Animator>();
        soundCtrl = GetComponent<GenericSoundController>();
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
        yield return null;
        readCollision = true;
        _animationEndCallback?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!readCollision)
            return;

        ShapeMatch shape = collision.GetComponent<ShapeMatch>();

        if (shape != null)
        {
            anim.SetTrigger("Print");

            if (shape.CheckShape(ShapeController.GetCurrentShape()))
            {
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
