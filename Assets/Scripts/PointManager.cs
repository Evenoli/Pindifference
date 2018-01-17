using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour {

    public enum InteractionType { BUMPER, PANEL, GATE, PIPEBOX, BALLLOSS };

    public int m_startingBallsRem;
    public int m_curBallsRem;

    private PowerupManager m_PowerMan;

    private int m_curScore;
    private int m_multiplyer;

    public const int bumperScore = 50;
    public const int panelScore = 100;
    public const int gateScore = 100;
    public const int pipeBoxScore = 300;
    public const int ballLossPenalty = -1000;

    public Text scoreText;
    //public Text BallsRemText;

	// Use this for initialization
	void Start () {
        m_curBallsRem = m_startingBallsRem;
        m_curScore = 0;
        m_multiplyer = 1;
        m_PowerMan = GetComponent<PowerupManager>();

    }

    public void AddToScore(InteractionType interaction)
    {
        switch(interaction)
        {
            case InteractionType.BUMPER:
                m_curScore += bumperScore * m_multiplyer;
                break;

            case InteractionType.PANEL:
                m_curScore += panelScore * m_multiplyer;
                break;

            case InteractionType.GATE:
                m_curScore += gateScore * m_multiplyer;
                break;

            case InteractionType.PIPEBOX:
                m_curScore += pipeBoxScore * m_multiplyer;
                break;

            case InteractionType.BALLLOSS:
                m_curScore += ballLossPenalty;
                break;
        }
    }

    public void AddToScore(int score)
    {
        m_curScore += score * m_multiplyer;
    }

    public int GetScore()
    {
        return m_curScore;
    }

    public void ResetScore()
    {
        m_curScore = 0;
    }

    public void IncreaseMultiplyer()
    {
        m_multiplyer++;
    }

    public void DecreaseMultiplyer()
    {
        if (m_multiplyer > 1)
            m_multiplyer--;
    }

    public void GetPowerup()
    {
        m_PowerMan.AddRandomPowerToQueue();
    }

    public void SetMultiplyer(int newMult)
    {
        m_multiplyer = newMult;
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = m_curScore.ToString();
        //BallsRemText.text = m_curBallsRem.ToString();
	}
}
