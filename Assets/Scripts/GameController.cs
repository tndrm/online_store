using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<TradeItem> productList;
    [SerializeField] Shelve shelvePrefab;
    [SerializeField] List<Transform> shelvePositions;
	[SerializeField] Vector2 itemsInPackageRange = new Vector2(1, 3f);
    private PackingTable packingTable;



	private void Awake()
    {
        for(int i = 0; i<productList.Count; i++)
        {
            SpawnShelve(shelvePositions[i], productList[i]);
		}
    }

    private void Start()
    {
        packingTable = (PackingTable)FindObjectOfType(typeof(PackingTable));
        ShowNextOrder();
	}

    private void SpawnShelve(Transform spawnPoint, TradeItem product) 
    {
		Shelve shelve = Instantiate(shelvePrefab, spawnPoint.position, Quaternion.identity);
		shelve.SpawnObjects(product);
	}

    public void ShowNextOrder()
    {
		List<TradeItem> nextPackage = generateNextPackage();
		packingTable.UploadNextOrder(nextPackage);
	}

	private List<TradeItem> generateNextPackage()
	{

        List<TradeItem> nextPackage = new List<TradeItem>();

        int productQuantity = (int)Random.Range(itemsInPackageRange.x, itemsInPackageRange.y);
		for (int i = 0; i < productQuantity; i++ ) {
            int item = (int)Random.Range(0, productList.Count);
			nextPackage.Add(productList[item]);
		}
        return nextPackage;
	}
    
    public void AddNextShelve()
    {

    }
}

/*
 * TODO
 * 
 * АПГРЕЙД МАГАЗА
 * + создать окно для найма
 * - создать ивент в банке
 * + найти иконки для нового продукта и для нового сотрудника
 * - создать скрипт вызова меню
 * 
 * НАЙМ СОТРУДНИКА
 * - Создать префаб нового сотрудника
 * - Написать скрипт для стола сотрудника
 * - Написать скрипт сбора заказа
 * - Написать скрипт найма
 * 
 * УСТАНОВКА НОВОЙ ПОЛКИ
 * - Написать скрипт для установки
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
*/