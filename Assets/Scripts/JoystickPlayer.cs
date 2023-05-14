﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed;
    private VariableJoystick variableJoystick;
    private Rigidbody rb;
	private Transform playerModel;
	private Animator animator;

	private void Start()
    {
		variableJoystick = (VariableJoystick)FindObjectOfType(typeof(VariableJoystick));
		rb = gameObject.GetComponent<Rigidbody>();
		playerModel = gameObject.transform.GetChild(0);
		animator = playerModel.gameObject.GetComponent<Animator>();
	}
    public void FixedUpdate()
    {
		Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

		rb.velocity = direction * speed * Time.fixedDeltaTime;

		if(rb.velocity != Vector3.zero)
		{
			animator.SetBool("run", true);
			transform.rotation = Quaternion.LookRotation(rb.velocity);
		}
		else
		{
			animator.SetBool("run", false);
		}
	}
}
