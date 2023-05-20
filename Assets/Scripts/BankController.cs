using System;
using TMPro;
using UnityEngine;

public class BankController : MonoBehaviour
{
	public TMP_Text coinText;
	private int coinBalance = 0;
	private int lastValue = 0;
	public float duration = 1f;
	private float timer;
	private int currentshowValue;

	public event EventHandler<int> OnBalanceChange;
	private void Start()
	{
		timer = 0f;
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer < duration || currentshowValue != coinBalance)
		{
			currentshowValue = (int)Mathf.Round(Mathf.Lerp(lastValue, coinBalance, timer / duration));
			coinText.text = currentshowValue.ToString();
		}
	}
	public void Increace(int coinValue)
	{
		lastValue = coinBalance;
		coinBalance += coinValue;
		timer = 0;

		OnBalanceChange?.Invoke(this, coinBalance);
	}
	public int Reduce(int coinValue)
	{
		lastValue = coinBalance;
		int reduced = 0;
		if (coinBalance > coinValue)
		{
			coinBalance -= coinValue;
			reduced = coinValue;
		}
		else
		{
			reduced = coinBalance;
			coinBalance = 0;
		}
		timer = 0;
		OnBalanceChange?.Invoke(this, coinBalance);
		return reduced;
	}

	public void SetBalance(int balance)
	{
		coinBalance = balance;
	}
}