using UnityEngine;

public class Bin : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		ProductHolder holder = other.gameObject.GetComponent<ProductHolder>();
		if (holder)
		{
			if(holder.isMainPlayer) GetComponent<AudioSource>().Play();
			holder.DiscardHoldedItem();
		}
	}
}
