using UnityEngine;

public class AsteroidBrown : ObjectBase
{
	[Header("Attributes")]
	[SerializeField] private float speed = 0f;
	[SerializeField] private float maxHealth = 100f;
	private float health;

	[Header("Particles")]
	private ParticleSystem particleShatter = null;
	private float shatterDuration;


	#region Properties
	public float Health
	{
		get
		{
			return health;
		}

		set
		{
			health = value;

			if (health <= 0)
			{
				Kill();
			}
		}
	}
	#endregion


	protected override void Awake()
	{
		base.Awake();

		particleShatter = GetComponentInChildren<ParticleSystem>();
		shatterDuration = particleShatter.main.duration;
	}


	private void OnEnable()
	{
		health = maxHealth;
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
			case "Projectile":
				float damage = collision.gameObject.GetComponent<_Projectile>().damage;
				Health -= damage;

				break;

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