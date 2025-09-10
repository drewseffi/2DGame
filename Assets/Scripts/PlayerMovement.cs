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

	[SerializeField] private float dashForce;
	[SerializeField] private float dashTime;
	[SerializeField] private float dashCooldown;
	private bool isDashing;
	private bool canDash = true;

	[SerializeField] private Animator anim;
	[SerializeField] private SpriteRenderer sr;

	private bool horiz;

	void Update()
	{
		if (!isDashing)
			ProcessInputs();

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


		// Horizontal movement
		switch (moveDir.x)
		{
			// Moving right
			case > 0:
				anim.SetBool("RunHor", true);
				sr.flipX = false;
				horiz = true;
				break;
			// Moving left
			case < 0:
				anim.SetBool("RunHor", true);
				sr.flipX = true;
				horiz = true;
				break;
			// Stopped
			case 0:
				anim.SetBool("RunHor", false);
				sr.flipX = false;
				horiz = false;
				break;
		}

		// Vertical movement
		if (!horiz)
		{
			switch (moveDir.y)
			{
				case > 0:
					anim.SetBool("RunUp", true);
					anim.SetBool("RunDown", false);
					break;
				case < 0:
					anim.SetBool("RunDown", true);
					anim.SetBool("RunUp", false);
					break;
				case 0:
					anim.SetBool("RunUp", false);
					anim.SetBool("RunDown", false);
					break;
			}
		}
	}

	void Move()
	{
		rb.velocity = moveDir * moveSpeed;
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
