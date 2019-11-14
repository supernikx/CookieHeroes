using System;
using System.Collections;
using UnityEngine;

public class PrintController : MonoBehaviour
{
    public static Action OnShapeGuessed;
    public static Action OnShapeWrong;
    private Animator anim;

    private bool readCollision;

    public void Setup()
    {
        anim = GetComponent<Animator>();
        readCollision = true;
    }

    public void EndGameAnimation(Action _animationEndCallback)
    {
        StartCoroutine(EndGameAnimationCoroutine(_animationEndCallback));
    }

    private IEnumerator EndGameAnimationCoroutine(Action _animationEndCallback)
    {
        readCollision = false;
        yield return new WaitForSeconds(1f);
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
