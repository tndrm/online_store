using UnityEngine;

public class Bin : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		ProductHolder holder = other.gameObject.GetComponent<ProductHolder>();
		if (holder)
		{
			holder.DiscardHoldedItem();
		}
	}
}
