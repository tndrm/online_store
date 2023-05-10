using System.Collections.Generic;
using UnityEngine;

public class PackingTable: MonoBehaviour
{
	public List<TradeItem> nededItemsList;
	private List<TradeItem> packedItems;
	public OrderShowing orderShowing;

	private GameController gameController;

	void Start()
    {
		packedItems = new List<TradeItem>();
		gameController = (GameController)FindObjectOfType(typeof(GameController));

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			List<TradeItem> puttedList = other.gameObject.GetComponent<PlayerItemHolder>().PutItems(nededItemsList);

			for(int i =0; i<puttedList.Count; i++)
			{
				puttedList[i].transform.SetParent(transform, false);
				int index = nededItemsList.FindLastIndex(nededItem => nededItem.GetItemType == puttedList[i].GetItemType);
				nededItemsList.RemoveAt(index);
			}
			
			Debug.Log(nededItemsList.Count);

			if(nededItemsList.Count > 0)
			{
				UpdateNededListShowing();
			}
			else
			{
				gameController.ShowNextOrder();
			}
		}
	}

	public void UploadNextOrder(List<TradeItem> newOrderItems) {

		Debug.Log("need" + newOrderItems.Count);
		nededItemsList = newOrderItems;
		UpdateNededListShowing();
	}

	private void UpdateNededListShowing()
	{
		orderShowing.UpdateItemsListShowing(nededItemsList);

	}
}
