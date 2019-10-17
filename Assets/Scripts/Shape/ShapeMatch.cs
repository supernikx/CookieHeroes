using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMatch : MonoBehaviour
{
    public static Action<ShapeMatch> ShapeDestroied;

    [SerializeField]
    private float shapeSpeed;

    ShapeMatchScriptable shape;
    SpriteRenderer spriteRenderer;
    bool isSetupped = false;

    public void Setup(ShapeMatchScriptable _shape)
    {
        shape = _shape;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = shape.sprite;

        isSetupped = true;
    }

    private void FixedUpdate()
    {
        if (!isSetupped)
            return;

        transform.Translate(Vector3.down * shapeSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.GetCurrentShape() == shape.shape)
            {
                DestroyShape();
            }
            else
            {
                player.Die();
            }
        }
    }

    public void DestroyShape()
    {
        Destroy(gameObject);
        ShapeDestroied?.Invoke(this);
    }
}
