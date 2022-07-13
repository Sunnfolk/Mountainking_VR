using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHumanHouse : MonoBehaviour {
	[Header("Used for Testing")]
	public bool PlayerBig;
	public bool NightTime; //Private Scrubs, used for testing

	[SerializeField]
	private Transform[] points;

	[SerializeField]
	[Range(8, 12)]
	private float RunSpeed;
	[SerializeField]
	[Header("Walking Speed")] //Walking Speed min and max
	private float WalkMin = 3f; //3
	[SerializeField]
	private float WalkMax = 5f; //5

	private float WalkSpeed;

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


	private int destPoint = 0;
	private NavMeshAgent agent;

	private bool Afraid;


	[SerializeField]
	[Header("Hide Timer")]
	private float WaitTime;


	private GameObject[] gos;  //used for finding closest
	private GameObject closest = null;

	private Transform target;
	private bool Attacking;


	private bool Sleeping;

	private enum States {

		Walk,
		Escape,
		Hiding,
		Attack,
		Sleep,

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
		CurrentState = States.Walk;
		GotoNextPoint();
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

			case States.Walk: {
					//Choose the next destination point when the agent gets close to current one
					if (!agent.pathPending && agent.remainingDistance < 0.5f && !Afraid)
						GotoNextPoint();

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
			case States.Sleep: {
					if (!Sleeping) {
						FindClosestSafeZone();
						agent.SetDestination(closest.transform.position);

					} else {
						if (!NightTime) {
							Sleeping = false;
							agent.speed = WalkSpeed;
							CurrentState = States.Walk;

						} else {
							agent.speed = 0;
							// Lay in bed MOTHERFUCKER
						}
					}





					//FIND BED
				}
				break;
		}

		if (NightTime)
			CurrentState = States.Sleep;

	}

	private void PlayOneShot(string sound) {
		FMODUnity.RuntimeManager.PlayOneShotAttached(sound, gameObject);
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {

			if (PlayerBig) //replace with scrub with int that determine size
				CurrentState = States.Escape;
			else
				CurrentState = States.Attack;


		}
		if (other.tag == "UnoccupiedBed" && Afraid) {
			//Starts hiding when it has found safe space 
			CurrentState = States.Hiding;
		}
		if (other.tag == "UnoccupiedBed" && NightTime) {
			Sleeping = true;
		}
		if (other.tag == "OccupiedBed") {
			FindClosestSafeZone();
			agent.SetDestination(closest.transform.position);
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


		gos = GameObject.FindGameObjectsWithTag("UnoccupiedBed");

		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;

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

