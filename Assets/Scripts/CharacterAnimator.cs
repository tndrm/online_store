using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Rigidbody rb;
	private Animator animator;
	GameController gameController;

	private void Start()
    {
		rb = GetComponent<Rigidbody>();
		Transform playerModel = transform.GetChild(0);
		animator = playerModel.gameObject.GetComponent<Animator>();
		ProductHolder productHolder = GetComponent<ProductHolder>();
		productHolder.OnProduktTaken += ChangeHoldingAnim;
		gameController = FindFirstObjectByType<GameController>();
		gameController.OnEmployerHired += ShowVinAnimation;
		gameController.OnProductListChange += ShowVinAnimation;

	}


	public void Update()
    {
		if(rb.velocity != Vector3.zero)
		{
			animator.SetBool("run", true);
		}
		else
		{
			animator.SetBool("run", false);
		}
	}
	private void ChangeHoldingAnim(object sender, bool isHold)
	{
		animator.SetBool("hold", isHold);
	}

	private void ShowVinAnimation(object sender, EventArgs e)
	{
		animator.SetTrigger("happy");
	}

	private void ShowVinAnimation(object sender, List<Product> e)
	{
		animator.SetTrigger("happy");
	}
}
