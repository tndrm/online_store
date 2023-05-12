using UnityEngine;

public class BankSavingService : MonoBehaviour
{
	private BankController bank;
	private void Start()
	{
		bank = GetComponent<BankController>();

		if (PlayerPrefs.HasKey("Balance")){
			int balance = PlayerPrefs.GetInt("Balance");
			bank.SetBalance(balance);
		}
	}
	public void SaveBalance(int balance)
	{
		PlayerPrefs.SetInt("Balance", balance);
		PlayerPrefs.Save();
	}
}
