using UnityEngine;
using UnityEngine.AI;

public class EmployerController : MonoBehaviour
{
	[SerializeField] PackingTable packingTable;
	[SerializeField] int employerCost;
	private ShippingService shippingTable;
	private ProductHolder productHolder;
	private NavMeshAgent navMeshAgent;
	private Product nextNededProduct;
	private Vector3 destinationPoint;
	private string readyOrderType;

	public int GetCost => employerCost;

	void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		productHolder = GetComponent<ProductHolder>();
		shippingTable = (ShippingService)FindObjectOfType(typeof(ShippingService));
		readyOrderType = shippingTable.readyOrder.GetProductType;
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

	public bool IsNeedToTake(Product product)
	{
		bool isNeed = nextNededProduct && product.GetProductType == nextNededProduct.GetProductType;
		bool isReady = product.GetProductType == readyOrderType;
		return isNeed || isReady;
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
			destinationPoint = holdedProduct.GetProductType != readyOrderType ? packingTable.transform.position : shippingTable.transform.position;
		}
	}
}
