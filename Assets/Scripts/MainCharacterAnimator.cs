using UnityEngine;

public class MainCharacterAnimator : MonoBehaviour
{
    private Rigidbody rb;
	private Animator animator;

	private void Start()
    {
		rb = GetComponent<Rigidbody>();
		Transform playerModel = transform.GetChild(0);
		animator = playerModel.gameObject.GetComponent<Animator>();
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
}
