using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour 
{
    public GameObject m_SpookJuiceBar;
    public GameObject m_SpookyBar;
    public GameObject m_SpookyFace;

    public GameObject m_EndText;
    public GameObject m_EndScoreText;
    public GameObject m_MainMenuButton;

    public Text m_SpoopyScoreText;
    private string SPOOPY_TEXT = "SPOOPY SCORE: ";
    private string FINAL_SCORE_TEXT = "FINAL SCORE: ";

    private Image m_SpookJuiceImage;
    private float m_SpookyEvilTimer = 0.0f;
    private const float SPOOKY_EVIL_TIMER_MAX = 10f;

    private bool m_bTriggeredSpookyScore = false;

    float spookJuice = 30f;

    PlayerStats m_PlayerStats;
    PlayerController m_PlayerController;

	// Use this for initialization
	void Start () 
    {
        m_SpookJuiceImage = m_SpookJuiceBar.gameObject.GetComponent<Image>();
        m_PlayerStats = this.gameObject.GetComponent<PlayerStats>();
        m_PlayerController = this.gameObject.GetComponent<PlayerController>();
        m_SpookyEvilTimer = SPOOKY_EVIL_TIMER_MAX;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_PlayerController.GetIsDead())
        {
            GameOverScreen();
        }

        else
        {
            spookJuice -= 1 * Time.deltaTime;
            UpdateSpookJuiceUI();
            UpdateSpoopyScoreUI();

            if (m_bTriggeredSpookyScore == true)
            {
                m_SpookyEvilTimer -= Time.deltaTime;
            }
        }
	}

	private void LateUpdate()
	{
        //UpdateSpookJuiceUI();

        if (m_PlayerStats.GetScore() == 666 && m_bTriggeredSpookyScore == false)
        {
            m_bTriggeredSpookyScore = true;
        }
	}

    private void UpdateSpookJuiceUI()
    {
        m_SpookJuiceImage.fillAmount = m_PlayerStats.GetCurrentSpookJuice() / 100.0f;
        //m_SpookJuiceBar.transform.localScale = new Vector2(spookJuice / SPOOKY_EVIL_TIMER_MAX, 1);
    }

    private void UpdateSpoopyScoreUI()
    {
        m_SpoopyScoreText.text = SPOOPY_TEXT + m_PlayerStats.GetScore().ToString();
    }

    private void UpdateEvilSpoopyText()
    {
        if (m_SpookyEvilTimer >= 0.0f)
        {
            m_SpoopyScoreText.color = new Color(255, 0, 0);
        }

        else
        {
            m_bTriggeredSpookyScore = false;
            m_SpoopyScoreText.color = new Color(255, 255, 255);
        }
    }

    private void GameOverScreen()
    {
        m_SpookyBar.SetActive(false);
        m_SpookyFace.SetActive(false);
        m_SpoopyScoreText.gameObject.SetActive(false);

        m_EndText.SetActive(true);
        m_EndScoreText.SetActive(true);
        m_MainMenuButton.SetActive(true);

        m_EndScoreText.GetComponent<Text>().text = FINAL_SCORE_TEXT + m_PlayerStats.GetScore().ToString();
    }
}
