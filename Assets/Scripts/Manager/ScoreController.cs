using System;
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
        currentScore += 100;
    }

    public bool CheckHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            return true;
        }

        return false;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
