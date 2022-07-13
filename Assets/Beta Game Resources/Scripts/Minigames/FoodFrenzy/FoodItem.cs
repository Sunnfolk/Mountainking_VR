using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodItem : MonoBehaviour
{
    public int scoreValue = 1;

    private FoodMinigameManager foodMinigameManager;

    public bool it;

    // Start is called before the first frame update
    void Start()
    {
        //foodMinigameManager = FindObjectOfType<FoodMinigameManager>();
        foodMinigameManager = GetComponent<FoodMinigameManager>();
    }

    public void Update()
    {
        if (it)
        {
            SetScoreUp();
            it = false;
        }
    }

    public void SetScoreUp()
    {
            foodMinigameManager.Score += scoreValue;
    }
}
