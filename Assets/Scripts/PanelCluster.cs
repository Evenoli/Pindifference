using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCluster : MonoBehaviour {

    private PointManager m_pointMan;

    public Panel[] panels;

    // Use this for initialization
    void Start () {
        m_pointMan = transform.root.gameObject.GetComponent<PointManager>();
    }
	
	// Update is called once per frame
	void Update () {

        bool allPanelsDown = true;

		foreach(Panel p in panels)
        {
            if (!p.isDown())
                allPanelsDown = false;
        }

        if(allPanelsDown)
        {
            m_pointMan.GetPowerup();
            foreach (Panel p in panels)
            {
                p.Reset();
            }
        }
	}
}
