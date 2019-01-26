using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour 
{
    public GameObject m_SpookJuiceBar;
    private Image m_SpookJuiceImage;
    private float m_SpookyEvilTimer = 0.0f;
    private const float SPOOKY_EVIL_TIMER_MAX = 30f;

    float spookJuice = 30f;

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
        spookJuice -= 1 * Time.deltaTime;
        UpdateSpookJuiceUI();
	}

	private void LateUpdate()
	{
        //UpdateSpookJuiceUI();
	}

    private void UpdateSpookJuiceUI()
    {
        //m_SpookJuiceImage.fillAmount = m_PlayerStats.GetCurrentSpookJuice() / 100.0f;
        m_SpookJuiceBar.transform.localScale = new Vector2(spookJuice / SPOOKY_EVIL_TIMER_MAX, 1);
    }

}
