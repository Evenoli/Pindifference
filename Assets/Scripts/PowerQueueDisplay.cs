using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerQueueDisplay : MonoBehaviour {

    public GameObject[] m_PowerSymbols;
    public GameObject m_Base;

	// Use this for initialization
	void Start () {
		
	}
	
	public void DisplayPower(PowerupManager.PowerUps power)
    {
        m_Base.SetActive(true);

        foreach (GameObject sym in m_PowerSymbols)
            sym.SetActive(false);

        m_PowerSymbols[(int)power].SetActive(true);
    }

    public void Hide()
    {
        foreach (GameObject sym in m_PowerSymbols)
            sym.SetActive(false);

        m_Base.SetActive(false);
    }
}
