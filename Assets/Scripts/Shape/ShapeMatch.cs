using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMatch : MonoBehaviour
{
    public static Action<ShapeMatch> ShapeDestroied;

    [SerializeField]
    private float shapeSpeed;

    ShapeScriptable shape;
    SpriteRenderer spriteRenderer;
    bool isSetupped = false;

    public void Setup(ShapeScriptable _shape)
    {
        shape = _shape;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = shape.guessShapeSprite;

        isSetupped = true;
    }

    private void FixedUpdate()
    {
        if (!isSetupped)
            return;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, shapeSpeed);
    }

    public bool CheckShape(ShapeScriptable _shape)
    {
        if (ShapeController.GetCurrentShape() == shape)
        {
            spriteRenderer.sprite = shape.shapeSprite;
            return true;
        }

        return false;
    }

    public void DestroyShape()
    {
        Destroy(gameObject);
        ShapeDestroied?.Invoke(this);
    }
}
