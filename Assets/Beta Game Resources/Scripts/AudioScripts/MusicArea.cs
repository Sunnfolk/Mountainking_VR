using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area {
    NONE,
	FOREST,
	FARM,
	CITY,
	CAVE
}

public class MusicArea : MonoBehaviour
{
	[SerializeField]
	private Area type;

	public Area Type() {
		return type;
	}
}
