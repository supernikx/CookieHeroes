using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class PrintController : MonoBehaviour
{
    public static Action OnShapeGuessed;
    public static Action OnShapeWrong;
    private PlayableDirector director;

    private bool readCollision;
    private Collider2D oldCollision;

    public void Setup()
    {
        director = GetComponent<PlayableDirector>();
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
        if (!readCollision || (oldCollision != null && oldCollision == collision))
            return;

        ShapeMatch shape = collision.GetComponent<ShapeMatch>();

        if (shape != null)
        {
            director.Play();

            if (shape.CheckShape(ShapeController.GetCurrentShape()))
            {
                OnShapeGuessed?.Invoke();
                return;
            }

            OnShapeWrong?.Invoke();
        }

        oldCollision = collision;
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}
