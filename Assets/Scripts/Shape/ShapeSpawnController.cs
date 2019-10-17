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

    private List<ShapeMatch> spawnedShapes;
    private IEnumerator spawnWaveRoutine;

    public void StartSpawn()
    {
        ShapeMatch.ShapeDestroied += HandleShapeDestroyed;

        spawnedShapes = new List<ShapeMatch>();
        spawnWaveRoutine = SpawnShapeCoroutine();
        StartCoroutine(spawnWaveRoutine);
    }

    private IEnumerator SpawnShapeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShapes);
            ShapeMatchScriptable shapeToSpawn = shapes[Random.Range(0, shapes.Count)];
            ShapeMatch newShape = Instantiate(shapePrefab, transform.position, Quaternion.identity, transform);
            newShape.Setup(shapeToSpawn);
            spawnedShapes.Add(newShape);
        }
    }

    private void HandleShapeDestroyed(ShapeMatch _shapeDestroied)
    {
        spawnedShapes.Remove(_shapeDestroied);
    }

    public void StopSpawn()
    {
        if (spawnWaveRoutine != null)
            StopCoroutine(spawnWaveRoutine);

        if (spawnedShapes != null && spawnedShapes.Count > 0)
        {
            for (int i = spawnedShapes.Count - 1; i >= 0; i--)
                spawnedShapes[i].DestroyShape();
        }

        ShapeMatch.ShapeDestroied -= HandleShapeDestroyed;
    }
}
