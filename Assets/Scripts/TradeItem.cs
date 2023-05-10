using System;
using UnityEngine;

public class TradeItem : MonoBehaviour
{
	[SerializeField] String itemType;
	[SerializeField] GameObject spritePrefab;
	public string GetItemType => itemType;
	public GameObject GetItemSprite => spritePrefab;
}
