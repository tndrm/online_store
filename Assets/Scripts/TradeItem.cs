using System;
using UnityEngine;

public class TradeItem : MonoBehaviour
{
	[SerializeField] string itemType;
	[SerializeField] GameObject spritePrefab;
	[SerializeField] int cost;
	public string GetItemType => itemType;
	public GameObject GetItemSprite => spritePrefab;
	public int GetCost => cost;
}
