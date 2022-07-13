using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWildAnimals : MonoBehaviour {
	//public float MoveSpeed = 3f; //3   Make into walkspeed


	//Used for distance before it runs
	[SerializeField]
	private float EnemyDistanceRun = 4f;  //SER
    #region Sound Stuff
    /* SOUND STUFF */
    [Header("Sound stuff")]
	[SerializeField]
	[FMODUnity.EventRef]
	private string idleSound;
	[SerializeField]
	[FMODUnity.EventRef]
	private string afraidSound;

	[SerializeField]
	[Range(0, 100)]
	private float soundProbability;

	private float soundTimer = 0.0f;

	[SerializeField]
	private float soundInterval;
    /* END OF SOUND STUFF */
    #endregion

    // This is for rotation
    private float RotSpeed = 100f; //100
	private bool isWandering = false;
	private bool isRotatingLeft = false;
	private bool isRotatingRight = false;
	private bool isWalking = false;

    //If it doesn't want to find it with tag
    #region Dangers
    [SerializeField]
	private GameObject Player; //SER

    [SerializeField]
    [HideInInspector]
    private GameObject[] WildAnimals;

    [SerializeField]
    [HideInInspector]
    private List<GameObject> _wildAnimals;

    private GameObject ClosestAnimal;
    private float Distance;

    [SerializeField]
    private float AnimalDistanceRun; // distance when animals run
    #endregion


    private bool BrainWorking;

	[SerializeField] //Walking Points
	private Transform[] points;

	private int destPoint = 0;
	private NavMeshAgent agent;

	public bool Afraid;

	[SerializeField] //RunSpeed
	[Range(8, 12)]
	private float RunSpeed;

	[SerializeField]
	[Header("Walking Speed")] //Walking Speed min and max
	private float WalkMin = 3f; //3
	[SerializeField]
	private float WalkMax = 5f; //5

	private float WalkSpeed; //random value between min and max

	[SerializeField]
	[Header("Wander Time")]
	private float WanMin = 7f; //Wander Time
	[SerializeField]
	private float WanMax = 10f;

    [Header("Wander Times")]
    [SerializeField]
    private int RotTimeMin;
    [SerializeField]
    private int RotTimeMax, RotateWaitMin, RotateWaitMax, WalkWaitMin, WalkWaitMax, WalkTimeMin, WalkTimeMax;
    // RotTime is how long it rotates, RotWait is how long it's idle before rotating, WalkWait is how long it waits before walking, WalkTime is how long it walks its speed.
    //RotateLoR is if it's going to go left or right, it's 50/50 atm.

    private bool Timer;
	private bool WanderingDone;
	

    
    [SerializeField]
    private float IdleTime;

    private bool IdleCheck;
    private bool Idle;

    //Animation
    [SerializeField]
    private Animator _anim;
    public bool walkAnim;

    private enum States {
		Idle,
		Walk,
		Escape,

	}

	[SerializeField]
	[Header("Animation")]
	private States CurrentState = States.Walk;

	// Start is called before the first frame update
	void Start() {
		agent = GetComponent<NavMeshAgent>();
		WalkSpeed = Random.Range(WalkMin, WalkMax);
		agent.speed = WalkSpeed;
		Player = GameObject.FindGameObjectWithTag("Player");
		CurrentState = States.Walk;
		BrainWorking = true;
		GotoNextPoint();
        walkAnim = true;

        WildAnimals = GameObject.FindGameObjectsWithTag("AI");

        foreach(GameObject go in WildAnimals)
        {
            _wildAnimals.Add(go);
        }

        _wildAnimals.RemoveAt(0);
    }

	// Update is called once per frame
	void Update() {

		/* Randomly plays a sound */
		soundTimer += Time.deltaTime;
		if (soundTimer >= soundInterval) {
			soundTimer = 0.0f;
			if (Random.Range(0.0f, 100.0f) > 100.0f - soundProbability) {
				if (Afraid) {
					PlayOneShot(afraidSound);
				} else {
					PlayOneShot(idleSound);
				}
			}
		}
		switch (CurrentState) {
			case States.Idle: {

				}
				break;
			case States.Walk: {

                    walkAnim = true;
				}
				break;

			case States.Escape: {
					//STATE
				}
				break;

		}

        FindAnimal();
        Danger();


		Brain();

        if (walkAnim == true)
            _anim.SetBool("Walk", true);
        else
            _anim.SetBool("Walk", false);
    }

    private void Danger()
    {
        Distance = Vector3.Distance(transform.position, Player.transform.position);
        if (Distance < EnemyDistanceRun)
        {
            if (!Afraid)
            {
                StartCoroutine(Escapez());
                CurrentState = States.Escape;
            }
                

        }
        else
        {
            Distance = Vector3.Distance(transform.position, ClosestAnimal.transform.position);
            if (Distance < AnimalDistanceRun)
            {
                if (!Afraid)
                {
                    Debug.Log("ANIMAL");
                    StartCoroutine(Escapez());
                    CurrentState = States.Escape;
                }
                    

            }


        }
    }

    private GameObject FindAnimal()
    {


        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in _wildAnimals)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                ClosestAnimal = go;
                distance = curDistance;
            }
        }
        return ClosestAnimal;
    }

    private void PlayOneShot(string sound) {
		FMODUnity.RuntimeManager.PlayOneShotAttached(sound, gameObject);
	}

	void Brain()
    {
		if (!agent.pathPending && agent.remainingDistance < 0.5f && !Afraid)
        {
			if (WanderingDone)
            {
				WanderingDone = false;
                walkAnim = false;
                GotoNextPoint();
			}
            else
            {
                if (!IdleCheck)
                {
                    IdleCheck = true;
                    float K = Random.Range(1, 3);
                    if (K == 1)
                        Idle = true;
                    else if (K >= 2)
                        Idle = false;

                }

                if (Idle && IdleCheck)
                {
                    
                    StartCoroutine(IdleStance());
                }
                else if (!Idle && IdleCheck)
                {
                    if (!Timer)
                        StartCoroutine(Timers());
                    if (!isWandering)
                        StartCoroutine(Wander());
                    if (isRotatingRight)
                        transform.Rotate(transform.up * Time.deltaTime * RotSpeed);
                    if (isRotatingLeft)
                        transform.Rotate(transform.up * Time.deltaTime * -RotSpeed);
                    if (isWalking)
                        transform.position += transform.forward * WalkSpeed * Time.deltaTime;

                }




            }

		}

	}

    IEnumerator IdleStance()
    {
        walkAnim = false;
        yield return new WaitForSeconds(IdleTime);
        WanderingDone = true;
        IdleCheck = false;


    }

	IEnumerator Wander() {
        int rotTime = Random.Range(RotTimeMin, RotTimeMax);
        int rotateWait = Random.Range(RotateWaitMin, RotateWaitMax);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(WalkWaitMin, WalkWaitMax);
        int walkTime = Random.Range(WalkTimeMin, WalkTimeMax);

        isWandering = true;


		yield return new WaitForSeconds(walkWait);
		isWalking = true;
        walkAnim = true;
        yield return new WaitForSeconds(walkTime);
		isWalking = false;
        walkAnim = false;
        yield return new WaitForSeconds(rotateWait);
		if (rotateLorR == 1) {
			isRotatingRight = true;
			yield return new WaitForSeconds(rotTime);
			isRotatingRight = false;
		}
		if (rotateLorR == 2) {
			isRotatingLeft = true;
			yield return new WaitForSeconds(rotTime);
			isRotatingLeft = false;
		}

		isWandering = false;
		WanderingDone = true;
        IdleCheck = false;

	}

	IEnumerator Timers() {
		Timer = true;
		float CD = Random.Range(WanMin, WanMax);
		yield return new WaitForSeconds(CD);
		WanderingDone = true;
		Timer = false;

	}

	IEnumerator Escapez() {
		Afraid = true;
        walkAnim = true;
        _anim.speed = 2;
        agent.speed = RunSpeed;

        //Idle = false;
        //IdleCheck = false;
        

		Vector3 dirToPlayer = transform.position - Player.transform.position;

		Vector3 newPos = transform.position + dirToPlayer;


        
        agent.SetDestination(newPos);

        if (transform.position == newPos)
            walkAnim = true;
        

		

		yield return new WaitForSeconds(1);
		Afraid = false;
        //WanderingDone = true;
        agent.speed = WalkSpeed;
		agent.destination = points[destPoint].position;
		
        _anim.speed = 1;

    }

	private void GotoNextPoint() {
		//Returns if no points have been set up
		if (points.Length == 0)
			return;

		//Set the agent to go to the currently selected destination.

		agent.destination = points[destPoint].position;

		//Choose the next poin in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}


}


