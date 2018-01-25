using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBox : MonoBehaviour {

    public float m_LaunchSpeed;

    // The contents order of these two arrays should correspond to each other
    public GameObject[] m_DirectionTriangles;
    public GameObject[] m_LaunchDirections;
    public GameObject m_BallPrefab;
    public Material m_BallMat;

    public Color m_TriangleHightlightCol;
    private Color m_DefaultTriangleCol;

    public int m_HoldTime;
    private int m_HoldCount;

    private PointManager m_PointMan;

    private bool m_HoldingBall;
    private GameObject m_HeldBall;
    public GameObject m_HeldBallPos;

    private bool m_SelectingDirection;

    private List<int> m_SelectedDirections;
    private int m_LaunchCountdown;
    public int m_LaunchTime;

    private int m_resetTime = 5;
    private int m_ResetTimer;
    private bool m_resetting;

    private int m_BallLaunchQueueCount;

    // Use this for initialization
    void Start () {
        m_PointMan = transform.root.gameObject.GetComponent<PointManager>();
        m_DefaultTriangleCol = m_DirectionTriangles[0].GetComponent<Renderer>().material.color;
        m_SelectingDirection = false;
        m_HoldingBall = false;
        m_resetting = false;
        m_BallLaunchQueueCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_SelectingDirection && m_HoldingBall)
        {
            m_HoldCount++;
            m_HeldBall.transform.position = m_HeldBallPos.transform.position;

            //Fast random highlighting of arrows
            if(m_HoldCount < (m_HoldTime / 2))
            {
                if (m_HoldCount % 2 == 0)
                { 
                    List<int> highlights = new List<int>();
                    for(int i=0; i<m_LaunchDirections.Length; i++)
                    {
                        if (Random.value > 0.5)
                            highlights.Add(i);
                    }
                    HighlightTriangle(highlights);
                }
            }
            // highlighting of arrows slows
            else if(m_HoldCount < m_HoldTime)
            {
                if (m_HoldCount % 10 == 0)
                {
                    List<int> highlights = new List<int>();
                    for (int i = 0; i < m_LaunchDirections.Length; i++)
                    {
                        if (Random.value > 0.5)
                            highlights.Add(i);
                    }
                    HighlightTriangle(highlights);
                }
            }
            // Pick final direction(s) of launch
            else
            {
                //How many balls to launch?
                int numBalls;
                float rnd = Random.value;
                if (rnd >= 0.95)
                    numBalls = 4;
                else if (rnd >= 0.85)
                    numBalls = 3;
                else if (rnd >= 0.75)
                    numBalls = 2;
                else
                    numBalls = 1;

                List<int> selectedDirections = new List<int>();
                if (numBalls != 4)
                {
                    for (int i = 0; i < numBalls; i++)
                    {
                        int dir;
                        do
                            dir = (int)(Random.value / 0.25f);
                        while (selectedDirections.Contains(dir));

                        selectedDirections.Add(dir);
                    }
                }
                else
                {
                    selectedDirections.Add(0);
                    selectedDirections.Add(1);
                    selectedDirections.Add(2);
                    selectedDirections.Add(3);
                }

                

                HighlightTriangle(selectedDirections);
                m_LaunchCountdown = m_LaunchTime;
                m_SelectedDirections = selectedDirections;
                m_SelectingDirection = false;
            }
        }
        else if(m_HoldingBall && !m_SelectingDirection)
        {
            --m_LaunchCountdown;
            m_HeldBall.transform.position = m_HeldBallPos.transform.position;

            if (m_LaunchCountdown <= 0) //Launch ball(s)
            {
                m_HoldingBall = false;
                foreach(int dir in m_SelectedDirections)
                {
                    Transform trns = m_LaunchDirections[dir].transform;
                    GameObject newBall = Object.Instantiate(m_BallPrefab);
                    newBall.GetComponent<Renderer>().material = m_BallMat;
                    newBall.transform.position = trns.position;
                    newBall.GetComponent<Rigidbody>().velocity = trns.TransformDirection(new Vector3(0, m_LaunchSpeed, 0));
                }
                HighlightTriangle(new List<int>()); // Clear highlights
                m_ResetTimer = 0;
                m_resetting = true;
            }

            
        }
        else if(m_resetting)
        {
            // Wait a few frames to destroy old ball to avoid a new one being spawned in the launcher 
            ++m_ResetTimer;
            if(m_ResetTimer >= m_resetTime)
            {
                m_resetting = false;
                if (m_BallLaunchQueueCount == 0)
                {
                    transform.root.gameObject.GetComponent<BallManager>().RemoveBall(m_HeldBall); //Destroy old ball, unless we have balls in queue
                    GameObject.Destroy(m_HeldBall);
                }
                else
                {
                    m_HoldingBall = true;
                    m_SelectingDirection = true;
                    m_HeldBall.transform.position = m_HeldBallPos.transform.position;
                    m_HoldCount = 0;
                    m_PointMan.AddToScore(PointManager.InteractionType.PIPEBOX);
                    m_PointMan.GetPowerup();
                    --m_BallLaunchQueueCount;
                }
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Ball")
        {
            if (!(m_HoldingBall || m_SelectingDirection || m_resetting))
                {
                obj.transform.position = m_LaunchDirections[0].transform.position;
                m_HoldingBall = true;
                m_SelectingDirection = true;
                m_HeldBall = obj;
                m_HeldBall.transform.position = m_HeldBallPos.transform.position;
                m_HoldCount = 0;
                m_PointMan.AddToScore(PointManager.InteractionType.PIPEBOX);
                //m_PointMan.GetPowerup();
            }
            else
            {
                ++m_BallLaunchQueueCount;
                transform.root.gameObject.GetComponent<BallManager>().RemoveBall(obj); //Destroy new ball and add to queue
                GameObject.Destroy(obj);
            }
        }
    }

    

    // highlights triangles from m_DirectionTriangles based on ints passed in
    private void HighlightTriangle(List<int> triNums)
    {
        for(int i=0; i<m_DirectionTriangles.Length; i++)
        {
            if (triNums.Contains(i))
                m_DirectionTriangles[i].GetComponent<Renderer>().material.color = m_TriangleHightlightCol;
            else
                m_DirectionTriangles[i].GetComponent<Renderer>().material.color = m_DefaultTriangleCol;
        }
    }
}
