using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using UnityEngine.Serialization;

public class CharacterController2d : MonoBehaviour
{
	
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .0f;	// How much to smooth out the movement
	[Range(0, .3f)] [SerializeField] private float m_AirMovementSmoothing = .2f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	[SerializeField] private float m_PotionFillStatus = 0.0f;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	public Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void Start() {
		originalMaterial = GetComponent<SpriteRenderer>().material;
		// MakeInvincible(10f);
	}

	private void FixedUpdate() {
		invincibilityDuration = Math.Max(0f, invincibilityDuration - Time.fixedDeltaTime);
	}

	public void Move(float move, bool crouch)
	{
		if (Math.Abs(move) > 0.1f && m_Grounded)
		{
			GetComponent<Animator>().enabled = true;
		}
		else
		{
			GetComponent<Animator>().enabled = false;
		}
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_Grounded ? m_MovementSmoothing : m_AirMovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
	}

	public bool IsGrounded
	{
		get {
			return m_Grounded;
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void setPotionFillStatus(float potionFillStatus) {
		this.m_PotionFillStatus = potionFillStatus;
	}

	public float getPotionFillStatus() {
		return m_PotionFillStatus;
	}

	private void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.layer == 7 || other.gameObject.layer == 0) {
			m_Grounded = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == 7 || collision.gameObject.layer == 0) {
			m_Grounded = true;
		}
	}

	public float invincibilityDuration;
	[SerializeField] private Material flashMaterial;
	private Material originalMaterial;

	public void MakeInvincible(float duration) {
		// Debug.Log("" + duration + " " + invincibilityDuration);
		var startFlash = invincibilityDuration <= 0f;
		invincibilityDuration += duration;
		if (duration > 0f && startFlash) {
			// Debug.Log("" + duration + " " + invincibilityDuration);
			StartCoroutine(FlashWhiteWhileInvincible());
		}
	}

	private IEnumerator FlashWhiteWhileInvincible() {
		var sr = GetComponent<SpriteRenderer>();
		while (invincibilityDuration > 0) {
			sr.material = flashMaterial;
			yield return new WaitForSeconds(1f / 5f);
			sr.material = originalMaterial;
			yield return new WaitForSeconds(1f / 3f);
		}
	}
}
