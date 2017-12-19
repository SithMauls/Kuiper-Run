using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponBase : MonoBehaviour
{
	[Header("Weapon")]
	public string weaponName;
	protected List<Transform> weaponTransforms = new List<Transform>();

	[Header("Ammo")]
	[SerializeField] private GameObject bulletPrefab = null;
	[SerializeField] protected int maxAmmo = 10;
	protected int ammo;
	protected Pooler poolScript = null;
	protected Transform gameTransform = null;

	[Header("Fire Rate")]
	[SerializeField] protected float fireRate = 0.5f;	
	protected bool canFire = true;

	[Header("User Interface")]
	protected Slider weaponSlider = null;
	protected Slider fireRateSlider = null;


	#region Properties
	public int Ammo
	{
		get
		{
			return ammo;
		}

		set
		{
			ammo = value;
			weaponSlider.value = ammo;

			if (ammo <= 0)
			{
				canFire = false;
			}
		}
	}
	#endregion


	protected virtual void Awake()
	{
		poolScript = Pooler.Instance;
		gameTransform = poolScript.transform.parent;

		weaponSlider = GameController.Instance.weaponSlider;
		fireRateSlider = GameController.Instance.fireRateSlider;

		foreach (Transform item in GetComponentsInChildren<Transform>()[1])
		{
			weaponTransforms.Add(item);
		}
	}


	protected virtual void OnEnable()
	{
		ammo = maxAmmo;

		//User Interface
		weaponSlider.maxValue = maxAmmo;
		weaponSlider.value = ammo;
		fireRateSlider.maxValue = fireRate;
	}


	public abstract void Fire();


	protected virtual void SpawnBullet(Transform origin)
	{
		GameObject bullet = poolScript.GetProjectile(bulletPrefab);

		bullet.transform.position = origin.position;
		bullet.transform.rotation = origin.rotation;
		bullet.transform.parent = gameTransform;
		bullet.SetActive(true);

		Ammo--;

		StartCoroutine(Reload(fireRate));
	}


	protected IEnumerator Reload(float time)
	{
		canFire = false;
		float timer = time;

		while (timer > 0.0f)
		{
			timer -= Time.deltaTime;

			if (timer < 0.0f)
			{
				timer = 0.0f;
			}

			fireRateSlider.value = timer;

			yield return null;
		}

		if (ammo > 0)
		{
			canFire = true;
		}
	}
}
