using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{


    private int PepBed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PepBed == 0)
            gameObject.tag = "UnoccupiedBed";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human")
            PepBed += 1;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Human")
            gameObject.tag = "OccupiedBed";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Human")
            PepBed -= 1;
    }

}
