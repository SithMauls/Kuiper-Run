using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinBlaster : WeaponBase
{	
	public override void Fire()
	{
		if (Input.GetTouch(0).phase == TouchPhase.Began && canFire)
		{
			foreach (Transform item in weaponTransforms)
			{
				SpawnBullet(item);
			}
		}
	}
}
