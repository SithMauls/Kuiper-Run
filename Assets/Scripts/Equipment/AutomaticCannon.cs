using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCannon : WeaponBase
{
	private int x = 0;


	public override void Fire()
	{
		if (canFire)
		{
			SpawnBullet(weaponTransforms[x]);
			x++;

			if (x >= weaponTransforms.Count)
			{
				x = 0;
			}
		}
	}
}
