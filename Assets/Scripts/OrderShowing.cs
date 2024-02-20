using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;


public class OrderShowing : MonoBehaviour
{
	public RectTransform spritePanel;
	public float animationDuration = 0.5f;


	private List<Product> productsSprites;


	private List<GameObject> shownSprites;

	private void Awake()
	{
		shownSprites = new List<GameObject>();
	}

	private void DrawPanel()
	{
		float height = spritePanel.rect.height;
		for (int i = 0; i < productsSprites.Count; i++)
		{
			GameObject sprite = Instantiate(productsSprites[i].GetSprite, spritePanel.transform);
			sprite.transform.localScale = Vector3.zero;

			sprite.transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);
			sprite.GetComponent<RectTransform>().sizeDelta = new Vector2(height, height);

			shownSprites.Add(sprite);
		}
	}

	public void DrawNewProductList(List<Product> products)
	{
		productsSprites = new List<Product>();
		foreach (GameObject psprite in shownSprites)
		{
			Destroy(psprite);
		}
		foreach (Product p in products)
		{
			productsSprites.Add(p);
		}
		DrawPanel();
	}

	public void RemoveFromPanel(Product product)
	{
		int index = productsSprites.FindIndex(p => p.GetProductType == product.GetProductType);
		if (index >= 0)
		{
			Destroy(shownSprites[index]);
			productsSprites.RemoveAt(index);
			shownSprites.RemoveAt(index);
		}
	}
}
