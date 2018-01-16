using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDisplay : MonoBehaviour {

    public GameObject[] m_PowerUpSprites; //Should be in same order as PowerupManager.PowerUps enum
    public GameObject m_BaseMask;
    public Vector3 m_LowerMaskPos;
    public Vector3 m_UpperMaskPos;

    private int m_RechargeTime;
    private int m_RechargeTimer;
    private bool m_recharging;

    // Use this for initialization
    void Start () {
        m_recharging = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(m_recharging)
        {
            ++m_RechargeTimer;
            float lerpFactor = ((float) m_RechargeTimer) / ((float) m_RechargeTime);
            m_BaseMask.transform.localPosition = Vector3.Lerp(m_LowerMaskPos, m_UpperMaskPos, lerpFactor);
            if (lerpFactor >= 1)
                m_recharging = false;
        }
	}

    public void StartRechargeAnim(int rechargeTime, PowerupManager.PowerUps nextPower)
    {
        m_RechargeTime = rechargeTime;
        m_RechargeTimer = 0;
        foreach (GameObject spr in m_PowerUpSprites)
            spr.SetActive(false);
        m_PowerUpSprites[(int)nextPower].SetActive(true);
        m_recharging = true;
    }

    public void SetDisplayInactive()
    {
        m_BaseMask.transform.localPosition = m_LowerMaskPos;
        foreach (GameObject spr in m_PowerUpSprites)
            spr.SetActive(false);
    }

    public void SetDisplayActive(PowerupManager.PowerUps nextPower)
    {
        m_BaseMask.transform.localPosition = m_UpperMaskPos;
        foreach (GameObject spr in m_PowerUpSprites)
            spr.SetActive(false);
        m_PowerUpSprites[(int)nextPower].SetActive(true);
    }
}
