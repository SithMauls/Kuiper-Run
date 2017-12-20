using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerBase : MonoBehaviour
{
	[Header("Power")]
	public string powerName;
	[SerializeField] protected float duration = 5.0f;

	[Header("User Interface")]
	protected Slider powerSlider;


	#region Properties
	public float Duration
	{
		get {
			return duration;
		}

		set {
			duration = value;
			powerSlider.value = duration;

			if (duration <= 0) {
				GameController.Instance.ShipSpeed = 5.0f;
				Destroy(gameObject);
			}
		}
	}
	#endregion


	protected virtual void Awake()
	{
		powerSlider = GameController.Instance.powerSlider;
	}


	protected virtual void OnEnable() {

		//User Interface
		powerSlider.maxValue = duration;
		powerSlider.value = duration;
	}
}
