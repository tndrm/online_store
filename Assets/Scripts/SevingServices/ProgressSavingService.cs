using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressSavingService : MonoBehaviour
{

	private GameController gameController;
	private void Start()          
	{
		gameController = GetComponent<GameController>();
		gameController.OnProductListChange += SaveOpenedShelves;
		gameController.OnEmployerHired += SaveHiredEmployer;
	}

	public bool IsEmloyerHired()
	{
		return PlayerPrefs.HasKey("EmployerHired");
	}
	
	private void SaveHiredEmployer(object sender, EventArgs e)
	{
		PlayerPrefs.SetInt("EmployerHired", 1);

	}

	public List<string> GetOpenedShelves()
	{
		List<string> openedProduct = new List<string>();
		if (PlayerPrefs.HasKey("OpenedShelves"))
		{
			openedProduct = PlayerPrefs.GetString("OpenedShelves").Split(", ").ToList();
		}		
		return openedProduct;
	}

	public void SaveOpenedShelves(object sender, List<Product> products)
	{
		List<string> productTypes = new List<string>();
		foreach(Product product in products)
		{
			productTypes.Add(product.GetProductType);
		}
		PlayerPrefs.SetString("OpenedShelves", String.Join(", ", productTypes.ToArray()));
	}


	public void ClearProgress()
	{
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
