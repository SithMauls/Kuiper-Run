using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShieldBase : MonoBehaviour
{
	[Header("Shield")]
	public string shieldName;
	[SerializeField] protected float duration = 5.0f;

	[Header("User Interface")]
	protected Slider shieldSlider;


	#region Properties
	public float Duration
	{
		get {
			return duration;
		}

		set {
			duration = value;
			shieldSlider.value = duration;

			if (duration <= 0) {
				Destroy(gameObject);
			}
		}
	}
	#endregion


	protected virtual void Awake()
	{
		shieldSlider = GameController.Instance.shieldSlider;
	}


	protected virtual void OnEnable() {
		//User Interface
		shieldSlider.maxValue = duration;
		shieldSlider.value = duration;
	}
}
