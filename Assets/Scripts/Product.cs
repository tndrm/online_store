using UnityEditor;
using UnityEngine;

public class Product : MonoBehaviour
{
	[SerializeField] string productType;
	[SerializeField] GameObject productSpritePrefab;
	[SerializeField] int cost;
	[SerializeField] int productBuyingCost;
	[SerializeField] Transform shelvePosition;
	public string GetProductType => productType;
	public GameObject GetSprite => productSpritePrefab;
	public int GetCost => cost;
	public int GetBuyingCost => productBuyingCost;
	public Transform GetShelvePosition => shelvePosition;

	public void SetCost(int newCost)
	{
		cost = newCost;
	}
}
