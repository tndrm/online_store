using TMPro;
using UnityEngine;

public class BankController : MonoBehaviour
{
	public TMP_Text coinText;
	private int coinCount = 0;
	private int lastValue = 0;
	public float duration = 1f;
	private float timer;
	private int currentshowValue;
	private BankSavingService saver;

	private void Start()
	{
		saver = GetComponent<BankSavingService>();
		timer = 0f;
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer < duration || currentshowValue != coinCount)
		{
			currentshowValue = (int)Mathf.Round(Mathf.Lerp(lastValue, coinCount, timer / duration));
			coinText.text = currentshowValue.ToString();
		}
	}
	public void Increace(int coinValue)
	{
		lastValue = coinCount;
		coinCount += coinValue;
		timer = 0;
		saver.SaveBalance(coinCount);
	}
	public void Reduce(int coinValue)
	{
		lastValue = coinCount;
		coinCount -= coinValue;
		timer = 0;
		saver.SaveBalance(coinCount);
	}

	public void SetBalance(int balance)
	{
		coinCount = balance;
	}
}