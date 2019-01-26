﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessableObject : MonoBehaviour 
{
    protected Rigidbody2D m_RigidBody;
    protected bool m_bIsOnCooldown = false;
    protected float m_CooldownTimer = 3.0f;
    protected float m_CurrentCDTimer;

	// Use this for initialization
	void Start () 
    {
        m_RigidBody = this.GetComponent<Rigidbody2D>();

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
            float randomY = Random.Range(2.0f, 8.0f);
            float randomX = Random.Range(-5.0f, 5.0f);

            m_RigidBody.AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);

            m_bIsOnCooldown = true;
        }
    }
}