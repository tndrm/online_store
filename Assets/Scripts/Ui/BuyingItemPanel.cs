using System;
using TMPro;
using UnityEngine;

public class BuyingItemPanel : MonoBehaviour
{
	public TMP_Text coinText;
	public TMP_Text productText;
	public event EventHandler OnItemBuy;


	public void SetItem(TradeItem product)
	{
		coinText.text = product.GetBuyingCost.ToString();
		productText.text = "Do you want to add shelve with " + product.GetItemType + "?";
	}

	public void BuyItem()
	{
		OnItemBuy?.Invoke(this, EventArgs.Empty);
		ClosePanel();
	}
	private void ClosePanel()
	{
		gameObject.SetActive(false);

	}
}
