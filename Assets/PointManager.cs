using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour {

    public enum InteractionType { BUMPER, PANEL, GATE };

    public int m_startingBallsRem;
    public int m_curBallsRem;

    private int m_curScore;
    private int m_multiplyer;

    public const int bumperScore = 50;
    public const int panelScore = 100;
    public const int gateScore = 100;

    public Text scoreText;
    public Text BallsRemText;

	// Use this for initialization
	void Start () {
        m_curBallsRem = m_startingBallsRem;
        m_curScore = 0;
        m_multiplyer = 1;
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

    public void IncreaseMultiplyer()
    {
        m_multiplyer++;
    }

    public void DecreaseMultiplyer()
    {
        if (m_multiplyer > 1)
            m_multiplyer--;
    }

    public void SetMultiplyer(int newMult)
    {
        m_multiplyer = newMult;
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = m_curScore.ToString();
        BallsRemText.text = m_curBallsRem.ToString();
	}
}
