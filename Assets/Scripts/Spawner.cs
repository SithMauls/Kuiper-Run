using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Header("Spawns")]
	[SerializeField] private float spawnInterval = 1.0f;

	private Transform thisTransform = null;
	private Vector2 screenSize;
	private Pooler poolScript = null;


	private void Start()
	{
		thisTransform = GetComponent<Transform>();
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		poolScript = Pooler.Instance;
	}

	private void OnEnable()
	{
		Invoke("SpawnObject", spawnInterval);
	}

	private void SpawnObject()
	{
		GameObject obj = poolScript.GetRandomObject();

		obj.transform.position = AboveScreen(obj);
		obj.transform.parent = thisTransform;
		obj.SetActive(true);

	  //spawnInterval = baseSpawnTime / (currentShipSpeed / averageShipSpeed)
		spawnInterval = 0.5f / (GameController.Instance.shipSpeed / 10.0f);

		Invoke("SpawnObject", spawnInterval);
	}

	private Vector2 AboveScreen(GameObject obj)
	{
		float x = Random.Range(-screenSize.x, screenSize.x);
		float y = screenSize.y + 1.0f;

		return new Vector2(x, y);
	}
}
