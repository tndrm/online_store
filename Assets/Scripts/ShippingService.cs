using UnityEngine;

public class ShippingService : MonoBehaviour
{
	public Product readyOrder;
	[SerializeField] GameObject coinsModel;
	[SerializeField] ParticleSystem coinsAnimation;
	[SerializeField] AudioClip coinsSound;
	[SerializeField] AudioClip putSound;
	private BankController bankController;
	private AudioSource audioSource;
	private int totalAnound = 0;


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
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
				encreaceCoinsOnTable(cost);
				if (holder.isMainPlayer) audioSource.PlayOneShot(putSound);
				Destroy(order.gameObject);
			}
			if (holder.isMainPlayer && totalAnound>0) TakeCoins();
		}
	}

	private void encreaceCoinsOnTable(int cost)
	{
		totalAnound += cost;
		LeaveCoins();
	}

	private void LeaveCoins()
	{
		coinsModel.gameObject.SetActive(true);
	}


	private void TakeCoins()
	{
		bankController.Increace(totalAnound);

		ShowCoinEffect();
		totalAnound = 0;

	}

	private void ShowCoinEffect()
	{
		coinsModel.gameObject.SetActive(false);
		var mainModule = coinsAnimation.main;
		audioSource.PlayOneShot(coinsSound, totalAnound*.01f);

		mainModule.maxParticles = totalAnound;
		coinsAnimation.Play();
	}
}
