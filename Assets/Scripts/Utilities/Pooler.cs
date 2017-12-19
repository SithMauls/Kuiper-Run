using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
	[Header("Setup")]
	public Transform poolTransform = null;
	private static Pooler instance = null;

	[Header("Pool Arrays")]
	public PoolStruct[] objectArray;
	public PoolStruct[] projectileArray;


	#region Properties
	public static Pooler Instance
	{
		get
		{
			return instance;
		}
	}
	#endregion


	#region Structs
	[System.Serializable]
	public struct PoolStruct
	{
		public GameObject objectToPool;
		public int poolAmount;
		public int spawnLevel;
		public bool canExpand;

		[HideInInspector] public List<GameObject> poolList;
	}
	#endregion


	private void Awake()
	{
		// Instantiate Pooler singleton
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}

		// Instantiate pooled objects
		foreach (PoolStruct item in objectArray)
		{
			for (int i = 0; i < item.poolAmount; i++)
			{
				GameObject obj = Instantiate(item.objectToPool, poolTransform);

				obj.SetActive(false);
				item.poolList.Add(obj);
			}
		}

		// Instantiate pooled projectiles
		foreach (PoolStruct item in projectileArray)
		{
			for (int i = 0; i < item.poolAmount; i++)
			{
				GameObject obj = Instantiate(item.objectToPool, poolTransform);

				obj.SetActive(false);
				item.poolList.Add(obj);
			}
		}

		GameController.Instance.poolScript = this;
	}


	public GameObject GetObject(GameObject prefab)
	{
		// Find object in pool
		foreach (PoolStruct item in objectArray)
		{
			if (item.objectToPool == prefab)
			{
				for (int i = 0; i < item.poolList.Count; i++)
				{
					if (!item.poolList[i].activeInHierarchy)
					{
						return item.poolList[i];
					}
				}

				// Instantiate new object if none are available
				if (item.canExpand)
				{
					GameObject newObject = Instantiate(item.objectToPool, poolTransform);

					newObject.SetActive(false);
					item.poolList.Add(newObject);
					return newObject;
				}
			}
		}

		return null;
	}


	public GameObject GetProjectile(GameObject prefab)
	{
		// Find projectile in pool
		foreach (PoolStruct item in projectileArray)
		{
			if (item.objectToPool == prefab)
			{
				for (int i = 0; i < item.poolList.Count; i++)
				{
					if (!item.poolList[i].activeInHierarchy)
					{
						return item.poolList[i];
					}
				}

				// Instantiate new projectile if none are available
				if (item.canExpand)
				{
					GameObject newProjectile = Instantiate(item.objectToPool, poolTransform);

					newProjectile.SetActive(false);
					item.poolList.Add(newProjectile);
					return newProjectile;
				}
			}
		}

		return null;
	}


	public GameObject GetRandomObject()
	{
		int currentLevel = GameController.Instance.level;
		int total = 0;

		foreach (PoolStruct item in objectArray)
		{
			if (item.spawnLevel <= currentLevel)
			{
				total += item.poolAmount;
			}
		}

		float randomPoint = Random.value * total;

		foreach (PoolStruct item in objectArray)
		{
			if (item.spawnLevel <= currentLevel)
			{
				if (randomPoint < item.poolAmount)
				{
					return GetObject(item.objectToPool);
				}
				else
				{
					randomPoint -= item.poolAmount;
				}
			}
		}

		return null;
	}
}
