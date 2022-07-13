using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPartyScore : MonoBehaviour
{

    [SerializeField]
    private float Timer;

    private bool RunTimer;

    private int Score;

    private bool win;

    [SerializeField]
    
    private GameObject[] FullCups;

    [SerializeField]
    private GameObject[] prize;

    private int nr;
    private bool hasSpawnedPrize;

    [SerializeField]
    private Transform spawn;


    // Start is called before the first frame update
    void Start()
    {
        RunTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (RunTimer)
            Timer -= 1 * Time.deltaTime;
        

        if (Timer <= 0)
        {
            RunTimer = false;
            FullCups = GameObject.FindGameObjectsWithTag("TeaCupFull");
            
            for (int i = 0; i < FullCups.Length; i ++)
            {
                if (i == 4)
                { nr = 0; }
                else if (i >= 2)
                { nr = 1; }
                else if (i >= 0)
                { nr = 2; }
            }
            win = true;
        }

        if (win)
        {
            if (!hasSpawnedPrize)
            {
                Instantiate(prize[nr], spawn.position, Quaternion.identity);
                hasSpawnedPrize = true;
            }
        }
    }
}
