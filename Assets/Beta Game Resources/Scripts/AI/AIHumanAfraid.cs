using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHumanAfraid : MonoBehaviour
{

    private AIHumanWalk Human;
    

    // Start is called before the first frame update
    void Start()
    {
        Human = GetComponentInParent<AIHumanWalk>();
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (Human.PlayerBig) //replace with scrub with int that determine size
                Human.StateEscape = true;

            else
                Human.StateAttack = true;


        }
        

    }
}
