using System;
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
    private int addNewShapeAfter;

    private Camera cam;
    private Bounds bgBounds;
    private int shapeSpawnedBeforeNewShape;
    private List<ShapeMatch> spawnedShapes;
    private IEnumerator spawnWaveRoutine;

    public void Setup(GameManager _gm)
    {
        cam = _gm.GetCamera();
        bgBounds = _gm.GetBackgroundManager().GetBackgroundBounds();
    }

    public void StartSpawn()
    {
        ShapeMatch.ShapeDestroied += HandleShapeDestroyed;

        spawnedShapes = new List<ShapeMatch>();
        spawnWaveRoutine = SpawnShapeCoroutine();
        shapeSpawnedBeforeNewShape = 0;
        StartCoroutine(spawnWaveRoutine);
    }

    private IEnumerator SpawnShapeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShapes);
            ShapeScriptable shapeToSpawn = ShapeController.GetRandomShapeMatch();

            //Calculate Random Position
            float randomXValue = UnityEngine.Random.Range((bgBounds.center.x - bgBounds.extents.x) + 1f, (bgBounds.center.x + bgBounds.extents.x) - 1f);
            Vector3 spawnVector = new Vector3(randomXValue , transform.position.y, transform.position.z);

            //Calculate Random Rotation
            float randomRoation = UnityEngine.Random.Range(-60f, 60f);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, randomRoation);

            ShapeMatch newShape = Instantiate(shapePrefab, spawnVector, spawnRotation, transform);
            newShape.Setup(shapeToSpawn, cam);
            spawnedShapes.Add(newShape);
            shapeSpawnedBeforeNewShape++;
            if (shapeSpawnedBeforeNewShape == addNewShapeAfter)
            {
                ShapeController.AddNewShape();
                shapeSpawnedBeforeNewShape = 0;
            }
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
