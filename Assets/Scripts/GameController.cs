using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public ParticleSystem stars = null;
	public ParticleSystem jet = null;

	[Header("Progress")]
	public float score = 0.0f;
	public int level = 0;
	public float shipSpeed = 5.0f;
	[Header("User Interface")]
	public Text scoreText = null;
	public Slider powerSlider = null;
	public Slider weaponSlider = null;
	public Slider fireRateSlider = null;
	public Slider shieldSlider = null;
	public Text itemText = null;

	private static GameController instance = null;


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
			StopAllCoroutines();
			StartCoroutine(Move(shipSpeed, value, 1.0f));
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
	}
	
	private void Update()
	{
		score += shipSpeed * Time.deltaTime;
		scoreText.text = Mathf.FloorToInt(score).ToString() + " Xm";
	}
}