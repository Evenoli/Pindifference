using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    // List of all ball objects currently on this table
    private List<GameObject> m_BallsList;

    public BallSpawner m_Spawner;

    // Use this for initialization
    void Start() {
        m_BallsList = new List<GameObject>();
    }

    private void Update()
    {
        if (m_BallsList.Count == 0)
            m_Spawner.SpawnBall();
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
