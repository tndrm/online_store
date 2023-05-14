using UnityEngine;

public class TradeItem : MonoBehaviour
{
	[SerializeField] string itemType;
	[SerializeField] GameObject spritePrefab;
	[SerializeField] int cost;
	[SerializeField] int buyingCost;
	[SerializeField] Transform shelvePosition;
	public string GetItemType => itemType;
	public GameObject GetItemSprite => spritePrefab;
	public int GetCost => cost;
	public int GetBuyingCost => buyingCost;
	public Transform GetShelvePosition => shelvePosition;
	public void SetCost(int newCost)
	{
		cost = newCost;
	}
}
