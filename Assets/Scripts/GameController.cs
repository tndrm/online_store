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
 * ������� ������
 * + ������� ���� ��� �����
 * + ����� ������ ��� ������ �������� � ��� ������ ����������
 * + ������� ����� � �����
 * + ������� ������ ������ ����
 * 
 * ���� ����������
 * + ������� ������ ������ ����������
 * + �������� ������ ��� ����� ����������
 * + �������� ������ ����� ������
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
 * - fix ��������� ����� �� ��� �����
 * - ��������� ����� ��������
 * - fix ����� ������� �� ���������� �� ����� -> ���������� -> ����� ������ -> �����! ������ ������
 * - ���������� ������� � ������
 * 
*/