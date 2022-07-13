using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBowlingManager : MonoBehaviour
{
    public bool CowRespawn = false;
    public BowlingBall _ball;
    public List<BowlingPin> _bowlingPins;

    public int Score;
    public int Round;

    public GameObject[] prize;
    public int nr;

    [SerializeField]
    private Transform _spawn;

    private bool hasSpawnedPrize;


    // Start is called before the first frame update
    void Start()
    {
        _bowlingPins.AddRange(GetComponentsInChildren<BowlingPin>());
    }


    // Update is called once per frame
    void Update()
    {
        if (_ball.balls >= 2)
        {
            CowRespawn = true;
            _ball.respawn = true;
            Round++;
        }
        if (CowRespawn)
        {
            _ball.balls = -1;

            foreach(BowlingPin pin in _bowlingPins)
            {
                if (pin.hasFallen)
                {
                    Score++;
                }
                pin.CowReset();
            }
            CowRespawn = false;
        }

        if (Round >= 3)
        {
            _ball.gameObject.SetActive(false);
            EndGame();

            if(!hasSpawnedPrize)
            {
                Instantiate(prize[nr], _spawn.position, Quaternion.identity);
                hasSpawnedPrize = true;
            }

        }

    }

    public void EndGame()
    {
        if (Score <= 30)
        { nr = 0; }
        else if (Score <= 15)
        { nr = 1; }
        else if (Score <= 4)
        { nr = 2; }
    }
}