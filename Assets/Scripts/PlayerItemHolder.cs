using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHolder : MonoBehaviour
{
	[SerializeField] Transform spawnPoint;
	[SerializeField] int maxHoldingItems;
	private List<TradeItem> tradeItemList;
	private HoldedItemSaver saver;

	private void Awake()
	{
		tradeItemList = new List<TradeItem>();
		saver = this.GetComponent<HoldedItemSaver>();
	}
	public void TakeItem(TradeItem newItem)
	{
		if(tradeItemList.Count < maxHoldingItems)
		{		Debug.Log(newItem.GetItemType);

			TradeItem newObj = Instantiate(newItem, spawnPoint.position, Quaternion.identity, transform);
			tradeItemList.Add(newObj);
			saver.SaveHoldItem(newObj);
		}
	}
	public List<TradeItem> PutItems(List<TradeItem> needList)
	{
		List<TradeItem> crossedList = new List<TradeItem>();

		foreach (TradeItem holdedItem in tradeItemList)
		{
			foreach(TradeItem neededItem in needList)
			{
				if(holdedItem.GetItemType == neededItem.GetItemType){
					crossedList.Add(holdedItem);
					break;
				};
			}
		}

		foreach(TradeItem crossed in crossedList) {
			tradeItemList.Remove(crossed);
			saver.SaveHoldItem();
		}
		return crossedList;
	}
}
