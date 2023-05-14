using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<TradeItem> productList;
    public List<string> productTypeList;
    [SerializeField] Shelve shelvePrefab;
    [SerializeField] List<Transform> shelvePositions;
	[SerializeField] Vector2 itemsInPackageRange = new Vector2(1, 3f);

    private List<Shelve> shelves;
    private PackingTable packingTable;
    private ItemLevelController levelController;


    private void Start()
    {
        levelController = GetComponent<ItemLevelController>();

        productList = levelController.GetOpenedTradeItems(productTypeList);

		shelves = new List<Shelve>();
		for (int i = 0; i < productList.Count; i++)
		{
			SpawnShelve(shelvePositions[i], productList[i]);
		}
		packingTable = (PackingTable)FindObjectOfType(typeof(PackingTable));
        ShowNextOrder();
	}

    private void SpawnShelve(Transform spawnPoint, TradeItem product) 
    {
		Shelve shelve = Instantiate(shelvePrefab, spawnPoint.position, Quaternion.identity);
		shelve.SpawnObjects(product);
		shelves.Add(shelve);
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
    
    public void AddNextShelve(TradeItem product)
    {
		productList.Add(product);
        SpawnShelve(shelvePositions[productList.Count], product);
	}
}

/*
 * TODO
 * 
 * ������� ������
 * + ������� ���� ��� �����
 * + ����� ������ ��� ������ �������� � ��� ������ ����������
 * + ������� ����� � �����
 * + ������� ������ ������ ����
 * 
 * ���� ����������
 * - ������� ������ ������ ����������
 * - �������� ������ ��� ����� ����������
 * - �������� ������ ����� ������
 * - �������� ������ �����
 * 
 * ��������� ����� �����
 * + �������� ������ ��� ���������
 * - �������� ��� ��� �������
 * 
 * ���� 
 * + ��������� ����
 * + ����� ������ ����� � �������� � ������� 
 * + �������� ������ 
 * - �������� ������ ��� ����
 * 
 * ������
 * - ����� ����� ���: 
 *      ����� �������
 *      ������ �������
 *      ����������� �������
 *      ���������� �������
 *      ������
 *      ������� �����
 * - ��������� ����� ��� ���� �������
 * - �������� ��� � �������
 * 
 * ����������
 * - ���������� �������� �����
 * - ���������� �������� ����������
 * - ������� ������
 * 
 * �������������
 * - ��������� ����� ���� � ��� � ��
 * - fix ������ �� ����� ��������
 * - fix ����� ������� �� ���������� �� ����� -> ���������� -> ����� ������ -> �����! ������ ������
*/