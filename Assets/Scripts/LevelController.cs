using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[SerializeField] List<Product> allProductsList;
	[SerializeField] BuyingItemPanel buyingItemPanel;
	private GameController gameController;
 	private List<Product> upgradeProductList;
	private BankController bank;
	private int nextProductToBuyCost = 0;
	private Product nextItepToBuy;
	private void Start()
	{
		bank = (BankController)FindObjectOfType(typeof(BankController));
		gameController = GetComponent<GameController>();

		bank.OnBalanceChange += Upgrade;
		buyingItemPanel.OnItemBuy += BuyNextShelve;
	}
	private void Upgrade(object sender, int balance)
	{
		Debug.Log(0 + upgradeProductList.ToString());
		if (upgradeProductList.Count>0 && CheckIsEnothForUpgrade(balance))
		{
			Debug.Log(upgradeProductList);
			ShowBuyingItemPanel();
		}
	}

	private void BuyNextShelve(object sender, EventArgs e)
	{
		bank.Reduce(nextItepToBuy.GetBuyingCost);
		upgradeProductList.Remove(nextItepToBuy);
		gameController.AddNextShelve(nextItepToBuy);
		nextProductToBuyCost = 0;
		nextItepToBuy = null;
		FindNextItemToBuy();
	}

	private void ShowBuyingItemPanel()
	{
		buyingItemPanel.gameObject.SetActive(true);
		buyingItemPanel.SetItem(nextItepToBuy);

	}
	private bool CheckIsEnothForUpgrade(int balance)
	{
		Debug.Log(1);
		if (nextProductToBuyCost == 0)
		{
			Debug.Log(2);

			FindNextItemToBuy();
		}
		return nextProductToBuyCost <= balance;
	}


	private void FindNextItemToBuy()
	{
		foreach(Product product in upgradeProductList)
		{
			Debug.Log(3 + product.GetProductType);

			if (nextProductToBuyCost >= product.GetBuyingCost || nextProductToBuyCost == 0)
			{
				nextProductToBuyCost = product.GetBuyingCost;
				nextItepToBuy = product;
			}
		}
	}

	public List<Product> GetOpenedTradeItems(List<string> productTypes)
	{
		List<Product> avalibleProducts = new List<Product>();
		foreach(string productType in productTypes)
		{
			avalibleProducts.Add( allProductsList.Find(item => item.GetProductType == productType));
		}
		SetItemsforUpgrade(avalibleProducts);
		return avalibleProducts;
	}

	private void SetItemsforUpgrade(List<Product> avalibleProducts)
	{
		upgradeProductList = allProductsList.Where(f => !avalibleProducts.Any(t => t.GetProductType == f.GetProductType)).ToList();
	}
	private void OnDestroy()
	{
		bank.OnBalanceChange -= Upgrade;
	}
}
