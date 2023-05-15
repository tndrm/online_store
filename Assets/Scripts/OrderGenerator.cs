using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
	[SerializeField] Vector2 itemsInPackageRange = new Vector2(1, 3f);

	private List<Product> productList;
	private GameController gameController;
	private PackingTable packingTable;
	private List<Product> nextPackage;

	private void Start()
	{
		gameController = (GameController)FindObjectOfType(typeof(GameController));
		gameController.OnProductListChange += UpdateProductList;
		productList = gameController.productList;
		packingTable = GetComponent<PackingTable>();
		nextPackage = new List<Product>();
		ShowNextOrder();

	}
	public void ShowNextOrder()
	{

		if (productList.Count != 0)
		{
			List<Product> nextPackage = generateNextPackage();
			packingTable.UploadNextOrder(nextPackage);
		}
	}
	private List<Product> generateNextPackage()
	{
		int productQuantity = (int)Random.Range(itemsInPackageRange.x, itemsInPackageRange.y);
		for (int i = 0; i < productQuantity; i++)
		{
			int item = (int)Random.Range(0, productList.Count);
			nextPackage.Add(productList[item]);
		}
		return nextPackage;
	}

	private void UpdateProductList(object sender, List<Product> newProductList)
	{
		productList = newProductList;
		if (nextPackage.Count == 0) ShowNextOrder();
	}
}
