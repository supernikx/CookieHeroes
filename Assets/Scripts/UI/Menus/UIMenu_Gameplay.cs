using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IC.UIBase;
using TMPro;

public class UIMenu_Gameplay : UIControllerBase
{
    [Header("Score Reference")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Shape Reference")]
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

    public void UpdateScore(int _newScore)
    {
        scoreText.text = "X" + _newScore.ToString();
    }
}
