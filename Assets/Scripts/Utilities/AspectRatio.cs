using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour
{
	private Camera thisCamera = null;
	[SerializeField] private float pixelScale = 2;
	[SerializeField] private float pixelsPerUnit = 50;

	private void Awake()
	{
		thisCamera = GetComponent<Camera>();

		Debug.Log(Screen.height);
		thisCamera.orthographicSize = ((Screen.height) / (pixelScale * pixelsPerUnit)) * 0.5f;
	}

}
