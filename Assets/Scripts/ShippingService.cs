using UnityEngine;

public class ShippingService : MonoBehaviour
{
	public Product readyOrder;
	[SerializeField] ParticleSystem coinsAnimation;
	private BankController bankController;

	private void Start()
	{
		bankController = (BankController)FindObjectOfType(typeof(BankController));
	}

	private void OnTriggerEnter(Collider other)
	{
		ProductHolder holder = other.gameObject.GetComponent<ProductHolder>();

		if (holder)
		{
			Product order = holder.PutItem(readyOrder);
			if (order)
			{
				int cost = order.GetCost;
				bankController.Increace(cost);
				ShowCoinAnimation(cost);
				Destroy(order.gameObject);
			}
		}
	}

	private void ShowCoinAnimation(int cost)
	{
		var mainModule = coinsAnimation.main;
		mainModule.maxParticles = cost;
		coinsAnimation.Play();
	}
}
