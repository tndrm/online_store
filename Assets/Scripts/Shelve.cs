using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Shelve : MonoBehaviour
{
	[SerializeField] GameObject shalve;
	public Vector2Int gridDimensions = new Vector2Int(5, 5);
	public float spacing = 1.0f;
	public float spawnDuration = 0.5f;
	public float dropDuration = 1.0f; // Длительность анимации подъема
	public float dropHeight =.1f; // Высота подъема объекта
	private Product productPrefab;
	private Product itemToTake;


	private void Start()
	{

	}

	private IEnumerator ShowShelveAnim()
	{
		transform.DOScale(Vector3.one, spawnDuration).SetEase(Ease.OutBack);
		yield return new WaitForSeconds(1f);
		SpawnObjects();
		InstantiateNextProduct();

	}

	public void SetSettings(Product product)
	{
		productPrefab = product;

		transform.localScale = Vector3.zero;
		StartCoroutine(ShowShelveAnim());




	}
	private void SpawnObjects()
	{
		Vector3 center = shalve.transform.position;
		Vector3 spawnPosition = center - new Vector3((gridDimensions.x - 1) * spacing / 2, -.2f, (gridDimensions.y - 1) * spacing / 2);
		for (int i = 0; i < gridDimensions.x; i++)
		{
			for (int j = 0; j < gridDimensions.y; j++)
			{
				Product prodItem = Instantiate(productPrefab, spawnPosition + new Vector3(i * spacing, 0, j * spacing), Quaternion.identity, transform);
				Vector3 initialPosition = prodItem.transform.position;
				Vector3 finalPosition = initialPosition + Vector3.up * dropHeight;

				// Устанавливаем начальную позицию объекта
				prodItem.transform.position = finalPosition;

				// Запускаем анимацию подъема объекта
				prodItem.transform.DOMove(initialPosition, dropDuration).SetEase(Ease.OutBounce);
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
		if (itemToTake == null || itemToTake.transform.parent != transform) {
			itemToTake = Instantiate(productPrefab, transform.position, Quaternion.identity, transform);
		}
	}
}