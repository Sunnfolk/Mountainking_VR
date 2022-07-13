using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIHumanWalk : MonoBehaviour {
	[Header("Used for Testing")]
	public bool PlayerBig;


	[SerializeField]
	private Transform[] points;


	private int destPoint = 0;
	private NavMeshAgent agent;

	public bool Afraid;

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


	[SerializeField]
	[Range(8, 12)]
	private float RunSpeed;

	[SerializeField]
	[Header("Hide Timer")]
	private float WaitTime;
	[Header("Walking Speed")] //Walking Speed min and max
	[SerializeField]
	private float WalkMin = 3f; //3
	[SerializeField]
	private float WalkMax = 5f; //5

	private float WalkSpeed;

	private GameObject[] gos;
	private GameObject closest = null;
	private Transform target;
	private bool Attacking;

    //Animation
    private Animator _anim;
    bool walkAnim;

    #region Agroo Zone
    [HideInInspector]
    public bool StateEscape, StateAttack;
    
    #endregion



    private enum States {

		Walk,

		Escape,
		Hiding,
		Attack,


	}
	[SerializeField]
	private States CurrentState = States.Walk;

	// Start is called before the first frame update
	void Start() {
		destPoint = Random.Range(0, points.Length);
		agent = GetComponent<NavMeshAgent>();
		WalkSpeed = Random.Range(WalkMin, WalkMax);
		agent.speed = WalkSpeed;
		agent.autoBraking = false;
		target = GameObject.FindGameObjectWithTag("Player").transform;
        if (RunSpeed == 0)
            RunSpeed = 8;

		GotoNextPoint();

        walkAnim = true;
        _anim = GetComponentInParent<Animator>();
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

        if (StateEscape)
        {
            CurrentState = States.Escape;
            StateEscape = false;
        }
        if (StateAttack)
        {
            CurrentState = States.Attack;
            StateAttack = false;
        }
       
        


		switch (CurrentState) {

			case States.Walk: {
					//Choose the next destination point when the agent gets close to current one
					if (!agent.pathPending && agent.remainingDistance < 0.5f && !Afraid)
						GotoNextPoint();

                    walkAnim = true;
				}
				break;

			case States.Escape: {
					if (!Afraid) {
						Afraid = true;
						FindClosestSafeZone();
						agent.SetDestination(closest.transform.position);
						agent.speed = RunSpeed;
						agent.autoBraking = true;
					}
				}
				break;
			case States.Hiding: {
					StartCoroutine(Hiding());
				}
				break;
			case States.Attack: {
					if (!Attacking)
						StartCoroutine(Attack());
				}
				break;

		}

        //Plays walk animation
        if (walkAnim == true)
            _anim.SetBool("Walk", true);
        else
            _anim.SetBool("Walk", false);
    }

	private void PlayOneShot(string sound) {
		FMODUnity.RuntimeManager.PlayOneShotAttached(sound, gameObject);
	}

	private void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Safe" && Afraid) {
			//Starts hiding when it has found safe space 
			CurrentState = States.Hiding;
		}
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
	private GameObject FindClosestSafeZone() {

		gos = GameObject.FindGameObjectsWithTag("Safe");

		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
				Debug.Log(closest);
			}
		}
		return closest;
	}
	private void LookAtPlayer() {
		Vector3 targetDir = target.position - transform.position;

		float step = 4f * Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

		transform.rotation = Quaternion.LookRotation(newDir);
	}
	IEnumerator Hiding() {
		agent.speed = 0;

		yield return new WaitForSeconds(WaitTime);
		Afraid = false;
		CurrentState = States.Walk;
		GotoNextPoint();
		agent.speed = WalkSpeed;
		agent.autoBraking = false;
        

	}
	IEnumerator Attack() {
		Attacking = true;
		agent.speed = 0;
		LookAtPlayer();

		yield return new WaitForSeconds(4);
		agent.speed = WalkSpeed;
		CurrentState = States.Walk;
		Attacking = false;

	}

}

