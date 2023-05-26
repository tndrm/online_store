using System.Collections.Generic;
using UnityEngine;

public class OrderShowing : MonoBehaviour
{
	public RectTransform spritePanel;
	private float distanceBetweenSprites;

	private List<GameObject> productsSprites;

	private List<GameObject> shownSprites;

	private void Awake()
	{
		shownSprites = new List<GameObject>();
	}

	private void DrawPanel()
	{

		float height = spritePanel.rect.height;
		Vector2 spritesize = new Vector2(height, height);

		float minXPanelPoint = GetMinXPosition();
		distanceBetweenSprites = spritesize.x / 4;

		float nextXPosition = minXPanelPoint + spritesize.x / 2; ;

		for (int i = 0; i < productsSprites.Count; i++)
		{
			Vector3 currentSpritePosition = new Vector3(nextXPosition, 0, 0);
			GameObject sprite = Instantiate(productsSprites[i], currentSpritePosition, spritePanel.transform.rotation, spritePanel.transform);
			RectTransform theBarRectTransform = sprite.transform as RectTransform;
			theBarRectTransform.sizeDelta = spritesize;
			theBarRectTransform.localPosition = currentSpritePosition;

			shownSprites.Add(sprite);
			nextXPosition = currentSpritePosition.x + spritesize.x + distanceBetweenSprites;
		}
	}

	private float GetMinXPosition()
	{
		Vector3[] corners = new Vector3[4];
		spritePanel.GetLocalCorners(corners);

		float minX = corners[0].x;

		for (int i = 1; i < corners.Length; i++)
		{
			if (corners[i].x < minX)
			{
				minX = corners[i].x;
			}
		}
		return minX;
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
