using System.Collections.Generic;
using UnityEngine;

public class OrderShowing : MonoBehaviour
{
	public GameObject spritePanel;
	public float distanceBetweenSprites = .3f;

	private List<GameObject> productsSprites;

	private List<GameObject> shownSprites;

	private void Awake()
	{
		shownSprites = new List<GameObject>();
	}

	private void DrawPanel()
	{
		Vector3 firstSpritePosition = spritePanel.transform.position;
		Vector3 previousPosition = firstSpritePosition;

		for (int i = 0; i < productsSprites.Count; i++)
		{
			Vector3 currentSpritePosition = new Vector3(previousPosition.x + distanceBetweenSprites, firstSpritePosition.y, firstSpritePosition.z);
			shownSprites.Add(Instantiate(productsSprites[i], currentSpritePosition, Quaternion.identity, spritePanel.transform));
			previousPosition = currentSpritePosition;
		}
	}

	public void UpdateItemsListShowing(List<Product> products)
	{
		ClearPanel();
		productsSprites = new List<GameObject>();
		foreach(Product product in products)
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
