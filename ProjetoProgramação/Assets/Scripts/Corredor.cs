using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Corredor : MonoBehaviour
{
	[SerializeField]
	public float Speed = 0.0f;

	private Animator animator;
	private Rigidbody2D RB;
	private int currentDirection = -1;
	void Start()
	{
		RB = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		RB.velocity = new Vector2(currentDirection * Speed, RB.velocity.y);
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Ground"))
		{
			currentDirection *= -1;
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}

}