using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] List<TradeItem> productList;
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
 * раскладка на столе сбора заказа
    поискать готовые ассеты дл€ заказа
    сделать префаб готового заказа
    написать логику сбора заказа
    написать логику забирани€ пакета со стола упаковки
    написать логику покладани€ на стол выдачи заказов
    
    ƒ≈Ќ№√»
    написать логику банка
    добавить стоимость к каждому айтему
    написать анимацию увеличени€ денег в банке
    написать логику отдачи заказа
*/