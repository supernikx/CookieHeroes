using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    private static ShapeController i;

    #region Action
    public static Action<ShapeMatchScriptable> OnNewShapeAdd;
    #endregion

    [SerializeField]
    private List<ShapeMatchScriptable> startShapes;
    [SerializeField]
    private List<ShapeMatchScriptable> addShapes;

    private GameManager gm;
    private List<ShapeMatchScriptable> shapesToAdd;
    private List<ShapeMatchScriptable> currentShapes;

    public void Setup(GameManager _gm)
    {
        i = this;
        gm = _gm;

        shapesToAdd = addShapes;
        currentShapes = startShapes;

        gm.OnGameEnd += HandleGameEnd;
    }

    public static ShapeMatchScriptable GetRandomShapeMatch()
    {
        return i.currentShapes[UnityEngine.Random.Range(0, i.currentShapes.Count)];
    }

    public static ShapeScriptable GetShapeByIndex(int _shapeIndex)
    {
        _shapeIndex = FixShapeIndex(_shapeIndex);
        return i.currentShapes[_shapeIndex].shape;
    }

    public static int GetIndexByShape(ShapeMatchScriptable _shape)
    {
        for (int j = 0; j < i.currentShapes.Count; j++)
        {
            if (i.currentShapes[j] == _shape)
                return j;
        }

        return 0;
    }

    public static int FixShapeIndex(int _shapeIndex)
    {
        if (_shapeIndex > i.currentShapes.Count - 1)
            return 0;

        if (_shapeIndex < 0)
            return i.currentShapes.Count - 1;

        return _shapeIndex;
    }

    public static void AddNewShape()
    {
        ShapeMatchScriptable shapeToAdd = i.shapesToAdd[0];
        i.shapesToAdd.RemoveAt(0);
        i.currentShapes.Add(shapeToAdd);
        OnNewShapeAdd?.Invoke(shapeToAdd);
    }

    private void HandleGameEnd()
    {
        shapesToAdd = addShapes;
        currentShapes = startShapes;
    }
}
