using System;
using TMPro;
using UnityEngine;

public class HirePanel : MonoBehaviour
{
	public TMP_Text coinText;
	public event EventHandler OnHireEmployer;


	public void SetItem(int employerCost)
	{
		coinText.text = employerCost.ToString();
	}

	public void HireEmployer()
	{
		OnHireEmployer?.Invoke(this, EventArgs.Empty);
		ClosePanel();
	}
	private void ClosePanel()
	{
		gameObject.SetActive(false);

	}
}
