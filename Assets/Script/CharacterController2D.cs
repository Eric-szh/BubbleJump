using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RangeAttribute = UnityEngine.RangeAttribute;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] public int speed = 10;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    private List<GameObject> foamList = new List<GameObject>();
	private List<GameObject> oilList = new List<GameObject>();
	private float poisonTimer = 0;
	public float poisonTime = 0.5f;

	public int health = 3;

    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

    private void Update()
    {
        if (oilList.Count > 0)
		{
            poisonTimer += Time.deltaTime;
			if (poisonTimer >= poisonTime)
			{
				Damage(1);
                poisonTimer = 0;
			}

        }
        else
		{
            poisonTimer = 0;
        }
    }

	public void Damage(int damage)
	{
		health -= damage;
		Debug.Log("Health: " + health);
		if (health <= 0)
		{
            Debug.Log("Game Over");
        }
	}

    public void Move(float move, bool jump)
	{


		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * speed, m_Rigidbody2D.linearVelocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.linearVelocity = Vector3.SmoothDamp(m_Rigidbody2D.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}

		//Check if the player is in contact with foam, only if the player is grounded
		if (m_Grounded)
		{
            if (foamList.Count > 0)
			{
				speed = 15;
                m_MovementSmoothing = 0.3f;
            } else
			{
				speed = 8;
                m_MovementSmoothing = 0.00f;
			}
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


	public void ContactFoam(GameObject gameobject)
	{
		foamList.Add(gameobject);
	}

	public void DeContactFoam(GameObject gameobject)
	{
        foamList.Remove(gameobject);
    }

	public void ContactOil(GameObject gameobject)
	{
		oilList.Add(gameobject);
	}

	public void DeContactOil(GameObject gameobject)
	{
        oilList.Remove(gameobject);
    }
}
