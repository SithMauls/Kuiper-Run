# Kuiper-Run

public class Spawner : Monobehavior
{
	[SerializeField] private Pooler poolscript = null;
    [SerializeField] private float spawnInterval = 1.0f;

    private void Awake()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void SpawnObject()
    {
		GameObject obj = poolScript.GetRandomObject();

		obj.transform.position = AboveScreen(obj);
		obj.transform.parent = thisTransform;
		obj.SetActive(true);

        // spawnInterval = baseSpawnTine / (currentShipSpeed / averageShipSpeed)
		spawnInterval = 0.5f / (GameController.Instance.shipSpeed / 10.0f);
		Invoke("SpawnObject", spawnInterval);
	}

    private Vector2 AboveScreen(GameObject obj)
	{
		float x = Random.Range(-screenSize.x, screenSize.x);
		float y = screenSize.y + obj.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2f;

		return new Vector2(x, y);
	}