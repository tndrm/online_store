using UnityEngine;

public class Shelve : MonoBehaviour
{
	[SerializeField] TradeItem tradeItem;
	[SerializeField] GameObject shalve;
	public Vector2Int gridDimensions = new Vector2Int(5, 5);
	public float spacing = 1.0f;

	private void OnEnable()
	{
		SpawnObjects();
	}

	private void SpawnObjects()
	{
		Vector3 center = shalve.transform.position;
		Vector3 spawnPosition = center - new Vector3((gridDimensions.x - 1) * spacing / 2, -.2f, (gridDimensions.y - 1) * spacing / 2);
		for (int i = 0; i < gridDimensions.x; i++)
		{
			for (int j = 0; j < gridDimensions.y; j++)
			{
				TradeItem newObj = Instantiate(tradeItem, spawnPosition + new Vector3(i * spacing, 0, j * spacing), Quaternion.identity, transform);
			}
		}
	}
}
