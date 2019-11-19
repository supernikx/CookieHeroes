using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startBg;
    [SerializeField]
    private BackgroundController seamlessBg;
    [SerializeField]
    private float maxStartPosY;

    private bool canMove = false;
    private Vector3 startPos;
    private IEnumerator backgroundStartRoutine;

    public void Setup()
    {
        startPos = transform.position;
    }

    public void StartBackground()
    {
        backgroundStartRoutine = BackgroundStartCoroutine();
        StartCoroutine(backgroundStartRoutine);
    }

    public void ResetBackground()
    {
        canMove = false;
        if (backgroundStartRoutine != null)
            StopCoroutine(backgroundStartRoutine);

        transform.position = startPos;
        seamlessBg.ResetBG();
    }

    public Bounds GetBackgroundBounds()
    {
        return seamlessBg.GetComponent<SpriteRenderer>().bounds;
    }

    private IEnumerator BackgroundStartCoroutine()
    {
        while (transform.position.y > maxStartPosY)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, DifficultyManager.GetMovementSpeed());
            yield return new WaitForFixedUpdate();
        }

        seamlessBg.Setup(this);
        canMove = true;
    }

    public bool GetCanMove()
    {
        return canMove;
    }
}
