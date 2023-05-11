using TMPro;
using UnityEngine;

public class BankController : MonoBehaviour
{
	public TMP_Text coinText; 
	private int coinCount = 0;

	private void Start()
	{
		UpdateCoinText();
	}

	private void UpdateCoinText()
	{
		if (coinText != null)
		{
			coinText.text  = coinCount.ToString();
		}
	}

	private void Increace(int coinValue)
	{
		coinCount += coinValue;
		UpdateCoinText();
	}
	private void Reduce(int coinValue)
	{
		coinCount -= coinValue;
		UpdateCoinText();
	}
}