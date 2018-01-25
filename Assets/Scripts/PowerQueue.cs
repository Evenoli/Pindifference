using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerQueue : MonoBehaviour {

    public PowerQueueArrow m_Arrow;
    public PowerQueueDisplay[] m_Displays; // indexed such that 0 is 'next' power

	// Use this for initialization
	void Start () {
        m_Arrow.SetColourActive(false);
        foreach (PowerQueueDisplay pqd in m_Displays)
            pqd.Hide();
	}

    public void UpdateQueueDisplay(Queue<PowerupManager.PowerUps> queue)
    {
        Queue<PowerupManager.PowerUps> queueCopy = new Queue<PowerupManager.PowerUps>(queue);

        // disregard first item
        if (queueCopy.Count > 0)
            queueCopy.Dequeue();

        if (queueCopy.Count > 0)
            m_Arrow.SetColourActive(true);
        else
            m_Arrow.SetColourActive(false);

        foreach (PowerQueueDisplay pqd in m_Displays)
            pqd.Hide();

        for(int i=0; i < m_Displays.Length && i < queueCopy.Count; i++)
        {
            m_Displays[i].DisplayPower(queueCopy.Dequeue());
        }
    }
}
