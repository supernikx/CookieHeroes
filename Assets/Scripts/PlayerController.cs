using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<ShapeScriptable> shapes;

    private SpriteRenderer spriteRenderer;
    private int shapeIndex = 0;

    public void Setup()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = shapes[shapeIndex].shapeSprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeShape(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeShape(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeShape(2);
    }

    private void ChangeShape(int _shapeIndex)
    {
        shapeIndex = _shapeIndex;
        spriteRenderer.sprite = shapes[shapeIndex].shapeSprite;
    }

    public ShapeScriptable GetCurrentShape()
    {
        return shapes[shapeIndex];
    }
}
