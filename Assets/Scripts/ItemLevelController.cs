using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLevelController : MonoBehaviour
{
	[SerializeField] List<TradeItem> allTradeItemsList;
	[SerializeField] BuyingItemPanel buyingItemPanel;
	private GameController gameController;
 	private List<TradeItem> upgradeTradeItemList;
	private BankController bank;
	private int nextItemToBuyCost = 0;
	private TradeItem nextItepToBuy;
	private void Start()
	{
		bank = (BankController)FindObjectOfType(typeof(BankController));
		gameController = GetComponent<GameController>();

		bank.OnBalanceChange += Upgrade;
		buyingItemPanel.OnItemBuy += BuyNextShelve;
	}
	private void Upgrade(object sender, int balance)
	{
		if (upgradeTradeItemList.Count>0 && CheckIsEnothForUpgrade(balance))
		{
			ShowBuyingItemPanel();
		}
	}

	private void BuyNextShelve(object sender, EventArgs e)
	{
		bank.Reduce(nextItepToBuy.GetBuyingCost);
		upgradeTradeItemList.Remove(nextItepToBuy);
		gameController.AddNextShelve(nextItepToBuy);
		nextItemToBuyCost = 0;
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
		if (nextItemToBuyCost == 0)
		{
			FindNextItemToBuy();
		}
		return nextItemToBuyCost <= balance;
	}

	private void OnDestroy()
	{
		bank.OnBalanceChange -= Upgrade;
	}

	private void FindNextItemToBuy()
	{
		foreach(TradeItem product in upgradeTradeItemList)
		{
			if (nextItemToBuyCost >= product.GetBuyingCost || nextItemToBuyCost == 0)
			{
				nextItemToBuyCost = product.GetBuyingCost;
				nextItepToBuy = product;
			}
		}
	}

	public List<TradeItem> GetOpenedTradeItems(List<string> productTypes)
	{
		List<TradeItem> avalibleProducts = new List<TradeItem>();
		foreach(string productType in productTypes)
		{
			avalibleProducts.Add( allTradeItemsList.Find(item => item.GetItemType == productType));
		}
		SetItemsforUpgrade(avalibleProducts);
		return avalibleProducts;
	}

	private void SetItemsforUpgrade(List<TradeItem> avalibleProducts)
	{
		upgradeTradeItemList = allTradeItemsList.Where(f => !avalibleProducts.Any(t => t.GetItemType == f.GetItemType)).ToList();

	}
}
