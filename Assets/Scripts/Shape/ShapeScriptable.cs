using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShape", menuName = "Shapes/NewShape")]
public class ShapeScriptable : ScriptableObject
{
    public Sprite guessShapeSprite;
    public Sprite shapeSprite;
    public Sprite uiShapeSprite;
}
