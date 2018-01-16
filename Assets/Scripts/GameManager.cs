using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject[] m_PlayerTables;
    public float m_GameLength; //In Seconds
    private float m_GameStartTime;
    private float m_GameTimeRem;

    private bool m_GameInProgress;
    private bool m_GameStarting;

    private int m_Countdown;
    private int m_CountdownTimer;

    public GameObject[] m_InitialInstructionText;
    public GameObject m_p1WinText;
    public GameObject m_p2WinText;
    public GameObject m_RestartCommand;
    public Text m_GameTimer;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        m_GameStarting = false;
        m_GameInProgress = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!m_GameInProgress && !m_GameStarting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_GameStarting = true;
                m_p1WinText.SetActive(false);
                m_p2WinText.SetActive(false);
                m_RestartCommand.SetActive(false);
                foreach (GameObject ui in m_InitialInstructionText)
                    ui.SetActive(false);
                m_GameStarting = true;

                m_PlayerTables[0].GetComponent<PointManager>().ResetScore();
                m_PlayerTables[1].GetComponent<PointManager>().ResetScore();

                m_Countdown = 3;
                m_CountdownTimer = 0;
            }
        }
        else if(m_GameStarting)
        {
            ++m_CountdownTimer;
            if (m_CountdownTimer % 50 == 0)
                --m_Countdown;
            m_GameTimer.text = m_Countdown.ToString();
            if(m_Countdown == 0)
            {
                m_GameStarting = false;
                m_GameInProgress = true;
                m_GameStartTime = Time.time;
                m_GameTimeRem = m_GameLength;
                Time.timeScale = 1;
            }
        }
        else if(m_GameInProgress)
        {
            m_GameTimeRem = m_GameLength - (Time.time - m_GameStartTime);
            m_GameTimer.text = FormatTime(m_GameTimeRem);
            
            if(m_GameTimeRem <= 0)
            {
                m_GameInProgress = false;
                Time.timeScale = 0;
                int p1Score = m_PlayerTables[0].GetComponent<PointManager>().GetScore();
                int p2Score = m_PlayerTables[1].GetComponent<PointManager>().GetScore();

                if (p1Score > p2Score)
                    m_p1WinText.SetActive(true);
                else if (p2Score > p1Score)
                    m_p2WinText.SetActive(true);
                else if (p1Score == p2Score)
                {
                    m_p1WinText.SetActive(true);
                    m_p2WinText.SetActive(true);
                }

                m_RestartCommand.SetActive(true);
            }
        }
	}

    private string FormatTime(float time)
    {
        int mins = (int) time / 60;
        int secs = (int) time % 60;

        return string.Format("{0:00} : {1:00}", mins, secs);
    }
}
