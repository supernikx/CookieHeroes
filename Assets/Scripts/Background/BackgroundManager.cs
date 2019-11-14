using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startBg;
    [SerializeField]
    private BackgroundController bg1;
    [SerializeField]
    private BackgroundController bg2;
    [SerializeField]
    private float backgoundDislpace = 11.5f;
    [SerializeField]
    private float maxStartPosY;

    private bool canMove = false;
    private Camera cam;
    private Vector3 startPos;
    private BackgroundController currentBG;
    private IEnumerator backgroundStartRoutine;

    public void Setup(Camera _cam)
    {
        startPos = transform.position;
        cam = _cam;
    }

    public void StartBackground()
    {
        currentBG = bg1;
        backgroundStartRoutine = BackgroundStartCoroutine();
        StartCoroutine(backgroundStartRoutine);
    }

    public void ResetBackground()
    {
        canMove = false;
        if (backgroundStartRoutine != null)
            StopCoroutine(backgroundStartRoutine);

        transform.position = startPos;
        bg1.ResetBG();
        bg2.ResetBG();
    }

    public Bounds GetBackgroundBounds()
    {
        return bg1.GetComponent<SpriteRenderer>().bounds;
    }

    private IEnumerator BackgroundStartCoroutine()
    {
        while (transform.position.y > maxStartPosY)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, DifficultyManager.GetMovementSpeed());
            yield return new WaitForFixedUpdate();
        }

        bg1.Setup(this, cam);
        bg2.Setup(this, cam);
        canMove = true;
    }

    public void RespawnBG(BackgroundController bg)
    {
        if (bg != currentBG)
            return;
        BackgroundController other = (bg == bg1) ? bg2 : bg1;
        bg.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + backgoundDislpace, other.transform.position.z);
        currentBG = other;
    }

    public bool GetCanMove()
    {
        return canMove;
    }
}
