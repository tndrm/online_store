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
	[SerializeField] List<string> productTypes;

	public List<Product> openedProductList;

	private ShelvePlaceholder currentPlaceholder;
	private bool isEmployerHired = false;

	private Product nextItemToBuy;
	private List<Shelve> shelves;
	private List<Product> upgradeProductList;
	private EmployerPlaceholder employerPlaceholder;
	private int employerCost;


	public event EventHandler<List<Product>> OnProductListChange;

	private void Start()
	{
		openedProductList = new List<Product>();
		upgradeProductList = new List<Product>();

		shelves = new List<Shelve>();
		foreach (Product product in allProductsList)
		{
			if (productTypes.Any(t => product.GetProductType == t))
			{
				AddNextShelve(product);
			}
			else
			{
				upgradeProductList.Add(product);
			}
		}
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
			currentPlaceholder.OnShelveBuy += onBuyingShelve;
		}
	}

	public void ShowEmployerPlaceholder()
	{

		employerPlaceholder = Instantiate(ePlaceholderPrefab, employerPosition.position, Quaternion.identity);
		employerPlaceholder.SetCost(employerCost);
		employerPlaceholder.OnEmployerHire += HireEmployer;
	}

	private void onBuyingShelve(object sender, Product product)
	{
		upgradeProductList.Remove(product);
		nextItemToBuy = null;
		AddNextShelve(product);
		ShowShelvePlaceholder();
	}

	private void HireEmployer(object sender, GameObject product)
	{
		Instantiate(employerPrefab);
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

/*
 * TODO
 * 
 * СОХРАНЕНИЕ
 * - сохранение открытых полок
 * - сохранение нанятого сотрудника
 * - сохранение данных плейсхолдера
 * 
 * 
 * ТРЕБОВАНИЯ К СЛЕДУЮЩЕМУ АПДЕЙТУ
 * - сотрудник может выкинуть
 * - размер плейсхолдера под размер панели
 * - fix адский спаун предметов на полках
 * 
 * ДОПОЛНИТЕЛЬНО
 * - настройка бабок типо к млн и тд
 * - fix coin partical animation
 * - fix стопка на столе упаковки
 * - fix берем предмет не подходящий по заказ -> выкидываем -> несем нужный -> бинго! выдает ошибку
 * - перекинуть спрайты в префаб
 * - разбить префаб сотрудника
 * - перенести скрипт отрисовыватель спрайтов
*/