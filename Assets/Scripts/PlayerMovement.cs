using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

	private Vector2 moveDir;

	void Update()
	{
		ProcessInputs();
		Move();
	}

	void FixedUpdate()
	{
		
	}

	void ProcessInputs()
	{
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");

		moveDir = new Vector2(moveX, moveY).normalized;
	}

	void Move()
	{
		rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
	}

}
