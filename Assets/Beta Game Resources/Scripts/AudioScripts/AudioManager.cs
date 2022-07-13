using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager {
	public static void SetMasterVolume(float _volume /* Volume goes from 0 to 100 */) {
		FMODUnity.RuntimeManager.GetBus("Bus:/").setVolume(_volume / 100.0f);
	}

	public static void SetEffectsVolume(float _volume /* Volume goes from 0 to 100 */) {
		FMODUnity.RuntimeManager.GetBus("Bus:/Effects").setVolume(_volume / 100.0f);
	}

	public static void SetNarrationVolume(float _volume /* Volume goes from 0 to 100 */) {
		FMODUnity.RuntimeManager.GetBus("Bus:/Narration").setVolume(_volume / 100.0f);
	}

	public static void SetMusicVolume(float _volume /* Volume goes from 0 to 100 */) {
		FMODUnity.RuntimeManager.GetBus("Bus:/Music").setVolume(_volume / 100.0f);
	}
}
