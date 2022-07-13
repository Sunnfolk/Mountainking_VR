using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language {
	ENGLISH = 0,
	NORWEGIAN = 1,
}

namespace NarratorEvents {
}

public class Narrator : MonoBehaviour {
	[Header("Main Game")]
	[SerializeField]
	[FMODUnity.EventRef]
	public string Cave_Start;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Cave_Delayed;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Cabin_Spotted;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Church_Spotted;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Church_Destroyed;
    [SerializeField]
    [FMODUnity.EventRef]
    public string Ending;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Farm_Delayed;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Forest_Enter;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Forest_Delayed;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Forest_Exit;
	[SerializeField]
	[FMODUnity.EventRef]
	public string City_Enter;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Stage_3;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Stage_4;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Stage_5;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Stage_5_Delayed;

	[Header("Mini Games")]
	[FMODUnity.EventRef]
	public string Baseball_Hit;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Bowling_Hit;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Bowling_Miss;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Bowling_Strike;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Darts_Miss;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Darts_Hit;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Frenzy;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Tea_Finish;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Tea_Served;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Whack_End;
	[SerializeField]
	[FMODUnity.EventRef]
	public string Whack_Hit;

	public static Narrator instance;

	[SerializeField]
	private float timer;

	[SerializeField]
	private bool narrating;

	[SerializeField]
	private Language language;

	[SerializeField]
	private FMOD.Studio.EventInstance active;

	public bool Narrating() {
		return narrating;
	}

	private void Awake() {
		if (instance != this) {
			Destroy(instance);
			instance = this;
		}
	}

	void Start() {

	}

	private FMOD.VECTOR UnityToFMODVector(Vector3 _v) {
		FMOD.VECTOR result;
		result.x = _v.x;
		result.y = _v.y;
		result.z = _v.z;

		return result;
	}

	void Update() {
		timer -= Time.deltaTime;
		if (timer < 0.0f) {
			timer = -0.5f;
			narrating = false;
		}

		FMOD.ATTRIBUTES_3D attr;
		attr.position = UnityToFMODVector(transform.position);
		attr.forward = UnityToFMODVector(transform.forward);
		attr.up = UnityToFMODVector(transform.up);
		attr.velocity = UnityToFMODVector(Vector3.zero);

		active.set3DAttributes(attr);
	}

	public void SetLanguage(Language _lang) {
		language = _lang;
	}

	public void PlayEvent(string _e) {
		if (narrating) return;
		narrating = true;

		active = FMODUnity.RuntimeManager.CreateInstance(_e);

		FMOD.ATTRIBUTES_3D attr;
		attr.position = UnityToFMODVector(transform.position);
		attr.forward = UnityToFMODVector(transform.forward);
		attr.up = UnityToFMODVector(transform.up);
		attr.velocity = UnityToFMODVector(Vector3.zero);
		active.set3DAttributes(attr);

		SetParam(ref active);

		active.start();
		int l;
		FMODUnity.RuntimeManager.GetEventDescription(_e).getLength(out l);
		timer = l / 1000;
	}

	private void SetParam(ref FMOD.Studio.EventInstance inst) {
		active.setParameterValue("Language", (float)language);
	}
}
