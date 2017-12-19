using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[Header("Setup")]
	private static GameController instance = null;
	private Transform thisTransform = null;
	[SerializeField] private ParticleSystem stars;
	[SerializeField] private ParticleSystem jet;

	[Header("Progress")]
	public float score = 0.0f;
	public int level = 0;
	public float shipSpeed = 5.0f;

	[Header("Spawns")]
	[SerializeField] private float spawnInterval = 5f;
	[HideInInspector] public Pooler poolScript = null;
	private Vector2 screenSize;

	[Header("User Interface")]
	public Text scoreText = null;
	public Slider weaponSlider = null;
	public Slider fireRateSlider = null;
	public Slider shieldSlider = null;
	public Text itemText = null;


	#region Properties
	public static GameController Instance
	{
		get
		{
			return instance;
		}
	}

	public float ShipSpeed
	{
		get
		{
			return shipSpeed;
		}

		set
		{
			//StartCoroutine(Move(shipSpeed, value, 1.0f));
			shipSpeed = value;

			ParticleSystem.MainModule starsMain = stars.main;
			starsMain.simulationSpeed = value / 5.0f;

			ParticleSystem.MainModule jetMain = jet.main;
			jetMain.startSpeed = value;
		}
	}
	#endregion

	IEnumerator Move(float start, float end, float time) {
		float timer = 0.0f;

		while (timer < time) {
			timer += Time.deltaTime;

			if (timer > time) {
				timer = time;
			}

			shipSpeed = Mathf.Lerp(start, end, timer / time);

			ParticleSystem.MainModule starsMain = stars.main;
			starsMain.simulationSpeed = Mathf.Lerp(start / 5.0f, end / 5.0f, timer / time);

			yield return null;
		}
	}

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}

		thisTransform = GetComponent<Transform>();
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
	}


	private void OnEnable()
	{
		Invoke("SpawnObject", spawnInterval);
	}

	
	private void Update()
	{
        //Update and print score
		score += shipSpeed * Time.deltaTime;
		scoreText.text = Mathf.FloorToInt(score).ToString() + " Xm";	
		
		float acceleration = (float)System.Math.Round((double)Input.acceleration.y, 2);
		acceleration = Mathf.Clamp(acceleration, -0.75f, -0.25f);
		ShipSpeed = 5.0f + (acceleration + 1.0f) * 10.0f;

		ParticleSystem.ShapeModule jetShape = jet.shape;
		jetShape.radius = 1.0f - (1.0f + acceleration);
	}


	private void SpawnObject()
	{
		GameObject obj = poolScript.GetRandomObject();

		obj.transform.position = AboveScreen(obj);
		obj.transform.parent = thisTransform;
		obj.SetActive(true);

		spawnInterval = 0.5f / (shipSpeed / 10.0f);
		Invoke("SpawnObject", spawnInterval);
	}


	private Vector2 AboveScreen(GameObject obj)
	{
		float x = Random.Range(-screenSize.x, screenSize.x);
		float y = screenSize.y + obj.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2f;

		return new Vector2(x, y);
	}
}