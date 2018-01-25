using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerQueueArrow : MonoBehaviour {

    public GameObject[] m_ArrowParts;

    public Color m_ActiveCol;
    public Color m_InactiveCol;

    public void SetColourActive(bool active)
    {
        if(active)
        {
            foreach (GameObject part in m_ArrowParts)
                part.GetComponent<Renderer>().material.color = m_ActiveCol;
        }
        else
        {
            foreach (GameObject part in m_ArrowParts)
                part.GetComponent<Renderer>().material.color = m_InactiveCol;
        }
    }
}
