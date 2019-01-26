﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour 
{
    public GameObject m_SpookJuiceBar;
    private Image m_SpookJuiceImage;

    PlayerStats m_PlayerStats;

	// Use this for initialization
	void Start () 
    {
        m_SpookJuiceImage = m_SpookJuiceBar.gameObject.GetComponent<Image>();
        m_PlayerStats = this.gameObject.GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

	private void LateUpdate()
	{
        UpdateSpookJuiceUI();
	}

    private void UpdateSpookJuiceUI()
    {
        m_SpookJuiceImage.fillAmount = m_PlayerStats.GetCurrentSpookJuice() / 100.0f;
    }
}