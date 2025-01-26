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
	private int original_speed;

	public bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    private List<GameObject> foamList = new List<GameObject>();
	private List<GameObject> oilList = new List<GameObject>();
	private float poisonTimer = 0;
	public float poisonTime = 0.5f;

	private bool isJumping = false;
	private GameObject twoWayPlat;

	public int health = 3;

	public GameObject face;
	public GameObject belly;
	public GameObject tail;
	public GameObject bodyDetect;

	public Vector3 megaDropPoint;
	public int megaDropDistance = 6;
    public bool isMegaDrop;
	private bool wasMegaDrop;
	private float orignialGravity;
	public float megaDropGravity;
	
	private bool megaJumpReady = false;
	public float megaJumpWindow = 0.5f;
	public float megaJumpStrength = 1000;

	public bool isDead = false;
	public Vector3 respawnPoint;
	public float respawnTime;

	public bool isCharging = false;
	public Vector3 chargePoint;
	public bool canCharge = false;
	public GameObject chargeEffect;
	public GameObject chargeStation;

    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		isMegaDrop = false;
		orignialGravity = m_Rigidbody2D.gravityScale;
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		original_speed = speed;
	}

	private void FixedUpdate()
	{
		return;
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
		if (GetComponent<CharacterSkillControler>().skill3Activated)
		{
			GetComponent<CharacterSkillControler>().Skill3Blocked();
            return;
        } else
		{
			health -= damage;
			Debug.Log("Health: " + health);
			if (health <= 0)
			{
				isDead = true;
				GetComponent<AniController>().ChangeAnimationState("Player_dies");
				HideFace();
			}
		}
		
	}

	public void Respawn()
	{
		health = 3;
		transform.position = respawnPoint;
		ShowFace();
		Invoke("RespawnFinished", respawnTime);
		face.GetComponent<FaceCtrl>().FastBlink();
        GetComponent<AniController>().ChangeAnimationState("Player_idle");
		GameStateManager.Instance.Load();
    }

	private void RespawnFinished()
	{
		isDead = false;
		face.GetComponent<FaceCtrl>().ResetBlink();
	}

    public void Move(float move, bool jump, bool down)
	{
		if (isDead || isCharging)
		{
            return;
        }

		bool insidePlat = bodyDetect.GetComponent<BodyDetect>().insidePlat;

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
		if (m_Grounded && jump &&!insidePlat)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			if (megaJumpReady)
			{
                m_Rigidbody2D.AddForce(new Vector2(0f, megaJumpStrength));
                megaJumpReady = false;
				CancelInvoke("LoseMegaJump");
				transform.localScale = new Vector3(1, 1, 1);
            }
			else
			{
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
            isJumping = true;
			HideFace();
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
				speed = original_speed;
                m_MovementSmoothing = 0.00f;
			}
        }

		if (down || (insidePlat && GetComponent<Rigidbody2D>().linearVelocityY <= 0))
		{
			if (twoWayPlat != null)
			{
                twoWayPlat.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
            }
        }
        else
		{
            if (twoWayPlat != null)
			{
                twoWayPlat.GetComponent<PlatformEffector2D>().rotationalOffset = 0;
            }
		}

		// Check if the player is able to mega drop, if so, set the gravity to mega drop gravity
		wasMegaDrop = isMegaDrop;
		if (down && GetComponent<CharacterSkillControler>().skill3Activated && GetComponent<CharacterSkillControler>().skill3canMegaDrop && !m_Grounded)
		{
            isMegaDrop = true;
			GetComponent<Rigidbody2D>().gravityScale = megaDropGravity;

            if (!wasMegaDrop)
			{
                megaDropPoint = transform.position;
            }
        }

		// animation stuff
		if (isJumping)
		{
            switch (health)
			{
				case 3:
                    GetComponent<AniController>().ChangeAnimationState("Player_jump3");
                    break;
				case 2:
					GetComponent<AniController>().ChangeAnimationState("Player_jump2");
                    break;
				case 1:
					GetComponent<AniController>().ChangeAnimationState("Player_jump1");
                    break;
			}
        }
        else
		{
            GetComponent<AniController>().ChangeAnimationState("Player_idle");
        }
	}

	public void Charge()
	{
        if (canCharge)
		{
            isCharging = true;
            transform.position = chargePoint;
			tail.GetComponent<SpriteRenderer>().enabled = false;
			face.GetComponent<SpriteRenderer>().enabled = false;
			chargeEffect.SetActive(true);
			chargeStation.GetComponent<ChargeCtrl>().startCharge();
			GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
			respawnPoint = chargePoint;
			GameStateManager.Instance.Save();
        }
    }

	public void ChargeRelease()
	{
        isCharging = false;
		tail.GetComponent<SpriteRenderer>().enabled = true;
		face.GetComponent<SpriteRenderer>().enabled = true;
		chargeEffect.SetActive(false);
		chargeStation.GetComponent<ChargeCtrl>().finishCharge();
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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

	public void Land()
	{
		isJumping = false;
		ShowFace();
		// if player is mega dropping, check if the player has fallen enough distance to trigger mega jump
		if (isMegaDrop)
		{
			isMegaDrop = false;
			GetComponent<Rigidbody2D>().gravityScale = orignialGravity;
            float fallingDistance = megaDropPoint.y - transform.position.y;
            Debug.Log("Falling Distance: " + fallingDistance);
			if (fallingDistance >= megaDropDistance)
			{
                // used up the mega drop ability so that the player can't use it again until it expires
                GetComponent<CharacterSkillControler>().skill3canMegaDrop = false;
                Debug.Log("Mega Drop");
				megaJumpReady = true;
				transform.localScale = new Vector3(1, 0.6f, 1);
				// TODO: Deal damage to enemies
				Invoke("LoseMegaJump", megaJumpWindow);
			}
        }

	}

	private void LoseMegaJump()
	{
        megaJumpReady = false;
		transform.localScale = new Vector3(1, 1, 1);
    }

	private void HideFace()
	{
        face.GetComponent<SpriteRenderer>().enabled = false;
		belly.GetComponent<SpriteRenderer>().enabled = false;
    }

	private void ShowFace()
	{
        face.GetComponent<SpriteRenderer>().enabled = true;
		belly.GetComponent<SpriteRenderer>().enabled = true;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "TwoWayPlat")
		{
            twoWayPlat = collision.gameObject;
			// Debug.Log("TwoWayPlat");
        }
    }

	private void OnCollisionExit2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "TwoWayPlat")
		{
            twoWayPlat = null;
			// Debug.Log("TwoWayPlat");
        }
    }
}
