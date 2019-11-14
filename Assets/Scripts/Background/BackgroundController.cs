using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    Vector3 startPos;
    float screenHeight;
    Bounds bound;
    BackgroundManager bgManager;
    Camera cam;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Setup(BackgroundManager _bgMng, Camera _cam)
    {
        bgManager = _bgMng;
        cam = _cam;
        bound = GetComponent<SpriteRenderer>().bounds;

        screenHeight = cam.orthographicSize - cam.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bgManager == null || !bgManager.GetCanMove())
            return;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, bgManager.GetMovementSpeed());
        if (CheckScreenPosition())
            bgManager.RespawnBG(this);
    }

    private bool CheckScreenPosition()
    {
        if (bound.extents.y + transform.position.y < -screenHeight)
            return true;
        return false;
    }

    public void ResetBG()
    {
        transform.position = startPos;
    }
}
