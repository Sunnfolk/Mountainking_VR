using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[RequireComponent(typeof(Narrator))]
//[RequireComponent(typeof(MusicBehaviour))]
public class PlayerSounds : MonoBehaviour {

	[SerializeField]
	[Range(0, 100)]
	private float masterVolume; //RMBB

	[SerializeField]
	[Range(0, 100)]
	private float effectsVolume; // RMBB

	[SerializeField]
	[Range(0, 100)]
	private float musicVolume; // RMBB

	[SerializeField]
	[Range(0, 100)]
	private float narrationVolume; // RMBB

	[SerializeField]
	private bool updateVolume; // RMBB

	[SerializeField]
	[FMODUnity.EventRef]
	string eatEventPath;
	private FMOD.Studio.EventInstance eatSoundInstance;

	[SerializeField]
	private float eatingDuration;
	private float eatingTime;
	private bool playing;

	[SerializeField]
	private Narrator narrator;
	[SerializeField]
	private MusicBehaviour musicBehaviour;

	[SerializeField]
	private bool test;

	public void Awake() {
		if (!narrator) narrator = gameObject.GetComponent<Narrator>();
		if (!musicBehaviour) musicBehaviour = gameObject.GetComponent<MusicBehaviour>();

		playing = false;
		eatingTime = 0.0f;

		FMOD.ATTRIBUTES_3D attr = new FMOD.ATTRIBUTES_3D();
		attr.position.x = 0.0f;
		attr.position.y = 0.0f;
		attr.position.z = 0.0f;

		attr.velocity.x = 0.0f;
		attr.velocity.y = 0.0f;
		attr.velocity.z = 0.0f;

		attr.forward.x = 0.0f;
		attr.forward.y = 0.0f;
		attr.forward.z = 1.0f;

		attr.up.x = 0.0f;
		attr.up.y = 1.0f;
		attr.up.z = 0.0f;
	}

	public void PlayEatSound() {
		playing = true;
		eatingTime = 0.0f;
		eatSoundInstance.start();
		return;
	}

	private void StopEatSound() {
		playing = false;
		eatSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		return;
	}

	public ref Narrator GetNarrator() {
		return ref narrator;
	}

	public ref MusicBehaviour GetMusicPlayer() {
		return ref musicBehaviour;
	}

	public bool isPlaying = true;
	public bool last = false;

	public bool instantiated = false;

	private bool InstantiateEvents() {
		if (!FMODUnity.RuntimeManager.HasBankLoaded("Master Bank") || instantiated) return false;

		instantiated = true;

		eatSoundInstance = FMODUnity.RuntimeManager.CreateInstance(eatEventPath);
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(eatSoundInstance, transform, GetComponent<Rigidbody>());

		return true;
	}

	private void Start() {
	}

	public void SetParam(float t, float g, float p, float m, float r, float s) {
		eatSoundInstance.setParameterValue("Size", s);
		eatSoundInstance.setParameterValue("Tree", t);
		eatSoundInstance.setParameterValue("Glass", g);
		eatSoundInstance.setParameterValue("Plant", p);
		eatSoundInstance.setParameterValue("Metal", m);
		eatSoundInstance.setParameterValue("Rock", r);
	}

	private void Update() {
		if (updateVolume) {
			AudioManager.SetMasterVolume(masterVolume);             // RMBB
			AudioManager.SetEffectsVolume(effectsVolume);           // RMBB
			AudioManager.SetMusicVolume(musicVolume);               // RMBB
			AudioManager.SetNarrationVolume(narrationVolume);       // RMBB
			updateVolume = false;
		}

		if (test) Narrator.instance.PlayEvent(Narrator.instance.Cave_Start); // RMBB

		if (InstantiateEvents()) return;

		eatingTime += Time.deltaTime;
		if (eatingTime > eatingDuration) {
			eatingTime = eatingDuration + 0.5f;
			if (playing) {
				StopEatSound();
			}
		}

		FMOD.ATTRIBUTES_3D attr = new FMOD.ATTRIBUTES_3D();
		attr.position.x = transform.position.x;
		attr.position.y = transform.position.y;
		attr.position.z = transform.position.z;

		attr.forward.x = transform.forward.x;
		attr.forward.y = transform.forward.y;
		attr.forward.z = transform.forward.z;

		attr.up.x = transform.up.x;
		attr.up.y = transform.up.y;
		attr.up.z = transform.up.z;

		eatSoundInstance.set3DAttributes(attr);
	}
}

