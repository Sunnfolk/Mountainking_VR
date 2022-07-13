using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFarmAnimals : MonoBehaviour {
	[Header("Used for Testing")]
	public bool NightTime; //scrub/remove
	public bool BrainWorking; //make private
	private bool Afraid; //make private


	[SerializeField] //RunSpeed
	[Range(8, 12)]
	private float RunSpeed;

	[SerializeField]
	[Header("Walking Speed")] //Walking Speed min and max
	private float WalkMin = 3f; //3
	[SerializeField]
	private float WalkMax = 5f; //5
	[SerializeField]
	[Header("When the Animal Runs")]
	private float EnemyDistanceRun = 7f;
	[SerializeField]
	[Header("How Long it's Idle")]
	private float IdleTime = 5;

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
	[FMODUnity.EventRef]
	private string hitSound;

	[SerializeField]
	[Range(0, 100)]
	private float soundProbability;

	private float soundTimer = 0.0f;

	[SerializeField]
	private float soundInterval;
	/* END OF SOUND STUFF */

	[Header("How long he runs away")] //How long it escapes
	[SerializeField]
	private float EscMin = 3f;
	[SerializeField]
	private float EscMax = 7f;

	[Header("Wander Times")]
	[SerializeField]
	private int RotTimeMin;
	[SerializeField]
	private int RotTimeMax, RotateWaitMin, RotateWaitMax, WalkWaitMin, WalkWaitMax, WalkTimeMin, WalkTimeMax;
	// RotTime is how long it rotates, RotWait is how long it's idle before rotating, WalkWait is how long it waits before walking, WalkTime is how long it walks its speed.
	//RotateLoR is if it's going to go left or right, it's 50/50 atm.

	private float RotSpeed = 100f; //100
	private bool isWandering = false;
	private bool isRotatingLeft = false;
	private bool isRotatingRight = false;
	private bool isWalking = false;

	private GameObject[] gos;
	private GameObject closest = null;

	private NavMeshAgent agent;
	[SerializeField]
	private GameObject Player;

    // Animation
    private Animator _anim;
    bool walkAnim = false;

    private enum States {
		Idle,
		Walk,
		Rot,
		Escape,
		Sleep,
	}
	[SerializeField]
	private States CurrentState = States.Idle;
	// Start is called before the first frame update
	void Start() {
		WalkSpeed = Random.Range(WalkMin, WalkMax);
		agent = GetComponent<NavMeshAgent>();
		Player = GameObject.FindGameObjectWithTag("Player");
		CurrentState = States.Idle;
		BrainWorking = true;

        _anim = GetComponent<Animator>();
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
					StartCoroutine(Idle());
					Brain();


				}
				break;
			case States.Walk: {

                    
				}
				break;
			case States.Rot: {

                    
                }
				break;

			case States.Escape: {
                    //Brain();
                }
				break;
			case States.Sleep: {
					if (!NightTime) {
						closest = null;
						BrainWorking = true;
						Brain();
                    }
				}
				break;
		}


		Walk();

		#region DANGER, NIGHT
		float Distance = Vector3.Distance(transform.position, Player.transform.position);
		if (Distance < EnemyDistanceRun) {
			if (!Afraid)
				StartCoroutine(Escapez());
			else if (Afraid)
				CurrentState = States.Escape;

		}

		if (NightTime) {
			BrainWorking = false;
			FindSleep();
			agent.SetDestination(closest.transform.position);

		}


		#endregion

	}

	private void PlayOneShot(string sound) {
		FMODUnity.RuntimeManager.PlayOneShotAttached(sound, gameObject);
	}

	void Walk() {
		if (!isWandering)
			StartCoroutine(Wander());
		if (isRotatingRight)
			transform.Rotate(transform.up * Time.deltaTime * RotSpeed);
		if (isRotatingLeft)
			transform.Rotate(transform.up * Time.deltaTime * -RotSpeed);
        if (isWalking)
            transform.position += transform.forward * WalkSpeed * Time.deltaTime;
        if (walkAnim == true)
            _anim.SetBool("Walk", true);
        else
            _anim.SetBool("Walk", false);

    }

	void Brain() {
		if (BrainWorking && !Afraid) {
			BrainWorking = false;
			agent.ResetPath();
			int WhatToDo = Random.Range(1, 100);
			if (WhatToDo <= 10)
				CurrentState = States.Idle;


		}
	}


	private GameObject FindSleep() {

		gos = GameObject.FindGameObjectsWithTag("AnimalShelter");

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
	private void OnTriggerEnter(Collider other) {
		if (other.tag == "AnimalShelter" && NightTime) {
			agent.ResetPath();
			CurrentState = States.Sleep;
		}

	}

	IEnumerator Idle() {


		yield return new WaitForSeconds(IdleTime);
		BrainWorking = true;
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
        BrainWorking = true;
		isWandering = false;
		Brain();
	}



	IEnumerator Escapez() {
		Afraid = true;
        walkAnim = true;
        _anim.speed = 2;
        agent.speed = RunSpeed;
		Vector3 dirToPlayer = transform.position - Player.transform.position;

		Vector3 newPos = transform.position + dirToPlayer;

		agent.SetDestination(newPos);
		float EscapeTime = Random.Range(EscMin, EscMax);

		yield return new WaitForSeconds(EscapeTime);
		Afraid = false;
		agent.speed = WalkSpeed;
		agent.ResetPath();
		CurrentState = States.Walk;
        walkAnim = false;
        _anim.speed = 1;

    }




}
