using UnityEngine;

public class HoldedItemSaver : MonoBehaviour
{

	private GameController gameController;
	void Start()
    {
		gameController = (GameController)FindObjectOfType(typeof(GameController));
		GetLastHoldedItem();

	}
	public void SaveHoldItem(Product item = null)
	{
		if (item)
		{
			PlayerPrefs.SetString("ItemType", item.GetComponent<Product>().GetProductType);
			PlayerPrefs.SetInt("ItemCost", item.GetComponent<Product>().GetCost);
		}
		else
		{
			PlayerPrefs.DeleteKey("ItemType");
			PlayerPrefs.DeleteKey("ItemCost");
		}
		PlayerPrefs.Save();
	}
	private void GetLastHoldedItem()
	{
		if (PlayerPrefs.HasKey("ItemType"))
		{
			string itemType = PlayerPrefs.GetString("ItemType");
			Product productPrefab = gameController.openedProductList.Find(item => item.GetProductType == itemType);
			if (productPrefab)
			{
				Product product = Instantiate(productPrefab);
				this.GetComponent<ProductHolder>().TakeItem(product);
			}
		}
	}
}
