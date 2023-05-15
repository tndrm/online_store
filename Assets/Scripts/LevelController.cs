using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[SerializeField] List<Product> allProductsList;
	[SerializeField] BuyingItemPanel buyingItemPanel;
	[SerializeField] HirePanel hirePanel;
	[SerializeField] GameObject employerPrefab;
	[SerializeField] int hireEmployerCost;
	
	private GameController gameController;
 	private List<Product> upgradeProductList;
	private BankController bank;
	private int nextProductToBuyCost = 0;
	private Product nextItepToBuy;
	private bool isEmployerHired = false;
	private void Start()
	{
		bank = (BankController)FindObjectOfType(typeof(BankController));
		gameController = GetComponent<GameController>();

		bank.OnBalanceChange += Upgrade;
		buyingItemPanel.OnItemBuy += BuyNextShelve;
		hirePanel.OnHireEmployer += HireEmployer;
	}
	private void Upgrade(object sender, int balance)
	{
		if (upgradeProductList.Count>0 && CheckIsEnothForUpgrade(balance))
		{
			ShowBuyingItemPanel();
		}
		if(!isEmployerHired && CheckIsEnothForHire(balance))
		{
			ShowHireItemPanel();
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

	private void HireEmployer(object sender, EventArgs e)
	{
		Instantiate(employerPrefab);
		isEmployerHired = true;
	}

	private void ShowBuyingItemPanel()
	{
		buyingItemPanel.gameObject.SetActive(true);
		buyingItemPanel.SetItem(nextItepToBuy);
	}
	private void ShowHireItemPanel()
	{
		hirePanel.gameObject.SetActive(true);
		hirePanel.SetItem(hireEmployerCost);
	}
	private bool CheckIsEnothForUpgrade(int balance)
	{
		if (nextProductToBuyCost == 0)
		{
			FindNextItemToBuy();
		}
		return balance >= nextProductToBuyCost;
	}
	private bool CheckIsEnothForHire(int balance)
	{
		return balance >= hireEmployerCost;
	}


	private void FindNextItemToBuy()
	{
		foreach(Product product in upgradeProductList)
		{
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
