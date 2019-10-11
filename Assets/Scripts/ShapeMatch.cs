using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMatch : MonoBehaviour
{
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
        transform.Translate(Vector3.down * shapeSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.GetCurrentShape() == shape.shape)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Hai Perso");
            }
        }
    }
}
