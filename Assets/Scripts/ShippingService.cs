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



	public float emissionRate = 10f; // �������� ������� ������
	//public float particleSpeed = 5f; // �������� �������� ������

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
		*//*		// ��������� ������ ������� ������
				emitTimer += Time.deltaTime;

				// ���� ������� ����� ��������� �������
				if (emitTimer >= 1f / emissionRate)
				{
					emitTimer = 0f;*//*
		Debug.Log("lalalal");
			Vector3 startPos = coinsAnimation.transform.position;
		// ������� ������� �� �������
		//ParticleSystem newParticle = Instantiate(coinsAnimation, startPos, Quaternion.identity);


		// �������� ����������� �������� ������� � ����� �
		Vector3 destPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
		float distance = Vector3.Distance(startPos, destPos);

		float particleSpeed = Mathf.Lerp(2, 4, distance / 10f); // ����������� ����� �������� � ����������� �� ������ ��������
			Vector3 direction = (destPos - startPos).normalized;
			var n = coinsAnimation.main;
			// ������������� �������� �������� �������
			n.startSpeed = particleSpeed;
			var vel = coinsAnimation.velocityOverLifetime;
			// ���������� ������� � ����� �
			vel.enabled = true;
			vel.xMultiplier = direction.x * particleSpeed;
			vel.yMultiplier = direction.y * particleSpeed;
			vel.zMultiplier = direction.z * particleSpeed;

			// ���������� ������� ����� ��������� �����, ����� �������� ������ ������
			//Destroy(coinsAnimation.gameObject, 5f); // ���������� ������� ����� 5 ������ (������������ �� ������ ����������)
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
