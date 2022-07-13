using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBirdDirection : MonoBehaviour
{

    #region Orbit
    public GameObject Player;

    [SerializeField]
    private float OrbitSpeed;
    [SerializeField]
    private float OrbitDistance;
    #endregion

    #region Waypoints
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float Speed;
    
    private int waypointIndex = 0;

    
    private int _End;
    #endregion



    [HideInInspector]
    public bool PlayerInRange;
    [HideInInspector]
    public bool OrbitRange;

    

    // Start is called before the first frame update
    void Start()
    {
        _End = waypoints.Length - 1;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (!PlayerInRange && !OrbitRange) // Decided by the range mod, other script calls to it
            Move();

        if (PlayerInRange && !OrbitRange)
        {
            GoToPlayer();
            LookAtPlayer();
        }


        if (PlayerInRange && OrbitRange)
            OrbitAround();


        float Distance = Vector3.Distance(transform.position, Player.transform.position);
        if (Distance < OrbitDistance)
        {
            OrbitRange = true;

        }
        else
        {
            OrbitRange = false;
        }

        

    }

    void OrbitAround()
    {


        transform.RotateAround(Player.transform.position, Vector3.up, OrbitSpeed * Time.deltaTime);

    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.LookAt(waypoints[waypointIndex].transform.position);
            transform.position = Vector3.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                Speed * Time.deltaTime);

            if (transform.position == waypoints[_End].transform.position)
            {
                waypointIndex = 0;
            }
            else if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }

        }
    }

    void GoToPlayer()
    {

        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
    }

    void LookAtPlayer()
    {
        transform.LookAt(Player.transform.position);

    }

}
