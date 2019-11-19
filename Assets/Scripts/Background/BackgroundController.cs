using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    BackgroundManager bgManager;
    SpriteRenderer sr;
    float currentOffset;

    public void Setup(BackgroundManager _bgMng)
    {
        bgManager = _bgMng;
        currentOffset = 0;
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (bgManager == null || !bgManager.GetCanMove())
            return;

        sr.material.mainTextureOffset = new Vector2(0, currentOffset);
        currentOffset += (DifficultyManager.GetMovementSpeed() / 6);
    }

    public void ResetBG()
    {
        currentOffset = 0;

        if (sr != null)
            sr.material.mainTextureOffset = new Vector2(0, 0);
    }
}
