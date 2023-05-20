using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployerPlaceholder : MonoBehaviour
{
	public Slider slider;
	[SerializeField] TMP_Text sliderText;
	[SerializeField] AudioClip byyingSound;
	[SerializeField] AudioClip payingCoinsSound;
	private AudioSource audioSource;
	private GameObject employerPrefab;
	private int buyCost;
	private BankController bank;
	private int spendCoins = 0;

	private int coinsToReduseInSec;

	public event EventHandler<GameObject> OnEmployerHire;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();

		bank = (BankController)FindObjectOfType(typeof(BankController));
		buyCost = 300;

		slider.maxValue = buyCost;
		coinsToReduseInSec = buyCost / 100;
		UpdateSlider();
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
			if (spendCoins >= buyCost)
			{
				audioSource.PlayOneShot(byyingSound);
				BuyCurrentItem();
			}
		}
	}

	private int CountNextReduce()
	{
		if (buyCost - spendCoins >= coinsToReduseInSec)
		{
			return coinsToReduseInSec;
		}
		else
		{
			return buyCost - spendCoins;
		}
	}

	public void SetCost(int cost)
	{
		buyCost = cost;
	}

	public void UpdateSlider()
	{
		slider.value = spendCoins;
		sliderText.text = spendCoins.ToString() + " / " + buyCost;
	}

	private void BuyCurrentItem()
	{
		OnEmployerHire?.Invoke(this, employerPrefab);
		audioSource.PlayOneShot(byyingSound);
		Destroy(gameObject);
	}
}
