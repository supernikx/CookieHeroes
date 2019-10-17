using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<ShapeScriptable> OnShapeChanged;

    [SerializeField]
    private List<ShapeScriptable> shapes;

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
        spriteRenderer.sprite = shapes[shapeIndex].shapeSprite;
        inputEnabled = spriteRenderer.enabled = true;
    }

    public void ChangeShape(int _shapeIndex)
    {
        if (!inputEnabled)
            return;

        shapeIndex = FixShapeIndex(_shapeIndex);
        spriteRenderer.sprite = shapes[shapeIndex].shapeSprite;
        OnShapeChanged?.Invoke(shapes[shapeIndex]);
    }

    public ShapeScriptable GetShapeByIndex(int _shapeIndex)
    {
        return shapes[FixShapeIndex(_shapeIndex)];
    }

    public ShapeScriptable GetCurrentShape()
    {
        return shapes[shapeIndex];
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

    private int FixShapeIndex(int _shapeIndex)
    {
        if (_shapeIndex > shapes.Count - 1)
            return 0;

        if (_shapeIndex < 0)
            return shapes.Count - 1;

        return _shapeIndex;
    }
}
