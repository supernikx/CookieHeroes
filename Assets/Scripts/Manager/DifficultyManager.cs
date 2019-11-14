using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private static DifficultyManager i;

    [Header("Movement Speed")]
    [SerializeField]
    private float startMovementSpeed;
    [SerializeField]
    private float increaseMovementSpeedValue;
    [SerializeField]
    private float increaseMovementSpeedTime;
    [SerializeField]
    private float maxMovementSpeedValue;

    [Header("Spawn Rate")]
    [SerializeField]
    private float startSpawnRate;
    [SerializeField]
    private float spawnRateReduceValue;
    [SerializeField]
    private float spawnRateReduceTime;
    [SerializeField]
    private float minSpawnRateValue;

    private float movementSpeedTimer;
    private float currentMovementSpeed;

    private float currentSpawnRateTimer;
    private float currentSpawnRate;

    private bool isPlaying = false;

    private void Awake()
    {
        i = this;
    }

    public static void StartGame()
    {
        i.isPlaying = true;
        i.currentMovementSpeed = i.startMovementSpeed;
        i.currentSpawnRate = i.startSpawnRate;
    }

    public static void StopGame()
    {
        i.currentMovementSpeed = i.startMovementSpeed;
        i.currentSpawnRate = i.startSpawnRate;
        i.isPlaying = false;
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        movementSpeedTimer += Time.deltaTime;
        currentSpawnRateTimer += Time.deltaTime;

        UpdateValues();
    }

    private void UpdateValues()
    {
        if (movementSpeedTimer > increaseMovementSpeedTime)
        {
            currentMovementSpeed = Mathf.Clamp(currentMovementSpeed + increaseMovementSpeedValue, startMovementSpeed, maxMovementSpeedValue);
            movementSpeedTimer = 0;
        }

        if (currentSpawnRateTimer > spawnRateReduceTime)
        {
            currentSpawnRate = Mathf.Clamp(currentSpawnRate - spawnRateReduceValue, minSpawnRateValue, startSpawnRate);
            currentSpawnRateTimer = 0;
        }
    }

    public static float GetMovementSpeed()
    {
        return i.currentMovementSpeed;
    }

    public static float GetCurrentSpawnRate()
    {
        return i.currentSpawnRate;
    }
}
