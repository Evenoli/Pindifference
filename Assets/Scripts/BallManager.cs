using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    // List of all ball objects currently on this table
    private List<GameObject> m_BallsList;

    public BallSpawner m_Spawner;
	// Frames to wait after spawning a new ball before doing another
	// Avoids double spawns
	private int m_SpawnCooldown = 10; 
	private int m_CooldownCounter;
    private bool m_CooldownActive;

    // Use this for initialization
    void Start() {
        m_BallsList = new List<GameObject>();
		m_CooldownCounter = 0;
        m_CooldownActive = false;
    }

    private void Update()
    {
        if(m_CooldownActive)
        {
            ++m_CooldownCounter;
            if (m_CooldownCounter >= m_SpawnCooldown)
                m_CooldownActive = false;
        }

        if (m_BallsList.Count == 0 && !m_CooldownActive)
        {
            m_Spawner.SpawnBall();
            m_CooldownCounter = 0;
            m_CooldownActive = true;
        }
    }

    public List<GameObject> GetBalls()
    {
        return m_BallsList;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Ball")
        {
            m_BallsList.Add(obj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Ball")
        {
            m_BallsList.Remove(obj);
        }
    }

    internal void RemoveBall(GameObject ball)
    {
        m_BallsList.Remove(ball);
    }
}
