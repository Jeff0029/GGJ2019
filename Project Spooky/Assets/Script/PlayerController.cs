using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Location))]
public class PlayerController : MonoBehaviour 
{
    private const float MOVEMENT_SPEED = 1.0f;
    private const float MAX_MOVEMENT_SPEED = 12.0f;
    private const float QUICK_DRAG_SLOWDOWN = 0.5f;

    private Rigidbody2D m_RigidBody;

    private BoxCollider2D m_SpoopyRangeCollider;
    public BoxCollider2D m_HeadCollider;
    public BoxCollider2D m_BodyCollider;
    public BoxCollider2D m_EyesCollider;

    private SpriteRenderer m_Renderer;

    private GameObject m_CurrentGameObjectPossessing = null;

    private bool m_bIsUsingPossessionButton = false;
    private bool m_bIsPossessing = false;
    private bool m_bIsExitingPossessionButton = false;

    private bool m_bCanUseSpook = false;
    private bool m_bUsingSpook = false;

    private float m_InputWaitTimer = 0.25f;
    private const float INPUT_WAIT_MAX = 0.25f;

    private Location m_location;
    // Use this for initialization
    void Start () 
    {
        m_RigidBody = this.GetComponent<Rigidbody2D>();
        m_SpoopyRangeCollider = this.GetComponent<BoxCollider2D>();
        m_Renderer = this.GetComponent<SpriteRenderer>();
        m_location = this.GetComponent<Location>();

    }
	
	// Update is called once per frame
	void Update () 
    {
		if (m_bIsPossessing)
        {
            if (m_InputWaitTimer >= 0.0f)
            {
                m_InputWaitTimer -= Time.deltaTime;
            }
        }
	}

	private void FixedUpdate()
    {
        GetPlayerInput();
        CapMovementSpeed();
	}

    private void GetPlayerInput()
    {
        /*
         * WASD / Arrows to move
         * Space bar to go into item
         * Space bar / left click to use the item when you're in it
         * E / Right click to exit
         */

        if (!m_bIsPossessing)
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
        }

        if (m_bIsPossessing == true && m_bIsExitingPossessionButton == true)
        {
            //exit possession function
            if (m_CurrentGameObjectPossessing != null)
            {
                DeactivatePossession();
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (m_bCanUseSpook == true)
            {
                ActivateSpook();
            }
            if (!m_bIsPossessing)
            {
                m_bIsUsingPossessionButton = true;
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            m_bIsUsingPossessionButton = false;
        }

        else if (Input.GetKey(KeyCode.E))
        {
            if (m_bIsPossessing)
            {
                m_bUsingSpook = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_bIsPossessing)
            {
                m_bIsExitingPossessionButton = true;
            }
        }

        else if (Input.GetKey(KeyCode.E))
        {
            if (m_bIsPossessing)
            {
                m_bIsExitingPossessionButton = false;
            }
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
        /*
        if (collision.tag == "Room")
        {

            m_location.CurrentRoom = collision.gameObject;

            if (collision.IsTouching(m_EyesCollider))
            {
                collision.gameObject.GetComponent<RoomBehaviour>().SetRoomVisible(false);
            }
        }
        */
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.tag == "Room")
        {
            if (!collision.IsTouching(m_EyesCollider))
            {
                collision.gameObject.GetComponent<RoomBehaviour>().SetRoomVisible(true);
            }
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.IsTouching(m_SpoopyRangeCollider) && collision.gameObject.tag == "PossessableObject")
        {
            if (m_bIsPossessing == true && m_InputWaitTimer <= 0.0f)
            {
                m_bCanUseSpook = true;
            }

            else if (m_bIsUsingPossessionButton == true && m_bIsPossessing == false)
            {
                ActivatePossession(collision.gameObject);
            }
        }

        if (collision.tag == "Room")
        {
            m_location.CurrentRoom = collision.gameObject;

            if (collision.IsTouching(m_EyesCollider))
            {
                collision.gameObject.GetComponent<RoomBehaviour>().SetRoomVisible(false);
            }
        }
    }
    private void ActivatePossession(GameObject targetObject)
    {
        m_CurrentGameObjectPossessing = targetObject;

        m_CurrentGameObjectPossessing.GetComponent<PossessableObject>().OnPossessionEnter();

        //call animatiom functions
        //call the targetObject's functions
        Debug.Log("Activating Possession");
        m_bIsPossessing = true;

        this.transform.position.Set(m_CurrentGameObjectPossessing.transform.position.x,
                                    m_CurrentGameObjectPossessing.transform.position.y,
                                    m_CurrentGameObjectPossessing.transform.position.z);

        //Do this in animation functions
        m_Renderer.enabled = false;
    }

    private void DeactivatePossession()
    {
        //call animation functions

        //call the targetObject's functions
        m_CurrentGameObjectPossessing.GetComponent<PossessableObject>().OnPossessionExit();

        m_bIsPossessing = false;
        m_bIsExitingPossessionButton = false;
        m_bCanUseSpook = false;

        //Maybe take this out? Also, it needs to update from that frame of the gameobject so it is being weird.
        this.transform.position.Set(m_CurrentGameObjectPossessing.transform.position.x,
                                    m_CurrentGameObjectPossessing.transform.position.y,
                                    m_CurrentGameObjectPossessing.transform.position.z);

        //Do this in animation functions
        m_Renderer.enabled = true;

        m_CurrentGameObjectPossessing = null;

        m_InputWaitTimer = INPUT_WAIT_MAX;
    }

    private void ActivateSpook()
    {
        m_CurrentGameObjectPossessing.GetComponent<PossessableObject>().ActivateSpook();
        m_bCanUseSpook = false;
    }
}
