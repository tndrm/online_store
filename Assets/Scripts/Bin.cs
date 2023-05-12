using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		PlayerItemHolder holder = other.gameObject.GetComponent<PlayerItemHolder>();
		if (holder)
		{
			holder.DiscardHoldedItem();
		}
	}
}
