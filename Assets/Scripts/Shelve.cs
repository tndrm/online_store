using UnityEngine;

public class Shelve : MonoBehaviour
{
	[SerializeField] GameObject shalve;
	public Vector2Int gridDimensions = new Vector2Int(5, 5);
	public float spacing = 1.0f;
	private Product productPrefab;
	private Product itemToTake;


	public void SpawnObjects(Product tradeItem)
	{
		productPrefab = tradeItem;
		Vector3 center = shalve.transform.position;
		Vector3 spawnPosition = center - new Vector3((gridDimensions.x - 1) * spacing / 2, -.2f, (gridDimensions.y - 1) * spacing / 2);
		for (int i = 0; i < gridDimensions.x; i++)
		{
			for (int j = 0; j < gridDimensions.y; j++)
			{
				Instantiate(tradeItem, spawnPosition + new Vector3(i * spacing, 0, j * spacing), Quaternion.identity, transform);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		ProductHolder holder = other.gameObject.GetComponent<ProductHolder>();
		if (holder)
		{
			InstantiateNextProduct();
			Product takenItem = holder.TakeItem(itemToTake);
			if(takenItem && holder.isMainPlayer) GetComponent<AudioSource>().Play();
		}
	}

	private void InstantiateNextProduct()
	{
		if (!itemToTake || itemToTake.transform.parent != gameObject)

			itemToTake = Instantiate(productPrefab, transform.position, Quaternion.identity, transform);
	}
}