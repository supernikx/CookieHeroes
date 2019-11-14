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
    SpriteMask spriteMask;
    SpriteRenderer spriteRenderer;
    Bounds bound;
    bool isSetupped = false;

    public void Setup(ShapeScriptable _shape, Camera _cam)
    {
        shape = _shape;
        spriteMask = GetComponent<SpriteMask>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = shape.guessShapeSprite;
        bound = _cam.GetOrthographicBounds();

        isSetupped = true;
    }

    private void FixedUpdate()
    {
        if (!isSetupped)
            return;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, shapeSpeed);
        if (transform.position.y < bound.center.y - bound.extents.y - 1f)
            DestroyShape();
    }

    public bool CheckShape(ShapeScriptable _shape)
    {
        if (ShapeController.GetCurrentShape() == shape)
        {
            spriteMask.sprite = shape.shadowSprite;
            spriteRenderer.sprite = shape.shapeSprite;
            return true;
        }

        return false;
    }

    public void DestroyShape()
    {
        ShapeDestroied?.Invoke(this);
        Destroy(gameObject);
    }
}
