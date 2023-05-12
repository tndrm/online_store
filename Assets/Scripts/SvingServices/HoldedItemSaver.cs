using UnityEngine;

public class HoldedItemSaver : MonoBehaviour
{

	private GameController gameController;
	void Start()
    {
		gameController = (GameController)FindObjectOfType(typeof(GameController));
		GetLastHoldedItem();

	}
	public void SaveHoldItem(TradeItem item = null)
	{
		if (item)
		{
			PlayerPrefs.SetString("ItemType", item.GetComponent<TradeItem>().GetItemType);
			PlayerPrefs.SetInt("ItemCost", item.GetComponent<TradeItem>().GetCost);
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
			TradeItem productPrefab = gameController.productList.Find(item => item.GetItemType == itemType);
			TradeItem product = Instantiate(productPrefab);
			this.GetComponent<PlayerItemHolder>().TakeItem(product);
		}
	}
}
