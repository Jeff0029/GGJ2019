using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private const float MOVEMENT_SPEED = 1.0f;
    private const float MAX_MOVEMENT_SPEED = 3.0f;
    private const float QUICK_DRAG_SLOWDOWN = 0.5f;

    private Rigidbody2D m_RigidBody;
    private CircleCollider2D m_SpoopyRangeCollider;
    public BoxCollider2D m_HeadCollider;
    public BoxCollider2D m_BodyCollider;

    private bool m_bIsUsingPossessionButton = false;
    private bool m_bIsPossessing = false;


	// Use this for initialization
	void Start () 
    {
        m_RigidBody = this.GetComponent<Rigidbody2D>();
        m_SpoopyRangeCollider = this.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

	private void FixedUpdate()
	{
        GetPlayerInput();
        CapMovementSpeed();
	}

    private void GetPlayerInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            m_RigidBody.AddForce(new Vector2(0.0f, MOVEMENT_SPEED), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            m_RigidBody.AddForce(new Vector2(-MOVEMENT_SPEED, 0.0f), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            m_RigidBody.AddForce(new Vector2(0.0f, -MOVEMENT_SPEED), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            m_RigidBody.AddForce(new Vector2(MOVEMENT_SPEED, 0.0f), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            m_bIsUsingPossessionButton = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && Input.GetMouseButtonUp(0))
        {
            m_bIsUsingPossessionButton = false;
        }
    }

    private void CapMovementSpeed()
    {
        float amountOver = 0.0f;

        //right
        if (m_RigidBody.velocity.x > MAX_MOVEMENT_SPEED)
        {
            amountOver = m_RigidBody.velocity.x - MAX_MOVEMENT_SPEED;
            m_RigidBody.AddForce(new Vector2(-amountOver, 0.0f), ForceMode2D.Impulse);
        }

        //left
        if (m_RigidBody.velocity.x < -MAX_MOVEMENT_SPEED)
        {
            amountOver = m_RigidBody.velocity.x + MAX_MOVEMENT_SPEED;
            m_RigidBody.AddForce(new Vector2(amountOver * -1, 0.0f), ForceMode2D.Impulse);
        }

        //up
        if (m_RigidBody.velocity.y > MAX_MOVEMENT_SPEED)
        {
            amountOver = m_RigidBody.velocity.y - MAX_MOVEMENT_SPEED;
            m_RigidBody.AddForce(new Vector2(0.0f, -amountOver), ForceMode2D.Impulse);
        }

        //down
        if (m_RigidBody.velocity.y < -MAX_MOVEMENT_SPEED)
        {
            amountOver = m_RigidBody.velocity.y + MAX_MOVEMENT_SPEED;
            m_RigidBody.AddForce(new Vector2(0.0f, amountOver * -1), ForceMode2D.Impulse);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.IsTouching(m_SpoopyRangeCollider) && collision.gameObject.tag == "PossessableObject")
        {
            if (m_bIsUsingPossessionButton == true)
            {
                collision.gameObject.GetComponent<PossessableObject>().ActivateSpook();
            }
        }
	}
}
