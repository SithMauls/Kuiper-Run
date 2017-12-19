using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Projectile : MonoBehaviour
{
	[Header("Setup")]
	private Transform thisTransform = null;
	private static Transform poolContainer = null;

	[Header("Attributes")]
	[SerializeField] private float speed = 20f;
	public float damage = 50f;


	private void Awake()
	{
		thisTransform = GetComponent<Transform>();

		if (poolContainer == null)
		{
			poolContainer = Pooler.Instance.poolTransform;
		}
	}


	private void Update()
	{
		thisTransform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameObject.SetActive(false);
		thisTransform.parent = poolContainer;
	}
}
