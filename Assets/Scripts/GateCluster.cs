using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCluster : MonoBehaviour {

    private PointManager m_pointMan;

    public Gate[] gates;

    // Use this for initialization
    void Start()
    {
        m_pointMan = transform.root.gameObject.GetComponent<PointManager>();
    }

    // Update is called once per frame
    void Update()
    {

        bool allGatesActive = true;

        foreach (Gate g in gates)
        {
            if (!g.IsGateActive())
                allGatesActive = false;
        }

        if (allGatesActive)
        {
            m_pointMan.GetPowerup();
            foreach (Gate g in gates)
            {
                g.ResetGate();
            }
        }
    }
}
