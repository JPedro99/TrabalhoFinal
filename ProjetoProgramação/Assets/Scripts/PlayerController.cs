using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
	public float Speed = 0.0f;
	public float JumpForce = 0.0f;

	public Transform FeetRectangleTopLeft;
	public Transform FeetRectangleBottomRight;
	public LayerMask GroundLayer;

	private bool isDead = false;

	private AudioSource audio;
	public AudioClip Jump;
	public AudioClip FootSteps;
	public AudioClip morte;

	private Rigidbody2D selfRigidbody;

	[SerializeField]
	public Animator animator;

	private float horizontalMovement = 0.0f;
	private bool isGrounded = false;
	private bool canJump = false;

	void Start()
	{
		selfRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}

	void Update()
	{

		if (!isDead)
		{
			{
				horizontalMovement = Input.GetAxis("Horizontal");


				animator.SetFloat("Velocidade", Mathf.Abs(horizontalMovement));

				if ((horizontalMovement > 0.1f && transform.localScale.x < 0) ||
					(horizontalMovement < -0.1f && transform.localScale.x > 0))
				{
					transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				}

				if (animator.GetBool("Jump") && selfRigidbody.velocity.y < 0)
				{
					animator.SetBool("Jump", false);
					animator.SetBool("Fall", true);
				}

				if (animator.GetBool("Fall") && isGrounded)
				{
					animator.SetBool("Fall", false);
				}

				if (Input.GetAxis("Jump") > 0.1f && isGrounded && !animator.GetBool("Jump"))
				{
					canJump = true;
					animator.SetBool("Jump", true);
					audio.PlayOneShot(Jump);
				}

			}

			{
				transform.localScale = new Vector3(transform.localScale.x * 1, transform.localScale.y, transform.localScale.z);
			}
		}
	}

	void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapArea(FeetRectangleTopLeft.position, FeetRectangleBottomRight.position, GroundLayer);

		if (canJump)
		{
			canJump = false;
			isGrounded = false;
			selfRigidbody.AddForce(new Vector2(0, 1) * JumpForce, ForceMode2D.Impulse);
		}

		selfRigidbody.velocity = new Vector2(Speed * horizontalMovement, selfRigidbody.velocity.y);

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Death"))
		{
			isDead = true;

			Collider2D[] list = GetComponents<Collider2D>();

			foreach (Collider2D co in list)
			{
				col.enabled = false;
			}

			horizontalMovement = 0;

			audio.PlayOneShot(morte);
			animator.SetTrigger("Death");
		}

		if (col.CompareTag("EndGame"))
		{
			SceneManager.LoadScene("Menu");
		}
	}

	public void Restart()
		{
			SceneManager.LoadScene("SampleScene");
		}
}