using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteringRam : ShieldBase
{
	protected override void OnEnable()
	{
		base.OnEnable();

		Destroy(gameObject, duration);
	}

	private void Update()
	{
		Duration -= Time.deltaTime;
	}
}
