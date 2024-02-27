using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    private Rigidbody rb;

	private void Start()
    {
		variableJoystick = (VariableJoystick)FindObjectOfType(typeof(VariableJoystick));
		rb = this.GetComponent<Rigidbody>();
	}
	public void FixedUpdate()
    {
		Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
		rb.velocity = direction * speed * Time.fixedDeltaTime;
		if(rb.velocity != Vector3.zero) rb.MoveRotation(Quaternion.LookRotation(rb.velocity));

	}
}
