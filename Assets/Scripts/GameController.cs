using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Product> productList;
    public List<string> productTypeList;
    [SerializeField] Shelve shelvePrefab;

    private List<Shelve> shelves;
    private LevelController levelController;

	public event EventHandler<List<Product>> OnProductListChange;

	private void Start()
    {
        levelController = GetComponent<LevelController>();

        productList = levelController.GetOpenedTradeItems(productTypeList);

		shelves = new List<Shelve>();
		for (int i = 0; i < productList.Count; i++)
		{
			SpawnShelve(productList[i]);
		}
	}

    private void SpawnShelve(Product product) 
    {
		Shelve shelve = Instantiate(shelvePrefab, product.GetShelvePosition.position, Quaternion.identity);
		shelve.SpawnObjects(product);
		shelves.Add(shelve);
		OnProductListChange?.Invoke(this, productList);
	}

    public void AddNextShelve(Product product)
    {
		productList.Add(product);
        SpawnShelve(product);
	}
}

/*
 * TODO
 * 
 * 
 * МУЗЫКА
 * - найти звуки для: 
 *      берет продукт
 *      кладет продукт
 *      упаковывает продукт
 *      выкидывает продукт
 *      топает
 *      летящих монет
 * - настроить звуки для всех событий
 * - добавить все в скрипты
 * - настроить отображение кнопки
 * 
 * СОХРАНЕНИЕ
 * - сохранение открытых полок
 * - сохранение нанятого сотрудника
 * 
 * ДОПОЛНИТЕЛЬНО
 * - настройка бабок типо к млн и тд
 * - добавление бабок только если игрок подходит
 * - fix стопка на столе упаковки
 * - fix сотрудник берет не тот заказ
 * - сотрудник может выкинуть
 * - fix берем предмет не подходящий по заказ -> выкидываем -> несем нужный -> бинго! выдает ошибку
 * - перекинуть спрайты в префаб
 * - разбить префаб сотрудника
*/