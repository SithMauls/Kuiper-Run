using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private bool collectPickUps = true;
	[Header("Equipment")]
	[SerializeField] private GameObject currentWeapon = null;
	[SerializeField] private GameObject currentPower = null;

	private Transform thisTransform = null;
	[HideInInspector] public WeaponBase weaponScript = null;
	[HideInInspector] public PowerBase powerScript = null;
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

	public GameObject CurrentPower
	{
		set
		{
			if (currentPower != null)
			{
				Destroy(currentPower);
			}

			currentPower = Instantiate(value, thisTransform);
			powerScript = currentPower.GetComponent<PowerBase>();

			if (gameObject.CompareTag("Player"))
			{
				textScript.Scroll(powerScript.powerName);
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
		if (collectPickUps)
		{
			if (collision.CompareTag("PowerUp"))
			{
				PickUp pickUpScript = collision.GetComponent<PickUp>();

				switch (pickUpScript.itemType)
				{
					case PickUp.Item.Weapon:
						CurrentWeapon = pickUpScript.currentItem;
						break;

					case PickUp.Item.Power:
						CurrentPower = pickUpScript.currentItem;
						break;

					default:
						break;
				}

				pickUpScript.Remove();
			}
		}
	}
}
