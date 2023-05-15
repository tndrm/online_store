using UnityEngine;
using UnityEngine.AI;

public class EmployerController : MonoBehaviour
{
	[SerializeField] PackingTable packingTable;
	private ShippingService shippingTable;
	private ProductHolder productHolder;
	private NavMeshAgent navMeshAgent;
	private Product nextNededProduct;
	private Vector3 destinationPoint;
	void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		productHolder = GetComponent<ProductHolder>();
		shippingTable = (ShippingService)FindObjectOfType(typeof(ShippingService));
	}

	void Update()
	{
		float velocity = navMeshAgent.velocity.magnitude;
		if (destinationPoint == Vector3.zero || navMeshAgent.remainingDistance < 1 || velocity < 1)
		{
			SetDestinationPoint();
		}
		navMeshAgent.destination = destinationPoint;
	}

	private void SetDestinationPoint()
	{
		Product holdedProduct = productHolder.holdedItem;
		if (holdedProduct == null)
		{
			nextNededProduct = packingTable.GetNextNededItem();
			destinationPoint = nextNededProduct.GetShelvePosition.position;
		}
		else if (holdedProduct != null)
		{
			destinationPoint = holdedProduct.GetProductType != shippingTable.readyOrder.GetProductType ? packingTable.transform.position : shippingTable.transform.position;
		}
	}
}
