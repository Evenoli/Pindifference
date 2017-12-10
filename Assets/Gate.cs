using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public Color m_activeCol;
    public Color m_inactiveCol;
    public GameObject m_indicator;

    private Material m_indicatorMat;

    private bool m_gateActive;

    private PointManager m_pointMan;

    // Use this for initialization
    void Start () {
        m_pointMan = transform.root.gameObject.GetComponent<PointManager>();
        m_gateActive = false;
        m_indicatorMat = m_indicator.GetComponent<Renderer>().material;
        m_indicatorMat.color = m_inactiveCol;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Ball")
        {
            m_gateActive = !m_gateActive;
            if (m_gateActive)
                m_indicatorMat.color = m_activeCol;
            else
                m_indicatorMat.color = m_inactiveCol;

            m_pointMan.AddToScore(PointManager.InteractionType.GATE);
        }
    }

    public bool IsGateActive()
    {
        return m_gateActive;
    }

    public void ResetGate()
    {
        m_gateActive = false;
        m_indicatorMat.color = m_inactiveCol;
    }
}
