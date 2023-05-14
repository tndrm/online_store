using UnityEngine;
using UnityEngine.AI;

public class EmployerController : MonoBehaviour
{
	[SerializeField] PackingTable packingTable;
	[SerializeField] ShippingService shippingTable;
	private ProductHolder productHolder;
	private NavMeshAgent navMeshAgent;
	private TradeItem nextNededProduct;
	private Vector3 destinationPoint;
	void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		productHolder = GetComponent<ProductHolder>();
	}

	void Update()
	{
		/*		if (!levelController.isGamePaused)
				{
					playerPosition = player.transform.position;

					transform.LookAt(player.transform);

					navMeshAgent.isStopped = !navMeshAgent.Raycast(playerPosition, out hit);
				}*/
		Debug.Log(navMeshAgent.velocity.magnitude);
		float velocity = navMeshAgent.velocity.magnitude;
			if (destinationPoint == Vector3.zero || navMeshAgent.remainingDistance<1 || velocity < 1)
		{
			SetDestinationPoint();
		}
		navMeshAgent.destination = destinationPoint;



	}

	private void SetDestinationPoint()
	{
		TradeItem holdedProduct = productHolder.holdedItem;
		if (holdedProduct == null)
		{
			nextNededProduct = packingTable.GetNextNededItem();
			destinationPoint = nextNededProduct.GetShelvePosition.position;
		}
		else if (holdedProduct != null)
		{
			destinationPoint = holdedProduct.GetItemType != "packedOrder" ? packingTable.transform.position : shippingTable.transform.position;
		}
	}
}
