using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="Score Manager", menuName ="Create Score Manager")]
public class Score : ScriptableObject
{
    public float score;
    public float _prevScore;
    public string previousScene;

    public int PlayerState;

    public Vector3 afterFrenzy;
    public Vector3 afterTea;
    public Vector3 afterBowling;

    public void AddScore(float amount)
    {
        score += amount;
    }

    public void SetScore(int amount)
    {
        score = amount;
    }

    public void SaveScore()
    {
        _prevScore = score;
    }

    public void LoadScore()
    {
        score = _prevScore;
    }

    public void AddMiniGameScore(float amount)
    {
        _prevScore += amount;
    }

    public void SetSceneName()
    {
        previousScene = SceneManager.GetActiveScene().name;
    }
}
