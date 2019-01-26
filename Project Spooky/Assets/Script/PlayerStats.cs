﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour 
{
    const float MAX_SPOOKJUICE = 100.0f;
    const float SPOOKJUICE_DECREASE_AMOUNT = 2.0f;
    const float SPOOKJUICE_DECREASE_INTERVAL = 1.0f;

    float m_CurrentSpookJuiceDecreaseTimer;
    float m_CurrentSpookJuice;

    PlayerController m_PlayerController;
    PlayerUI m_PlayerUI;

	// Use this for initialization
	void Start () 
    {
        m_PlayerController = this.gameObject.GetComponent<PlayerController>();
        m_PlayerUI = this.gameObject.GetComponent<PlayerUI>();
        m_CurrentSpookJuiceDecreaseTimer = SPOOKJUICE_DECREASE_INTERVAL;
        m_CurrentSpookJuice = MAX_SPOOKJUICE;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

	private void LateUpdate()
	{
        m_CurrentSpookJuiceDecreaseTimer -= Time.deltaTime;

        if (m_CurrentSpookJuiceDecreaseTimer <= 0)
        {
            m_CurrentSpookJuiceDecreaseTimer = SPOOKJUICE_DECREASE_INTERVAL;
            m_CurrentSpookJuice -= SPOOKJUICE_DECREASE_AMOUNT;
        }

        if (m_CurrentSpookJuice <= 0)
        {
            //player is dead.... even though he is already dead....
            //does that make him double dead? Can a ghost die? :thinking emoji:

            m_CurrentSpookJuice = MAX_SPOOKJUICE;
        }

	}

    public float GetCurrentSpookJuice()
    {
        return m_CurrentSpookJuice;
    }

    public void AddToSpookJuice(float amountToAdd)
    {
        m_CurrentSpookJuice += amountToAdd;

        if (m_CurrentSpookJuice >= MAX_SPOOKJUICE)
        {
            m_CurrentSpookJuice = MAX_SPOOKJUICE;
        }
    }

    public void RemoveFromSpookJuice(float amountToRemove)
    {
        m_CurrentSpookJuice -= amountToRemove;
    }
}