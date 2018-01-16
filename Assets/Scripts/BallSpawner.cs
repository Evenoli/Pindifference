using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

	public GameObject ballPrefab;
    public string inputName;

    private bool m_axisPressed;

    // Use this for initialization
    void Start () {
        m_axisPressed = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis(inputName) == 1 && !m_axisPressed) {
            SpawnBall();
            m_axisPressed = true;
		}

        if (Input.GetAxis(inputName) == 0)
            m_axisPressed = false;

    }

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab);
        newBall.transform.position = transform.position ;
        newBall.transform.localScale = new Vector3(2, 2, 2);
    }
}
