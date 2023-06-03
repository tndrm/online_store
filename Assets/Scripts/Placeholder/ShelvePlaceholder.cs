using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShelvePlaceholder : MonoBehaviour
{
	public Slider slider;
	[SerializeField] TMP_Text sliderText;
	[SerializeField] AudioClip byyingSound;
	[SerializeField] AudioClip payingCoinsSound;
	private AudioSource audioSource;
	private Product productPrefab;
	private BankController bank;
	private int spendCoins = 0;

	private int coinsToReduseInSec = 10;

	public event EventHandler<Product> OnShelveBuy;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		bank = (BankController)FindObjectOfType(typeof(BankController));
	}

	private void OnTriggerStay(Collider other)
	{
		ProductHolder holder = other.gameObject.GetComponent<ProductHolder>();

		if (holder && holder.isMainPlayer && bank)
		{
			int redused = bank.Reduce(CountNextReduce());
			spendCoins += redused;
			if (redused > 0 && !audioSource.isPlaying) audioSource.PlayOneShot(payingCoinsSound, .1f);
			UpdateSlider();
			if (spendCoins >= productPrefab.GetBuyingCost)
			{
				audioSource.PlayOneShot(byyingSound);
				BuyCurrentItem();
			}
		}
	}

	private int CountNextReduce()
	{
		if (productPrefab.GetBuyingCost - spendCoins >= coinsToReduseInSec)
		{
			return coinsToReduseInSec;
		}
		else
		{
			return productPrefab.GetBuyingCost - spendCoins;
		}
	}

	public void SetSettings(Product product)
	{
		productPrefab = product;
		slider.maxValue = productPrefab.GetBuyingCost;
		UpdateSlider();
	}

	public void UpdateSlider()
	{
		slider.value = spendCoins;
		sliderText.text = spendCoins.ToString() + " / " + productPrefab.GetBuyingCost.ToString();
	}

	private void BuyCurrentItem()
	{
		OnShelveBuy?.Invoke(this, productPrefab);
		audioSource.PlayOneShot(byyingSound);
		Destroy(gameObject);
	}
}
