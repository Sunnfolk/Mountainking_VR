using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Eating : MonoBehaviour {
	[SerializeField]
	private Hand _leftHand;

	[SerializeField]
	private Hand _rightHand;

	private PlayerBehaviour _instance;

	private void Start() {
		_instance = PlayerBehaviour._instance;
	}


	private void OnTriggerStay(Collider other) {
		if (!other.GetComponent<Edible.Edible>()) return;

        if (other.CompareTag("Crown"))
        {
            Debug.Log("End game");
            other.gameObject.SetActive(false);
        }

        else if (other.CompareTag("Interractable") && other.GetComponent<ItemController>().interactedWith && (int)_instance.stage >= other.GetComponent<Edible.Edible>()._edibleIndex)
        {
			PlayerBehaviour._instance.Eat(other.GetComponent<Edible.Edible>());
			Debug.Log("Munch time is here");
		}
        
        

	}
}
