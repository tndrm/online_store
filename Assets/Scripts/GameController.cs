using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{

	[SerializeField] List<Product> allProductsList;

	[SerializeField] Shelve shelvePrefab;
	[SerializeField] GameObject employerPrefab;
	[SerializeField] Transform employerPosition;

	[SerializeField] EmployerPlaceholder ePlaceholderPrefab;

	[SerializeField] ShelvePlaceholder placeholderPrefab;

	public List<Product> openedProductList;

	private ShelvePlaceholder currentPlaceholder;
	private bool isEmployerHired = false;

	private Product nextItemToBuy;
	private List<Shelve> shelves;
	private List<Product> upgradeProductList;
	private EmployerPlaceholder employerPlaceholder;

	private ProgressSavingService progressSavingService;


	public event EventHandler<List<Product>> OnProductListChange;
	public event EventHandler OnEmployerHired;

	private void Start()
	{
		openedProductList = new List<Product>();
		upgradeProductList = new List<Product>();

		shelves = new List<Shelve>();

		progressSavingService = GetComponent<ProgressSavingService>();

		isEmployerHired = progressSavingService.IsEmloyerHired();

		ShowOpenedShelves();
		ShowShelvePlaceholder();

		if (isEmployerHired)
		{
			HireEmployer(null, null);
		}
		else
		{
			ShowEmployerPlaceholder();
		}

	}

	private void ShowOpenedShelves()
	{
		List<string> openedProductTypes = progressSavingService.GetOpenedShelves();
		if (openedProductTypes.Count > 0)
		{
			foreach (Product product in allProductsList)
			{
				if (openedProductTypes.Any(t => product.GetProductType == t))
				{
					AddNextShelve(product);
				}
				else
				{
					upgradeProductList.Add(product);
				}
			}
		}
		else
		{
			 FindFreeProducts();

		}


	}

	private void FindFreeProducts()
	{
		foreach(Product product in allProductsList)
		{
			if (product.GetBuyingCost == 0)
			{
				AddNextShelve(product);
			}
			else
			{
				upgradeProductList.Add(product);
			}
		}
	}

	public void AddNextShelve(Product product)
	{
		openedProductList.Add(product);
		SpawnShelve(product);
	}

	private void SpawnShelve(Product product)
	{
		Shelve shelve = Instantiate(shelvePrefab, product.GetShelvePosition.position, Quaternion.identity);
		shelve.SetSettings(product);
		shelves.Add(shelve);
		OnProductListChange?.Invoke(this, openedProductList);
	}

	public void ShowShelvePlaceholder()
	{
		FindNextItemToBuy();
		if (nextItemToBuy != null)
		{
			currentPlaceholder = Instantiate(placeholderPrefab, nextItemToBuy.GetShelvePosition.position, Quaternion.identity);
			currentPlaceholder.SetSettings(nextItemToBuy);
			currentPlaceholder.OnShelveBuy += OnBuyingShelve;
		}
	}

	public void ShowEmployerPlaceholder()
	{

		employerPlaceholder = Instantiate(ePlaceholderPrefab, employerPosition.position, Quaternion.identity);
		int employerCost = employerPrefab.GetComponentInChildren<EmployerController>().GetCost;
		employerPlaceholder.SetCost(employerCost);
		employerPlaceholder.OnEmployerHire += HireEmployer;
	}

	private void OnBuyingShelve(object sender, Product product)
	{
		upgradeProductList.Remove(product);
		nextItemToBuy = null;
		AddNextShelve(product);
		ShowShelvePlaceholder();
	}

	private void HireEmployer(object sender, EventArgs e)
	{
		Instantiate(employerPrefab);
		OnEmployerHired?.Invoke(this, EventArgs.Empty);
		isEmployerHired = true;
	}


	private void FindNextItemToBuy()
	{
		foreach (Product product in upgradeProductList)
		{
			if (nextItemToBuy == null || nextItemToBuy.GetBuyingCost >= product.GetBuyingCost) nextItemToBuy = product;
		}
	}
}