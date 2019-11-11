using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private int startScore;

    private int currentScore;

    public void Init()
    {
        currentScore = startScore;
    }

    public void AddScore()
    {
        currentScore+=100;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
