using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private EquipmentController equipmentScript = null;
	[Header("Movement")]
	[SerializeField] private float strafeMultiplier = 20f;
	[SerializeField] private float rotationMultiplier = 90f;
	[SerializeField] private float rotationClamp = 45f;

	private Transform thisTransform = null;
	private SpriteRenderer thisSprite = null;
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
		float accelerationX = (float)System.Math.Round((double)Input.acceleration.x, 2);

		// Horizontal movement
		float xMove = accelerationX * strafeMultiplier * Time.deltaTime;
		thisTransform.Translate(new Vector2(xMove, 0f), Space.World);
		float xClamp = Mathf.Clamp(thisTransform.position.x, -xBound, xBound);
		thisTransform.position = new Vector2(xClamp, thisTransform.position.y);

		// Yaw rotation
		float zRot = Mathf.Clamp(accelerationX * rotationMultiplier, -rotationClamp, rotationClamp);
		thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.Euler(0f, 0f, -zRot), 0.5f);

		// Touch input
		if (Input.touchCount == 1 && equipmentScript.weaponScript != null)
		{
			equipmentScript.weaponScript.Fire();
		}
	}
}