
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edible {
	[RequireComponent(typeof(Valve.VR.InteractionSystem.Interactable))]
	[RequireComponent(typeof(Valve.VR.InteractionSystem.VelocityEstimator))]
	[RequireComponent(typeof(Valve.VR.InteractionSystem.Throwable))]
	[RequireComponent(typeof(ItemController))]
	public class Edible : MonoBehaviour {
		/* MATERIAL SLIDERS BEGIN */
		[SerializeField]
		[Range(0, 1)]
		private float tree;

		[SerializeField]
		[Range(0, 1)]
		private float plant;

		[SerializeField]
		[Range(0, 1)]
		private float rock;

		[SerializeField]
		[Range(0, 1)]
		private float glass;

		[SerializeField]
		[Range(0, 1)]
		private float metal;
		/* MATERIAL SLIDERS END */

		[SerializeField]
		private UnityEvent onEat;

		private void Awake() {
			oVal = value;
			gameObject.tag = "Interractable";
		}

		[Tooltip("1 = edible stage 1 and above. 2 = Edible stage 2 and above. 3 = Edible stage 3 and above. 4 = Edible stage 4 and above. 5 = Edible stage 5")]
		public int _edibleIndex;

		[Tooltip("The value that will be added if you eat this")]
		public float value;

		private float oVal;

		public float OriginalValue() {
			return oVal;
		}

		public float Tree() {
			return tree;
		}

		public float Plant() {
			return plant;
		}

		public float Rock() {
			return rock;
		}

		public float Glass() {
			return glass;
		}

		public float Metal() {
			return metal;
		}

		public void Eat() {
            if (gameObject.GetComponentInParent<Valve.VR.InteractionSystem.Hand>())
                   gameObject.GetComponentInParent<Valve.VR.InteractionSystem.Hand>().DetachObject(gameObject, false);

            onEat.Invoke();
			Destroy(gameObject);
		}
	}


}
