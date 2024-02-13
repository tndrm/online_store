using Unity.VisualScripting;
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



	public float emissionRate = 10f; // Скорость выпуска частиц
	//public float particleSpeed = 5f; // Скорость движения частиц

	private float emitTimer;
	public JoystickPlayer player;
	private void Start()
	{
		coinsAnimation.Play();

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
/*
	private void Update()
	{
		*//*		// Обновляем таймер выпуска частиц
				emitTimer += Time.deltaTime;

				// Если настало время выпустить частицу
				if (emitTimer >= 1f / emissionRate)
				{
					emitTimer = 0f;*//*
		Debug.Log("lalalal");
			Vector3 startPos = coinsAnimation.transform.position;
		// Создаем частицу из префаба
		//ParticleSystem newParticle = Instantiate(coinsAnimation, startPos, Quaternion.identity);


		// Настроим направление движения частицы к точке Б
		Vector3 destPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
		float distance = Vector3.Distance(startPos, destPos);

		float particleSpeed = Mathf.Lerp(2, 4, distance / 10f); // Разделитель может меняться в зависимости от вашего масштаба
			Vector3 direction = (destPos - startPos).normalized;
			var n = coinsAnimation.main;
			// Устанавливаем скорость движения частицы
			n.startSpeed = particleSpeed;
			var vel = coinsAnimation.velocityOverLifetime;
			// Направляем частицу к точке Б
			vel.enabled = true;
			vel.xMultiplier = direction.x * particleSpeed;
			vel.yMultiplier = direction.y * particleSpeed;
			vel.zMultiplier = direction.z * particleSpeed;

			// Уничтожаем частицу через некоторое время, чтобы избежать утечек памяти
			//Destroy(coinsAnimation.gameObject, 5f); // Уничтожить частицу через 5 секунд (настраивайте по вашему усмотрению)
		*//*}*//*
	
}
*/
	private void ShowCoinEffect()
	{
		coinsModel.gameObject.SetActive(false);
		var mainModule = coinsAnimation.main;
		mainModule.maxParticles = totalAnound;

		audioSource.PlayOneShot(coinsSound, totalAnound*.01f);

		coinsAnimation.Play();
	}
}
