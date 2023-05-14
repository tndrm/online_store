using UnityEngine;

public class ProductHolder : MonoBehaviour
{
	[SerializeField] Transform spawnPoint;
	public TradeItem holdedItem;
	private HoldedItemSaver saver;

	private void Awake()
	{
		//saver = this.GetComponent<HoldedItemSaver>();
	}
	public TradeItem TakeItem(TradeItem item)
	{
		TradeItem takenItem = null;

		if (!holdedItem)
		{
			item.transform.SetParent(transform);
			item.transform.position = spawnPoint.position;
			item.transform.rotation = transform.rotation;
			holdedItem = item;
			takenItem = item;
			//saver.SaveHoldItem(item);
		}
		return takenItem;
	}
	public TradeItem PutItem(TradeItem needItem)
	{
		TradeItem putedItem = null;
		if (holdedItem && holdedItem.GetItemType == needItem.GetItemType)
		{
			putedItem = holdedItem;
			holdedItem = null;
			//saver.SaveHoldItem();
		}
		return putedItem;
	}

	public void DiscardHoldedItem()
	{
		if (holdedItem)
		{
			Destroy(holdedItem.gameObject);
			//saver.SaveHoldItem();
		}
	}
}
