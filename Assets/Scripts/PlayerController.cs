using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<ShapeScriptable> OnShapeChanged;

    private SpriteRenderer spriteRenderer;
    private bool inputEnabled;
    private int shapeIndex = 0;

    public void Setup()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Disable();
    }

    public void Enable()
    {
        spriteRenderer.sprite = GetCurrentShape().shapeSprite;
        inputEnabled = spriteRenderer.enabled = true;
    }

    public void ChangeShape(int _shapeIndex)
    {
        if (!inputEnabled)
            return;

        shapeIndex = ShapeController.FixShapeIndex(_shapeIndex);
        ShapeScriptable newShape = ShapeController.GetShapeByIndex(shapeIndex);
        spriteRenderer.sprite = newShape.shapeSprite;
        OnShapeChanged?.Invoke(newShape);
    }

    public ShapeScriptable GetCurrentShape()
    {
        return ShapeController.GetShapeByIndex(shapeIndex);
    }

    public int GetCurrentShapeIndex()
    {
        return shapeIndex;
    }

    public void Die()
    {
        GameManager.GameOver();
    }

    public void Disable()
    {
        inputEnabled = spriteRenderer.enabled = false;
    }
}
