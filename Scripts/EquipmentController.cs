using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private bool collectPowerUps = true;
	private Transform thisTransform = null;

	[Header("Equipment")]
	[SerializeField] private GameObject currentWeapon = null;
	[HideInInspector] public WeaponBase weaponScript = null;
	[SerializeField] private GameObject currentShield = null;
	[HideInInspector] public ShieldBase shieldScript = null;

	[Header("User Interface")]
	private ScrollingText textScript = null;


	#region Properties
	public GameObject CurrentWeapon
	{
		set
		{
			if (currentWeapon != null)
			{
				Destroy(currentWeapon);
			}

			currentWeapon = Instantiate(value, thisTransform);
			weaponScript = currentWeapon.GetComponent<WeaponBase>();

			if (gameObject.CompareTag("Player"))
			{
				textScript.Scroll(weaponScript.weaponName);
			}
		}
	}

	public GameObject CurrentShield
	{
		set
		{
			if (currentShield != null)
			{
				Destroy(currentShield);
			}

			currentShield = Instantiate(value, thisTransform);
			shieldScript = currentShield.GetComponent<ShieldBase>();

			if (gameObject.CompareTag("Player"))
			{
				textScript.Scroll(shieldScript.shieldName);
			}
		}
	}
	#endregion


	private void Start()
	{
		thisTransform = GetComponent<Transform>();
		textScript = GameController.Instance.itemText.GetComponent<ScrollingText>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collectPowerUps)
		{
			if (collision.CompareTag("PowerUp"))
			{
				PowerUp powerUpScript = collision.GetComponent<PowerUp>();

				switch (powerUpScript.itemType)
				{
					case PowerUp.Item.Weapon:
						CurrentWeapon = powerUpScript.currentItem;
						break;

					case PowerUp.Item.Shield:
						CurrentShield = powerUpScript.currentItem;
						break;

					default:
						break;
				}

				powerUpScript.Remove();
			}
		}
	}
}
