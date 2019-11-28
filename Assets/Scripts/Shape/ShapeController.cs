using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    private static ShapeController i;

    #region Action
    public static Action<Direction, ShapeScriptable, bool> OnShapeChanged;
    public static Action<ShapeScriptable> OnNewShapeAdd;
    #endregion

    [SerializeField]
    private List<ShapeScriptable> startShapes;
    [SerializeField]
    private List<ShapeScriptable> addShapes;
    [SerializeField]
    private int startShapeIndex;

    private GameManager gm;
    private List<ShapeScriptable> shapesToAdd;
    private List<ShapeScriptable> currentShapes;
    private int shapeIndex = 0;

    public void Setup(GameManager _gm)
    {
        i = this;
        gm = _gm;

        shapesToAdd = new List<ShapeScriptable>(addShapes);
        currentShapes = new List<ShapeScriptable>(startShapes);

        gm.OnGameEnd += HandleGameEnd;
    }

    public static ShapeScriptable GetRandomShapeMatch()
    {
        return i.currentShapes[UnityEngine.Random.Range(0, i.currentShapes.Count)];
    }

    public static ShapeScriptable GetShapeByIndex(int _shapeIndex)
    {
        _shapeIndex = FixShapeIndex(_shapeIndex);
        return i.currentShapes[_shapeIndex];
    }

    public void ChangeShape(Direction _swipeDir, bool _animation)
    {
        switch (_swipeDir)
        {
            case Direction.Right:
                shapeIndex = FixShapeIndex(GetCurrentShapeIndex() - 1);
                break;
            case Direction.Left:
                shapeIndex = FixShapeIndex(GetCurrentShapeIndex() + 1);
                break;
        }

        ShapeScriptable newShape = GetShapeByIndex(shapeIndex);
        OnShapeChanged?.Invoke(_swipeDir, newShape, _animation);
    }

    public void ChangeShape(ShapeScriptable _shape)
    {
        shapeIndex = GetIndexByShape(_shape);
        OnShapeChanged?.Invoke(Direction.None, _shape, false);
    }

    public static int GetCurrentShapeIndex()
    {
        return i.shapeIndex;
    }

    public static ShapeScriptable GetCurrentShape()
    {
        return GetShapeByIndex(GetCurrentShapeIndex());
    }

    public static int GetIndexByShape(ShapeScriptable _shape)
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
        if (i.shapesToAdd != null && i.shapesToAdd.Count > 0)
        {
            ShapeScriptable shapeToAdd = i.shapesToAdd[0];
            i.shapesToAdd.RemoveAt(0);
            i.currentShapes.Add(shapeToAdd);
            OnNewShapeAdd?.Invoke(shapeToAdd);
        }
    }

    private void HandleGameEnd()
    {
        shapesToAdd = new List<ShapeScriptable>(addShapes);
        currentShapes = new List<ShapeScriptable>(startShapes);
        shapeIndex = startShapeIndex;
    }

    private void OnDisable()
    {
        gm.OnGameEnd -= HandleGameEnd;
    }
}
