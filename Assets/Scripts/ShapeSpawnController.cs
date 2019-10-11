using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawnController : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenShapes;
    [SerializeField]
    private ShapeMatch shapePrefab;
    [SerializeField]
    private List<ShapeMatchScriptable> shapes;


    public void Setup()
    {
        StartCoroutine(SpawnShapeCoroutine());
    }

    private IEnumerator SpawnShapeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShapes);
            ShapeMatchScriptable shapeToSpawn = shapes[Random.Range(0, shapes.Count)];
            ShapeMatch newShape = Instantiate(shapePrefab, transform.position, Quaternion.identity, transform);
            newShape.Setup(shapeToSpawn);
        }
    }
}
