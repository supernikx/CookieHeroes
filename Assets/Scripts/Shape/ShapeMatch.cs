using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMatch : MonoBehaviour, IPoolObject
{
    #region Pool
    public event PoolManagerEvets.Events OnObjectSpawn;
    public event PoolManagerEvets.Events OnObjectDestroy;

    private GameObject ownerObject;
    public GameObject OwnerObject
    {
        get
        {
            return ownerObject;
        }
        set
        {
            ownerObject = value;
        }
    }

    private State currentState;
    public State CurrentState 
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }
    #endregion

    public static Action<ShapeMatch> ShapeDestroied;

    ShapeScriptable shape;
    SpriteMask spriteMask;
    SpriteRenderer spriteRenderer;
    Bounds bound;
    bool isSetupped = false;
    bool isToGuess = true;

    public void Setup(ShapeScriptable _shape, Camera _cam)
    {
        shape = _shape;
        spriteMask = GetComponent<SpriteMask>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = shape.guessShapeSprite;
        bound = _cam.GetOrthographicBounds();

        OnObjectSpawn?.Invoke(this);
        isToGuess = true;
        isSetupped = true;
    }

    private void FixedUpdate()
    {
        if (!isSetupped)
            return;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, DifficultyManager.GetMovementSpeed());
        if (transform.position.y < bound.center.y - bound.extents.y - 1f)
            DestroyShape();
    }

    public bool CheckShape(ShapeScriptable _shape)
    {
        isToGuess = false;

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
        OnObjectDestroy?.Invoke(this);

        shape = null;
        spriteMask.sprite = null;
        isSetupped = false;
    }

    public bool IsToGuess()
    {
        return isToGuess;
    }
}
