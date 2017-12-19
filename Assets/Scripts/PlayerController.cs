using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private EquipmentController equipmentScript = null;
	private Transform thisTransform = null;
	private SpriteRenderer thisSprite = null;

	[Header("Movement")]
	[SerializeField] private float speedMultiplier = 20f;
	[SerializeField] private float rotationMultiplier = 90f;
	[SerializeField] private float rotationClamp = 45f;
	private float xBound;


	private void Awake()
	{
		thisTransform = GetComponent<Transform>();
		thisSprite = GetComponentInChildren<SpriteRenderer>();

		// Calculate the screen's width boundary
		xBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0f)).x;
		xBound -= thisSprite.bounds.size.x / 2f;
	}


	private void Update()
	{
		// Reduce accelerometer sensitivity
		float acceleration = (float)System.Math.Round((double)Input.acceleration.x, 2);

		// Horizontal movement
		float xMove = acceleration * speedMultiplier * Time.deltaTime;
		thisTransform.Translate(new Vector2(xMove, 0f), Space.World);

		// Yaw rotation
		float zRot = Mathf.Clamp(acceleration * rotationMultiplier, -rotationClamp, rotationClamp);
		thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.Euler(0f, 0f, -zRot), 0.5f);

		// Clamp X position within screen's width boundary
		float xClamp = Mathf.Clamp(thisTransform.position.x, -xBound, xBound);
		thisTransform.position = new Vector2(xClamp, thisTransform.position.y);

		// Touch input
		if (Input.touchCount == 1 && equipmentScript.weaponScript != null)
		{
			equipmentScript.weaponScript.Fire();
		}
	}
}