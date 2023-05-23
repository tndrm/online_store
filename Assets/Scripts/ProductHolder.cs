using UnityEngine;

public class ProductHolder : MonoBehaviour
{
	[SerializeField] Transform spawnPoint;
	public Product holdedItem;
	private HoldedItemSaver saver;
	public bool isMainPlayer = true;
	private EmployerController employerController = null;

	private void Awake()
	{
		if (TryGetComponent<EmployerController>(out EmployerController e))
		{
			employerController = e;
			isMainPlayer = false;
		}
		//saver = this.GetComponent<HoldedItemSaver>();
	}
	public Product TakeItem(Product item)
	{
		Product takenItem = null;
		bool isNeededProduct = IsNeededProduct(item);
		if (!holdedItem && isNeededProduct)
		{
			item.transform.SetParent(transform);
			item.transform.position = spawnPoint.position;
			item.transform.rotation = transform.rotation;
			holdedItem = item;
			takenItem = item;
			//saver.SaveHoldItem(item);
		}
		return takenItem;
	}

	private bool IsNeededProduct(Product item)
	{
		bool isNeed = employerController != null ? employerController.IsNeedToTake(item) : true;
		return isNeed;
	}
	public Product PutItem(Product needItem)
	{
		Product putedItem = null;
		if (holdedItem && holdedItem.GetProductType == needItem.GetProductType)
		{
			putedItem = holdedItem;
			holdedItem = null;
			//saver.SaveHoldItem();
		}
		return putedItem;
	}

	public void DiscardHoldedItem()
	{
		if (holdedItem)
		{
			Destroy(holdedItem.gameObject);
			//saver.SaveHoldItem();
		}
	}
}
