using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IC.UIBase;

public class UIMenu_Gameplay : UIControllerBase
{
    [SerializeField]
    private Image rightShape;
    [SerializeField]
    private Image currentShape;
    [SerializeField]
    private Image leftShape;

    public void UpdateShape(ShapeScriptable _currentShape, ShapeScriptable _previousShape, ShapeScriptable _nextShape)
    {
        rightShape.sprite = _nextShape.uiShapeSprite;
        currentShape.sprite = _currentShape.uiShapeSprite;
        leftShape.sprite = _previousShape.uiShapeSprite;
    }
}
