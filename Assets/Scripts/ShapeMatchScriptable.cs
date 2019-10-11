using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShapeMatch", menuName = "Shapes/NewShapeMatch")]
public class ShapeMatchScriptable : ScriptableObject
{
    public ShapeScriptable shape;
    public Sprite sprite;
}
