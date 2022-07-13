using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField]
    private float Speed; //Speed of the cloud

    [SerializeField]  
    private Transform Spawner; //Spawner that spawns the cloud when it reaches destination

    [SerializeField]
    private bool SpawnAtSpawner; // When true the cloud start at spawner, if false it starts at it's point in the world.
    
    void Start()
    {
        if (SpawnAtSpawner) //if  true, start at Spawn Location
            transform.position = Spawner.position;
    }

    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
        //transfomr.forward can be changed to change direction
        // forward is Z+ Backward is Z- Right is X+ and Left is X-

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Despawner")  //Need a gameobject with tag Despawner, Rigidbody and BoxCollider with trigger.
            transform.position = Spawner.position;
    }
}
