using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : ObjectBase
{
	[Header("Movement")]
	[SerializeField] private float speed = 0f;
	[SerializeField] private float rotationSpeed = 0f;

	[Header("Items")]
	[SerializeField] public Item itemType;
	[SerializeField] private List<GameObject> itemList;
	[SerializeField] public GameObject currentItem = null;


	#region Enumerators
	public enum Item { Weapon, Shield };
	#endregion


	private void OnEnable()
	{
		currentItem = itemList[Random.Range(0, itemList.Count)];

		thisTransform.Rotate(0f, 0f, Random.Range(0, 360));
	}


	protected virtual void Update()
	{
		speed = GameController.Instance.shipSpeed;

		thisTransform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
		thisTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
	}
}
