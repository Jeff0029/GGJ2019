using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Location))]
public class PossessableObject : MonoBehaviour 
{
    public const string POSSESSED_ANIMATION = "possessed";
    public const string SPOOKED_ANIMATION = "spook";

    protected Rigidbody2D m_RigidBody;
    protected bool m_bIsOnCooldown = false;
    protected float m_CooldownTimer = 3.0f;
    protected float m_CurrentCDTimer;

    protected Animator m_Animator;
    protected bool m_IsObjectPossessed = false;

	// Use this for initialization
	void Start () 
    {
        m_RigidBody = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();

        m_CurrentCDTimer = m_CooldownTimer;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (m_bIsOnCooldown)
        {
            m_CurrentCDTimer -= Time.deltaTime;

            if (m_CurrentCDTimer <= 0)
            {
                m_CurrentCDTimer = m_CooldownTimer;
                m_bIsOnCooldown = false;
            }
        }
	}

    public virtual void ActivateSpook()
    {
        if (m_bIsOnCooldown == false)
        {
            /*
            float randomY = Random.Range(2.0f, 8.0f);
            float randomX = Random.Range(-5.0f, 5.0f);

            m_RigidBody.AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);

            m_bIsOnCooldown = true;

            ActivateSpookAnimation();
            transform.parent.GetComponent<RoomBehaviour>().SpookCivilians();*/

            m_bIsOnCooldown = true;
            ActivateSpookAnimation();
            transform.parent.GetComponent<RoomBehaviour>().SpookCivilians();
        }
    }

    //This hack is needed, don't ask
    public void ResetTimer()
    {
        m_bIsOnCooldown = true;
        m_CurrentCDTimer = 0.5f;
    }

    public virtual void ActivateSpookAnimation()
    {
        SetPossessionAnimation(false);
        m_Animator.SetTrigger(SPOOKED_ANIMATION);
    }

    public virtual void SetPossessionAnimation(bool bAnimating)
    {
        m_Animator.SetBool(POSSESSED_ANIMATION, bAnimating);
    }

    public virtual void OnPossessionEnter()
    {
        ResetTimer();
        SetPossessionAnimation(true);
    }

    public virtual void OnPossessionExit()
    {
        SetPossessionAnimation(false);
    }
}
