using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PackingTable : MonoBehaviour
{
	[SerializeField] Transform productsStackPoint;
	[SerializeField] TradeItem packedOrderPrefab;
	public List<TradeItem> nededItemsList;
	private List<TradeItem> readyOrders;
	private List<TradeItem> readyForPaking;
	public OrderShowing orderShowing;

	private GameController gameController;

	void Start()
	{
		readyOrders = new List<TradeItem>();
		readyForPaking = new List<TradeItem>();
		gameController = (GameController)FindObjectOfType(typeof(GameController));

	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerItemHolder holder = other.gameObject.GetComponent<PlayerItemHolder>();
		if (holder)
		{
			for (int i = 0; i < nededItemsList.Count; i++) 
			{
				TradeItem puttedItem = holder.PutItem(nededItemsList[i]);
				if (puttedItem) PutOnTable(puttedItem);
			}
			if (readyOrders.Count > 0)
			{
				readyOrders.Remove(holder.TakeItem(readyOrders.Last()));
			}
			if (nededItemsList.Count > 0)
			{
				UpdateNededListShowing();
			}
			else
			{
				PackOrder();
				gameController.ShowNextOrder();
			}
		}
	}

	private void PutOnTable(TradeItem item)
	{
		item.transform.SetParent(transform, false);
		item.transform.position = GetPutPoint(item.gameObject);
		item.transform.rotation = Quaternion.identity;
		readyForPaking.Add(item);
		int index = nededItemsList.FindLastIndex(nededItem => nededItem.GetItemType == item.GetItemType);
		nededItemsList.RemoveAt(index);
	}

	private Vector3 GetPutPoint(GameObject currentItem)
	{
		Vector3 putPoint = productsStackPoint.position;
		putPoint = GetTopPointOfStack();
		Bounds currentItemBounds = currentItem.GetComponent<Renderer>().bounds;
		float halfHeight = (currentItemBounds.max.y - currentItemBounds.min.y) / 2;
		putPoint.y += halfHeight;
		return putPoint;
	}

	private Vector3 GetTopPointOfStack()
	{
		Vector3 putPoint = productsStackPoint.position;
		if (readyForPaking.Any())
		{
			TradeItem lastProductOnTable = readyForPaking.Last();
			Bounds lastItemBounds = lastProductOnTable.GetComponent<Renderer>().bounds;
			putPoint.y = Math.Max( putPoint.y, lastItemBounds.max.y );
		}
		if (readyOrders.Any())
		{
			TradeItem lastOrderOnTable = readyOrders.Last();
			Bounds lastItemBounds = lastOrderOnTable.GetComponent<Renderer>().bounds;
			putPoint.y = Math.Max(putPoint.y, lastItemBounds.max.y);
		}
		return putPoint;
	}

	private void PackOrder()
	{
		int totalCost = 0;
		foreach (TradeItem product in readyForPaking)
		{
			totalCost += product.GetCost;
			Destroy(product.gameObject);
		}
		TradeItem package = Instantiate(packedOrderPrefab, GetPutPoint(packedOrderPrefab.gameObject), Quaternion.identity, transform);
		package.SetCost(totalCost);
		readyOrders.Add(package);
		readyForPaking.Clear();
	}

	public void UploadNextOrder(List<TradeItem> newOrderItems)
	{
		nededItemsList = newOrderItems;
		UpdateNededListShowing();
	}

	private void UpdateNededListShowing()
	{
		orderShowing.UpdateItemsListShowing(nededItemsList);

	}
}
