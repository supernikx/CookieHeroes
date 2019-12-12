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

    [System.Serializable]
    private class AddShapeClass
    {
        public ShapeScriptable shape;
        public int addNewShapeAfter;
    }

    [SerializeField]
    private List<ShapeScriptable> startShapes;
    [SerializeField]
    private List<AddShapeClass> addShapes;
    [SerializeField]
    private int startShapeIndex;

    private GameManager gm;
    private List<AddShapeClass> shapesToAdd;
    private List<ShapeScriptable> currentShapes;
    private int shapeIndex = 0;
    private int shapeGuessed;

    public void Setup(GameManager _gm)
    {
        i = this;
        gm = _gm;

        shapesToAdd = new List<AddShapeClass>(addShapes);
        currentShapes = new List<ShapeScriptable>(startShapes);

        PrintController.OnShapeGuessed += HandleOnShapeGuessed;
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

    public static int GetShapeAmmount()
    {
        return i.currentShapes.Count;
    }

    public static void AddNewShape()
    {
        if (i.shapesToAdd != null && i.shapesToAdd.Count > 0)
        {
            i.shapeGuessed = 0;
            ShapeScriptable shapeToAdd = i.shapesToAdd[0].shape;
            i.shapesToAdd.RemoveAt(0);
            i.currentShapes.Add(shapeToAdd);
            OnNewShapeAdd?.Invoke(shapeToAdd);
        }
    }

    public static bool CheckNextShapeToAdd(int _shapesInGame)
    {
        if (i.shapesToAdd.Count == 0)
            return false;

        return i.shapeGuessed + _shapesInGame == i.shapesToAdd[0].addNewShapeAfter;
    }

    private void HandleGameEnd()
    {
        shapesToAdd = new List<AddShapeClass>(addShapes);
        currentShapes = new List<ShapeScriptable>(startShapes);
        shapeIndex = startShapeIndex;
        shapeGuessed = 0;
    }

    private void HandleOnShapeGuessed()
    {
        if (i.shapesToAdd.Count == 0)
            return;

        shapeGuessed++;
        if (shapeGuessed == shapesToAdd[0].addNewShapeAfter)
        {
            CoroutineController.StartRoutine(AddNewShape, 0.5f);
        }
    }

    private void OnDisable()
    {
        PrintController.OnShapeGuessed -= HandleOnShapeGuessed;
        gm.OnGameEnd -= HandleGameEnd;
    }
}
