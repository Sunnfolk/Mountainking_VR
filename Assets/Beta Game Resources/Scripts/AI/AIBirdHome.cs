using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBirdHome : MonoBehaviour
{
    [SerializeField]
    private AIBirdDirection Bird;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Bird.PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Bird.PlayerInRange = false;
        }
    }
}
