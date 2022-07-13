using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBirdBody : MonoBehaviour {
	[SerializeField]
	private float Speed;
	[SerializeField]
	private GameObject BirdDirection;

	private Animator _anim;

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

	// Start is called before the first frame update
	void Start() {


		_anim = GetComponent<Animator>();
		_anim.SetBool("Walk", true);
	}

	// Update is called once per frame
	void Update() {
		/* Randomly plays a sound */
		soundTimer += Time.deltaTime;
		if (soundTimer >= soundInterval) {
			soundTimer = 0.0f;
			if (Random.Range(0.0f, 100.0f) > 100.0f - soundProbability) {
				if (true) {
					PlayOneShot(afraidSound);
				} else {
					PlayOneShot(idleSound);
				}
			}
		}

		transform.LookAt(BirdDirection.transform.position);
		transform.position = Vector3.MoveTowards(transform.position, BirdDirection.transform.position, Speed * Time.deltaTime);
	}


	private void PlayOneShot(string sound) {
		FMODUnity.RuntimeManager.PlayOneShotAttached(sound, gameObject);
	}
}
