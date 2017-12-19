using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeTurret : WeaponBase
{
	public override void Fire()
	{
		if (Input.GetTouch(0).phase == TouchPhase.Began && canFire)
		{
			foreach (Transform item in weaponTransforms)
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				item.up = touchPos - (Vector2)item.position;

				SpawnBullet(item);
			}
		}
	}
}
