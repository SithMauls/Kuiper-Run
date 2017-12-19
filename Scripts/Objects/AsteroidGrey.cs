using UnityEngine;

public class AsteroidGrey : ObjectBase
{
	[Header("Attributes")]
	[SerializeField] private float speed = 0f;

	[Header("Particles")]
	private ParticleSystem particleShatter = null;
	private float shatterDuration;


	protected override void Awake()
	{
		base.Awake();

		particleShatter = GetComponentInChildren<ParticleSystem>();
		shatterDuration = particleShatter.main.duration;
	}


	private void OnEnable()
	{
		thisCollider.enabled = true;
		thisSprite.enabled = true;

		particleShatter.Stop();

		thisTransform.Rotate(0f, 0f, Random.Range(0, 360));
	}


	private void Update()
	{
		speed = GameController.Instance.shipSpeed;

		thisTransform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Player":
				Kill();

				break;

			case "Shield":
				Kill();

				break;

			default:
				break;
		}
	}


	private void Kill()
	{
		thisCollider.enabled = false;
		thisSprite.enabled = false;

		particleShatter.Play();

		Invoke("Remove", shatterDuration);
	}
}