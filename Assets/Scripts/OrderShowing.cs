using System.Collections.Generic;
using UnityEngine;

public class OrderShowing : MonoBehaviour
{
	public RectTransform spritePanel;
	private List<GameObject> productsSprites;

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
			GameObject sprite = Instantiate(productsSprites[i], spritePanel.transform);
			sprite.GetComponent<RectTransform>().sizeDelta = new Vector2(height, height);

			shownSprites.Add(sprite);
		}
	}

	public void UpdateItemsListShowing(List<Product> products)
	{
		ClearPanel();
		productsSprites = new List<GameObject>();
		foreach (Product product in products)
		{
			productsSprites.Add(product.GetSprite);
		}
		DrawPanel();
	}

	private void ClearPanel()
	{
		foreach (GameObject psprite in shownSprites)
		{
			Destroy(psprite);
		}
	}
}
