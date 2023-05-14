using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<TradeItem> productList;
    public List<string> productTypeList;
    [SerializeField] Shelve shelvePrefab;

    private List<Shelve> shelves;
    private ItemLevelController levelController;

	public event EventHandler<List<TradeItem>> OnProductListChange;

	private void Start()
    {
        levelController = GetComponent<ItemLevelController>();

        productList = levelController.GetOpenedTradeItems(productTypeList);

		shelves = new List<Shelve>();
		for (int i = 0; i < productList.Count; i++)
		{
			SpawnShelve(productList[i]);
		}
	}

    private void SpawnShelve(TradeItem product) 
    {
		Shelve shelve = Instantiate(shelvePrefab, product.GetShelvePosition.position, Quaternion.identity);
		shelve.SpawnObjects(product);
		shelves.Add(shelve);
		OnProductListChange?.Invoke(this, productList);
	}

    public void AddNextShelve(TradeItem product)
    {
		productList.Add(product);
        SpawnShelve(product);
	}
}

/*
 * TODO
 * 
 * АПГРЕЙД МАГАЗА
 * + создать окно для найма
 * + найти иконки для нового продукта и для нового сотрудника
 * + создать ивент в банке
 * + создать скрипт вызова меню
 * 
 * НАЙМ СОТРУДНИКА
 * - Создать префаб нового сотрудника
 * - Написать скрипт для стола сотрудника
 * - Написать скрипт сбора заказа
 * - Написать скрипт найма
 * 
 * УСТАНОВКА НОВОЙ ПОЛКИ
 * + Написать скрипт для установки
 * + добавить еще три префаба
 * 
 * МЕНЮ 
 * + сверстать меню
 * + найти иконки звука и настроек и мусорки 
 * + добавить кнопки 
 * - написать скрипт для меню
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
 * 
 * СОХРАНЕНИЕ
 * - сохранение открытых полок
 * - сохранение нанятого сотрудника
 * - очистка памяти
 * 
 * ДОПОЛНИТЕЛЬНО
 * - настройка бабок типо к млн и тд
 * - fix стопка на столе упаковки
 * - fix берем предмет не подходящий по заказ -> выкидываем -> несем нужный -> бинго! выдает ошибку
*/