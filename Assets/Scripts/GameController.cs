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
 * + �������� ��� ��� �������
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