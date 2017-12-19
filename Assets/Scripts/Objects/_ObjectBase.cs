using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
	[Header("Setup")]
	protected Transform thisTransform = null;
	protected Collider2D thisCollider = null;
	protected SpriteRenderer thisSprite = null;
	protected static Transform poolContainer = null;


	protected virtual void Awake()
	{
		thisTransform = GetComponent<Transform>();
		thisCollider = GetComponent<Collider2D>();
		thisSprite = GetComponentInChildren<SpriteRenderer>();

		if (poolContainer == null)
		{
			poolContainer = Pooler.Instance.poolTransform;
		}
	}


	public virtual void Remove()
	{
		gameObject.SetActive(false);
		thisTransform.SetParent(poolContainer);
	}
}
