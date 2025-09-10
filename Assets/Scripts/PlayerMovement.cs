using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private Rigidbody2D rb;

	private Vector2 moveDir;
	private Vector2 lastDir;

	[SerializeField] private float dashForce;
	[SerializeField] private float dashTime;
	[SerializeField] private float dashCooldown;
	private bool isDashing;
	private bool canDash = true;
	private bool isWalking;

	[SerializeField] private Animator anim;
	[SerializeField] private SpriteRenderer sr;

	void Update()
	{
		if (!isDashing)
		{
			ProcessInputs();
		}

		if (Input.GetKeyDown(KeyCode.Space) && canDash && moveDir != Vector2.zero)
		{
			StartCoroutine(Dash());
		}
	}

	void FixedUpdate()
	{
		if (!isDashing)
		{
			Move();
		}
	}

	void ProcessInputs()
	{
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");

		moveDir = new Vector2(moveX, moveY).normalized;

		if (moveDir != Vector2.zero)
		{
			lastDir = moveDir;
		}
		else
		{
			isWalking = false;
			anim.SetBool("isWalking", isWalking);

			anim.SetFloat("LastInputX", lastDir.x);
			anim.SetFloat("LastInputY", lastDir.y);
		}
	}

	void Move()
	{
		rb.velocity = moveDir * moveSpeed;
		isWalking = true;
		anim.SetBool("isWalking", isWalking);

		anim.SetFloat("InputX", moveDir.x);
		anim.SetFloat("InputY", moveDir.y);
	}

	IEnumerator Dash()
	{
		isDashing = true;
		canDash = false;

		rb.velocity = moveDir * dashForce;

		yield return new WaitForSeconds(dashTime);

		isDashing = false;

		yield return new WaitForSeconds(dashCooldown);
		canDash = true;
	}
}
