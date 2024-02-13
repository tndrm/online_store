using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PackingTable : MonoBehaviour
{
	[SerializeField] Transform productsStackPoint;
	[SerializeField] Product packedOrderPrefab;
	[SerializeField] AudioClip putSound;
	[SerializeField] AudioClip pakeOrderSound;
	[SerializeField] AudioClip takeOrderSound;
	public ProductHolder tableOwner;
	public List<Product> nededItemsList;
	private AudioSource audioSource;
	private List<Product> readyOrders;
	private List<Product> readyForPaking;
	public OrderShowing orderShowing;
	private OrderGenerator orderGenerator;

	void Start()
	{
		readyOrders = new List<Product>();
		readyForPaking = new List<Product>();
		audioSource = GetComponent<AudioSource>();
		orderGenerator = GetComponent<OrderGenerator>();

	}

	private void OnTriggerEnter(Collider other)
	{
		ProductHolder holder = other.gameObject.GetComponent<ProductHolder>();
		if (holder&& holder == tableOwner)
		{
			for (int i = 0; i < nededItemsList.Count; i++) 
			{
				Product puttedItem = holder.PutItem(nededItemsList[i]);
				if (puttedItem)
				{
					PutOnTable(puttedItem);
					if(holder.isMainPlayer) audioSource.PlayOneShot(putSound);
				}
			}
			if (readyOrders.Count > 0)
			{
				Product packedOrder = holder.TakeItem(readyOrders.Last());
				if (holder.isMainPlayer) audioSource.PlayOneShot(takeOrderSound);
				readyOrders.Remove(packedOrder);
			}
			if (nededItemsList.Count > 0)
			{
				UpdateNededListShowing();
			}
			else
			{
				PackOrder();
				if (holder.isMainPlayer) audioSource.PlayOneShot(pakeOrderSound);
				orderGenerator.ShowNextOrder();
			}
		}
	}

	private void PutOnTable(Product item)
	{
		item.transform.SetParent(transform, false);
		item.transform.position = GetPutPoint(item.gameObject);
		item.transform.rotation = Quaternion.identity;
		readyForPaking.Add(item);
		int index = nededItemsList.FindLastIndex(nededItem => nededItem.GetProductType == item.GetProductType);
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
			Product lastProductOnTable = readyForPaking.Last();
			Bounds lastItemBounds = lastProductOnTable.GetComponent<Renderer>().bounds;
			putPoint.y = Math.Max( putPoint.y, lastItemBounds.max.y );
		}
		if (readyOrders.Any())
		{
			Product lastOrderOnTable = readyOrders.Last();
			Bounds lastItemBounds = lastOrderOnTable.GetComponent<Renderer>().bounds;
			putPoint.y = Math.Max(putPoint.y, lastItemBounds.max.y);
		}
		return putPoint;
	}

	private void PackOrder()
	{
		int totalCost = 0;
		foreach (Product product in readyForPaking)
		{
			totalCost += product.GetCost;
			Destroy(product.gameObject);
		}
		Product package = Instantiate(packedOrderPrefab, GetPutPoint(packedOrderPrefab.gameObject), Quaternion.identity, transform);
		package.SetCost(totalCost);
		readyOrders.Add(package);
		readyForPaking.Clear();
	}

	public void UploadNextOrder(List<Product> newOrderItems)
	{
		nededItemsList = newOrderItems;
		UpdateNededListShowing();
	}

	private void UpdateNededListShowing()
	{
		orderShowing.UpdateItemsListShowing(nededItemsList);
	}

	public Product GetNextNededItem()
	{
		return nededItemsList[0];
	}
}
