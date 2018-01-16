using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBin : MonoBehaviour {

    private PointManager m_Pointman;
    private BallManager m_Ballman;

	// Use this for initialization
	void Start () {
        m_Pointman = transform.root.GetComponent<PointManager>();
        m_Ballman = transform.root.GetComponent<BallManager>();

    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Ball")
        {
            m_Pointman.AddToScore(PointManager.InteractionType.BALLLOSS);
            m_Ballman.RemoveBall(obj);
            GameObject.Destroy(obj);
        }
    }
}
