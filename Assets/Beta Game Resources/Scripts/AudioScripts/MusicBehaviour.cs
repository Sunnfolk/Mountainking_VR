using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehaviour : MonoBehaviour {
	[Header("Test Variables")]
	[SerializeField]
	[Range(1, 5)]
	private float size;

	[SerializeField]
	private bool narrating;

	[Header("Area")]
	[SerializeField]
	private Area area;

	[SerializeField]
	private Area a;

	[Header("Events")]
	[SerializeField]
	[FMODUnity.EventRef]
	private string caveMusicEvent;

	[SerializeField]
	[FMODUnity.EventRef]
	private string forestMusicEvent;

	[SerializeField]
	[FMODUnity.EventRef]
	private string farmMusicEvent;

	[SerializeField]
	[FMODUnity.EventRef]
	private string cityMusicEvent;

	

	private FMOD.Studio.EventInstance rActive;

	private FMOD.Studio.EventInstance caveMusicInstance;
	private FMOD.Studio.EventInstance forestMusicInstance;
	private FMOD.Studio.EventInstance farmMusicInstance;
	private FMOD.Studio.EventInstance cityMusicInstance;

	[SerializeField]
	private Narrator narrator;

	public void SetArea(Area val) {
		a = val;
	}

	private void Awake() {
		if (!narrator) narrator = gameObject.GetComponent<Narrator>();

		caveMusicInstance = FMODUnity.RuntimeManager.CreateInstance(caveMusicEvent);
		forestMusicInstance = FMODUnity.RuntimeManager.CreateInstance(forestMusicEvent);
		farmMusicInstance = FMODUnity.RuntimeManager.CreateInstance(farmMusicEvent);
		cityMusicInstance = FMODUnity.RuntimeManager.CreateInstance(cityMusicEvent);

		FMOD.ATTRIBUTES_3D attr = new FMOD.ATTRIBUTES_3D();
		attr.position.x = transform.position.x;
		attr.position.y = transform.position.y;
		attr.position.z = transform.position.z;

		attr.velocity.x = 0.0f;
		attr.velocity.y = 0.0f;
		attr.velocity.z = 0.0f;

		attr.forward.x = transform.forward.x;
		attr.forward.y = transform.forward.y;
		attr.forward.z = transform.forward.z;

		attr.up.x = transform.up.x;
		attr.up.y = transform.up.y;
		attr.up.z = transform.up.z;

		caveMusicInstance.set3DAttributes(attr);
		forestMusicInstance.set3DAttributes(attr);
		farmMusicInstance.set3DAttributes(attr);
		cityMusicInstance.set3DAttributes(attr);
	}

	// Update is called once per frame
	void Update() {
		FMOD.ATTRIBUTES_3D attr = new FMOD.ATTRIBUTES_3D();
		attr.position.x = transform.position.x;
		attr.position.y = transform.position.y;
		attr.position.z = transform.position.z;

		attr.velocity.x = 0.0f;
		attr.velocity.y = 0.0f;
		attr.velocity.z = 0.0f;

		attr.forward.x = transform.forward.x;
		attr.forward.y = transform.forward.y;
		attr.forward.z = transform.forward.z;

		attr.up.x = transform.up.x;
		attr.up.y = transform.up.y;
		attr.up.z = transform.up.z;

		rActive.set3DAttributes(attr);

		if (PlayerBehaviour._instance) rActive.setParameterValue("Size", (float)PlayerBehaviour._instance.stage);
		else rActive.setParameterValue("Size", size);

		if (narrator) rActive.setParameterValue("Narration", (narrator.Narrating()) ? 0.0f : 1.0f);
		else rActive.setParameterValue("Narration", narrating ? 0.0f : 1.0f);

		if (area != a) {
			area = a;
			rActive.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

			switch (area) {
				case Area.CAVE:
					rActive = caveMusicInstance;
					break;
				case Area.FOREST:
					rActive = forestMusicInstance;
					break;
				case Area.FARM:
					rActive = farmMusicInstance;
					break;
				case Area.CITY:
					rActive = cityMusicInstance;
					break;
			}

			rActive.start();
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.GetComponent<MusicArea>()) {
			//Debug.Log("In music area");
			SetArea(other.GetComponent<MusicArea>().Type());
		}
	}
}
