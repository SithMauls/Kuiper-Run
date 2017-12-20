using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteringRam : PowerBase
{
	protected override void OnEnable()
	{
		base.OnEnable();
		GameController.Instance.ShipSpeed = 20f;

		Destroy(gameObject, duration);
	}

	private void Update()
	{
		Duration -= Time.deltaTime;
	}
}
