using UnityEngine;

public class RemoveOffScreen : MonoBehaviour
{
	private SpriteRenderer thisSprite = null;
	private GameObject parentObject = null;
	private Transform poolContainer = null;

	
	private void Awake()
	{
		thisSprite = GetComponent<SpriteRenderer>();
		parentObject = transform.parent.gameObject;
		poolContainer = Pooler.Instance.poolTransform;
	}


	private void OnBecameInvisible()
	{
		if (parentObject.activeInHierarchy && thisSprite.enabled == true)
		{
			parentObject.SetActive(false);
			parentObject.transform.parent = poolContainer;
		}
	}
}
